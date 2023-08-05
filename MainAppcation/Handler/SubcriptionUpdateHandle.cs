using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetProxyController.Handler
{
    internal class SubcriptionUpdateHandle
    {
        private static SubcriptionUpdateHandle? instance;
        public static SubcriptionUpdateHandle Instance => instance ??= new SubcriptionUpdateHandle();
        private List<(Task Task,CancellationTokenSource Cts)> AutoUpdateTasks;
        public SubcriptionUpdateHandle()
        {
            AutoUpdateTasks = new();
        }
        public void StartAutoUpdateTasks()
        {
            AutoUpdateTasks.Clear();
            foreach(var item in SubscriptionItem.SubscriptionItemDataList.Where(i => i.IsAutoUpdate))
            {
                (Task Task, CancellationTokenSource Cts) taskInfo;
                taskInfo.Cts = new();
                var updateCts = CancellationTokenSource.CreateLinkedTokenSource(taskInfo.Cts.Token);
                updateCts.CancelAfter(TimeSpan.FromSeconds(10));
                _ = UpdateSubcriptionItem(item, updateCts.Token);
                taskInfo.Task = Task.Run(() =>
                {
                    do
                    {
                        
                    } while (!taskInfo.Cts.IsCancellationRequested);
                });


            }
        }
        public async Task<bool> UpdateSubcriptionItem(SubscriptionItem subItem, CancellationToken? token = null)
        {
            using HttpClient http = new()
            {
                Timeout = TimeSpan.FromSeconds(10)
            };
            var subContent = await http.GetStringAsync(subItem.Url, token ?? new());
            if(!string.IsNullOrEmpty(subContent))
            {
                var serverItems = SubscriptionResolveHandle.ResolveSubFromSubContent(subContent);
                var oldServerItems = ServerItem.ServerItemsDataList.Where(i => i.SubGroupId == subItem.SubcriptionId).ToList();
                foreach (var serverItem in serverItems)
                {
                    serverItem.IsActivated = oldServerItems.FirstOrDefault(i => i.Remarks == serverItem.Remarks)?.IsActivated ?? default;
                    serverItem.SubGroupId = subItem.SubcriptionId;
                    serverItem.SaveToDataBase();
                }
                var defaultServerName = oldServerItems.FirstOrDefault(i => i.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex)?.Remarks;
                if(!string.IsNullOrEmpty(defaultServerName))
                {
                    var defaultIndex = serverItems.FirstOrDefault(i => i.Remarks == defaultServerName)?.Index ?? ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex;
                    ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex = defaultIndex;
                }
                if(serverItems.Count > 0)
                {
                    oldServerItems.ForEach(i => i.DeleteFromDataBase());
                    subItem.LastUpdateTime = DateTime.Now;
                    return true;
                }
            }
            return false;
        }
    }
}

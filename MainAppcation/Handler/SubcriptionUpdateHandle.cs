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
        private Dictionary<Guid, (Task Task, CancellationTokenSource Cts)> AutoUpdateTasks;
        public SubcriptionUpdateHandle()
        {
            AutoUpdateTasks = new();
        }
        private void StartAutoUpdateTasks()
        {
            AutoUpdateTasks.Clear();
            foreach(var item in SubscriptionItem.SubscriptionItemDataList.Where(i => i.IsAutoUpdate && i.AutoUpdateInterval > 0))
            {
                (Task Task, CancellationTokenSource Cts) taskInfo;
                taskInfo.Cts = new();
                taskInfo.Task = Task.Run(() =>
                {
                    while (!taskInfo.Cts.IsCancellationRequested)
                    {
                        try
                        {
                            var updateCts = CancellationTokenSource.CreateLinkedTokenSource(taskInfo.Cts.Token);
                            updateCts.CancelAfter(TimeSpan.FromSeconds(10));
                            if (DateTime.Now >= item.LastUpdateTime.AddMinutes(item.AutoUpdateInterval))
                            {
                                UpdateSubcriptionItem(item, updateCts.Token).Wait();
                            }
                            Task.Delay(TimeSpan.FromSeconds(item.AutoUpdateInterval), taskInfo.Cts.Token);
                        }
                        catch (Exception) { }
                    }
                });
            }
        }
        private void AddAutoUpdateTask(SubscriptionItem SubItem)
        {
            if (!SubItem.IsAutoUpdate) return;
            (Task Task, CancellationTokenSource Cts) taskInfo;
            taskInfo.Cts = new();
            if(SubItem.AutoUpdateInterval > 0)
            {
                taskInfo.Task = Task.Run(() =>
                {
                    do
                    {
                        try
                        {
                            var updateCts = CancellationTokenSource.CreateLinkedTokenSource(taskInfo.Cts.Token);
                            updateCts.CancelAfter(TimeSpan.FromSeconds(10));
                            if (DateTime.Now >= SubItem.LastUpdateTime.AddMinutes(SubItem.AutoUpdateInterval))
                            {
                                UpdateSubcriptionItem(SubItem, updateCts.Token).Wait();
                            }
                            Task.Delay(TimeSpan.FromSeconds(SubItem.AutoUpdateInterval), taskInfo.Cts.Token);
                        }
                        catch (Exception) { }
                    } while (!taskInfo.Cts.IsCancellationRequested && SubItem.AutoUpdateInterval > 0);
                });
            }
            else
            {
                
            }
        }
        public async Task<bool> UpdateSubcriptionItem(SubscriptionItem subItem, CancellationToken? token = null)
        {
            subItem.LastUpdateTime = DateTime.Now;
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
                    return true;
                }
            }
            return false;
        }
    }
}

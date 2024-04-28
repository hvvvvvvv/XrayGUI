using XrayGUI.Modle;
using XrayGUI.Modle.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vanara.Extensions;

namespace XrayGUI.Handler
{
    internal class SubcriptionUpdateHandle
    {
        private static SubcriptionUpdateHandle? instance;
        public static SubcriptionUpdateHandle Instance => instance ??= new SubcriptionUpdateHandle();
        private Dictionary<Guid, (Task Task, CancellationTokenSource Cts)> AutoUpdateTasks;
        private Dictionary<Guid, DateTime> LastUpdateOprationTimes;
        private Dictionary<Guid, bool> SubItemIsUpdating;
        public delegate void UpdateEventHandler(SubItemUpdateEventArgs arg);
        public event UpdateEventHandler? UpdateEvent;
        public SubcriptionUpdateHandle()
        {
            AutoUpdateTasks = new();
            LastUpdateOprationTimes = new();
            SubItemIsUpdating = new();
            StartAutoUpdateTasks();
            
        }
        private void StartAutoUpdateTasks()
        {
            foreach(var item in Global.DBService.Table<SubscriptionItem>().Where(i => i.IsAutoUpdate && i.AutoUpdateInterval > 0))
            {
                AddAutoUpdateTask(item);
            }
        }
        private void AddAutoUpdateTask(SubscriptionItem SubItem)
        {
            if (!SubItem.IsAutoUpdate) return;
            if(!LastUpdateOprationTimes.ContainsKey(SubItem.SubcriptionId))
            {
                LastUpdateOprationTimes.Add(SubItem.SubcriptionId, default);
            }
            (Task Task, CancellationTokenSource Cts) taskInfo;
            taskInfo.Cts = new();
            taskInfo.Task = Task.Run(() =>
            {
                do
                {
                    if (SubItem.AutoUpdateInterval <= 0 && LastUpdateOprationTimes[SubItem.SubcriptionId] != default) break;
                    if (DateTime.Now >= LastUpdateOprationTimes[SubItem.SubcriptionId].AddMinutes(SubItem.AutoUpdateInterval))
                    {
                        UpdateSubcriptionItem(SubItem, taskInfo.Cts).Wait();
                        LastUpdateOprationTimes[SubItem.SubcriptionId] = DateTime.Now;
                    }
                    try
                    {
                        var delayTimeSec = (LastUpdateOprationTimes[SubItem.SubcriptionId].AddMinutes(SubItem.AutoUpdateInterval) - DateTime.Now).TotalSeconds;
                        Task.Delay(TimeSpan.FromSeconds(delayTimeSec), taskInfo.Cts.Token).Wait();
                    }
                    catch { }
                }
                while (!taskInfo.Cts.IsCancellationRequested);
            });
            AutoUpdateTasks[SubItem.SubcriptionId] = taskInfo;
        }
        public void ReloadAutoUpdateTask(SubscriptionItem SubItem)
        {
            RemoveAutoUpdateTask(SubItem);
            AddAutoUpdateTask(SubItem);
        }
        public void RemoveAutoUpdateTask(SubscriptionItem subItem)
        {
            if (AutoUpdateTasks.ContainsKey(subItem.SubcriptionId))
            {
                AutoUpdateTasks[subItem.SubcriptionId].Cts.Cancel();
                AutoUpdateTasks[subItem.SubcriptionId].Task.Wait();
                AutoUpdateTasks.Remove(subItem.SubcriptionId);
            }
        }

        public async Task<bool> UpdateSubcriptionItem(SubscriptionItem subItem, CancellationTokenSource cts)
        {
            var ret = false;
            var msg = string.Empty;
            var requestCts = CancellationTokenSource.CreateLinkedTokenSource(cts.Token);
            requestCts.CancelAfter(TimeSpan.FromSeconds(10));
            using HttpClient http = subItem.IsProxyUpdate ? new(new SocketsHttpHandler { UseProxy = true, Proxy = new WebProxy(IPAddress.Loopback.ToString(), ConfigObject.Instance.localPort.Http) }) : new();
            try
            {
                lock (SubItemIsUpdating)
                {
                    if (SubItemIsUpdating.ContainsKey(subItem.SubcriptionId) && SubItemIsUpdating[subItem.SubcriptionId]) throw new AvoidException();
                    SubItemIsUpdating[subItem.SubcriptionId] = true;
                }
                var subContent = await http.GetStringAsync(subItem.Url, requestCts.Token);
                if (!string.IsNullOrEmpty(subContent))
                {
                    var serverItems = SubscriptionResolveHandle.ResolveSubFromSubContent(subContent);
                    if (serverItems.Count > 0)
                    {
                        serverItems.ForEach(i => i.SubGroupId = subItem.SubcriptionId);
                        var updatetime = UpdateServerItems(serverItems);
                        subItem.LastUpdateTime = updatetime;
                        subItem.Save();
                        serverItems.Where(i => i.SubGroupId == subItem.SubcriptionId && i.UpdatedTime != updatetime.Ticks).ToList().ForEach(i => i.DeleteFromDataBase());
                        ret = true;
                    }
                    else
                    {
                        msg = "未能解析订阅内容";
                    }
                }
            }
            catch (TaskCanceledException)           
            {
                if (cts.IsCancellationRequested)
                {
                    return false;     //当前线程被取消                           
                }
                msg = "请求订阅内容超时";
            }
            catch (HttpRequestException ex)
            {
                msg = $"请求订阅内容错误,Error:{ex.Message}";
            }
            catch (AvoidException)
            {
                return false;   //当前订阅正在更新，避让返回
            }
            lock (SubItemIsUpdating) SubItemIsUpdating[subItem.SubcriptionId] = false;
            UpdateEvent?.Invoke(new SubItemUpdateEventArgs(subItem, ret, msg));
            return ret;
        }
        private DateTime UpdateServerItems(List<ServerItem> serverItems)
        {
            var updatedtime = DateTime.Now;
            foreach (var item in serverItems)
            {
                var targetItem = ServerItem.ServerItemsDataList.FirstOrDefault(i => i.SubGroupId == item.SubGroupId && i.Remarks == item.Remarks);
                item.UpdatedTime = updatedtime.Ticks;
                if (targetItem != null)
                {                   
                    targetItem.UpdateFrom(item);
                    targetItem.SaveToDataBase();
                }
                else
                {
                    item.SaveToDataBase();
                }
            }
            return updatedtime;

        }
        private class AvoidException : Exception { }
        public class SubItemUpdateEventArgs : EventArgs
        {
            public SubItemUpdateEventArgs(SubscriptionItem subscription, bool isCompeleteUpdate, string errMsg)
            {
                Subscription = subscription;
                IsCompeleteUpdate = isCompeleteUpdate;
                ErrMsg = errMsg;
            }
            public SubscriptionItem Subscription { get; private set;}
            public bool IsCompeleteUpdate { get; private set; }
            public string ErrMsg { get; private set; }
        }
    }
}

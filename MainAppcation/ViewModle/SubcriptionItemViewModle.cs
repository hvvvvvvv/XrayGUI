using CommunityToolkit.Mvvm.Input;
using NetProxyController.Handler;
using NetProxyController.Modle.Server;
using NetProxyController.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class SubcriptionItemViewModle : ViewModleBase
    {
        public Modle.SubscriptionItem SubItem { get; private set; }
        private (Task task, CancellationTokenSource cts)? UpdateTaskInfo;
        public SubcriptionItemViewModle(Modle.SubscriptionItem subscription)
        {
            SubItem = subscription;
            UpdateData();
        }
        public void UpdateData()
        {
            SubName = SubItem.SubcriptionName;
            IsAutoUpdate = SubItem.IsAutoUpdate.ToString();
            Url = SubItem.Url;
            AutoUpdateInterval = SubItem.AutoUpdateInterval;
            LastUpdateTime = SubItem.LastUpdateTime == default ? "--" : SubItem.LastUpdateTime.ToString();
        }
        private string subName = default!;
        public string SubName
        {
            get => subName;
            private set
            {
                subName = value;
                OnPropertyChanged();
            }
        }
        private string isAutoUpdate = default!;
        public string IsAutoUpdate
        {
            get => isAutoUpdate;
            private set
            {
                isAutoUpdate = value;
                OnPropertyChanged();
            }
        }
        private string url = default!;
        public string Url
        {
            get => url;
            private set
            {
                url = value;
                OnPropertyChanged();
            }
        }
        private int autoUpdateInterval;
        public int AutoUpdateInterval
        {
            get => autoUpdateInterval; 
            private set
            {
                autoUpdateInterval = value;
                OnPropertyChanged();
            }
        }
        private string lastUpdateTime = default!;
        public string LastUpdateTime
        {
            get => lastUpdateTime;
            private set
            {
                lastUpdateTime = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand DoubleClickCmd => new(() => EditSubItem());
        public void UpdateSubItem()
        {
            if (UpdateTaskInfo is not null && !UpdateTaskInfo.Value.task.IsCompleted) return;
            CancellationTokenSource cts = new();
            cts.CancelAfter(TimeSpan.FromSeconds(10));
            UpdateTaskInfo = (Task.Run(() =>
            {
                if(SubcriptionUpdateHandle.Instance.UpdateSubcriptionItem(SubItem,cts.Token).Result)
                {
                    UpdateData();
                }
            }), cts);
        }
        public async void DeleteSubItem()
        {
            if (UpdateTaskInfo is not null && !UpdateTaskInfo.Value.task.IsCompleted)
            {
                UpdateTaskInfo.Value.cts.Cancel();
                await UpdateTaskInfo.Value.task;
            }
            ServerItem.ServerItemsDataList.Where(i => i.SubGroupId == SubItem.SubcriptionId).ToList().ForEach(i => i.DeleteFromDataBase());
            SubItem.DelateFormDataBase();
        }
        public bool EditSubItem()
        {
            var IsEdited = new EditSubcriptionItemView(SubItem).ShowDialog() ?? false;
            if(IsEdited)
            {
                UpdateData();
                SubcriptionUpdateHandle.Instance.ReloadAutoUpdateTask(SubItem);
            }
            return IsEdited;
        }

    }
}

using NetProxyController.Handler;
using NetProxyController.Modle.Server;
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
        private Task<bool>? updateItemTask;
        private bool isUpdating;
        private CancellationTokenSource? cts;
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
        public async void UpdateSubItem()
        {
            if (isUpdating) return;
            isUpdating = true;
            try
            {
                if (await SubcriptionUpdateHandle.Instance.UpdateSubcriptionItem(SubItem, (cts ??= new()).Token))
                {
                    UpdateData();
                }
            } catch (Exception) { }
            cts = null;
            isUpdating = false;
        }
        public async void DeleteSubItem()
        {
            cts?.Cancel();
            while (isUpdating) await Task.Delay(20);
            ServerItem.ServerItemsDataList.Where(i => i.SubGroupId == SubItem.SubcriptionId).ToList().ForEach(i => i.DeleteFromDataBase());
            SubItem.DelateFormDataBase();
        }

    }
}

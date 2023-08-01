using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class SubcriptionItemViewModle : ViewModleBase
    {
        public Modle.SubscriptionItem SubItem { get; private set; }
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
            lastUpdateTime = SubItem.LastUpdateTime.ToString();
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
                LastUpdateTime = value;
                OnPropertyChanged();
            }
        }
        public void Edit()
        {
            
        }

    }
}

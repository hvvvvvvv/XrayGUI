using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class EditSubcriptionItemViewMdole : ViewModleBase
    {
        public SubcriptionItemViewModle subItemViewModle;
        public EditSubcriptionItemViewMdole(SubcriptionItemViewModle itemVm)
        {
            subItemViewModle= itemVm;
            subName = subItemViewModle.SubItem.SubcriptionName;
            subUrl = subItemViewModle.SubItem.Url;
            isAutoUpdate = subItemViewModle.SubItem.IsAutoUpdate;
            autoUpdateInterval = subItemViewModle.SubItem.AutoUpdateInterval;
        }
        private string subName;
        public string SubName
        {
            get => subName;
            set
            {
                subName= value;
                OnPropertyChanged();
            }
        }
        private string subUrl;
        public string SubUrl
        {
            get => subUrl;
            set
            {
                subUrl = value;
                OnPropertyChanged();
            }
        }
        private bool isAutoUpdate;
        public bool IsAutoUpdate
        {
            get => isAutoUpdate; 
            set
            {
                isAutoUpdate = value;
                OnPropertyChanged();
            }
        }
        private int autoUpdateInterval;
        public int AutoUpdateInterval
        {
            get => autoUpdateInterval; 
            set
            {
                autoUpdateInterval = value;
                OnPropertyChanged();
            }
        }

    }
}

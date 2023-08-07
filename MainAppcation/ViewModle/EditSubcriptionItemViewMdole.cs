using CommunityToolkit.Mvvm.Input;
using NetProxyController.Modle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetProxyController.ViewModle
{
    internal class EditSubcriptionItemViewMdole : ViewModleBase
    {
        public SubscriptionItem subItem;
        public EditSubcriptionItemViewMdole(SubscriptionItem itemVm)
        {
            subItem = itemVm;
            subName = subItem.SubcriptionName;
            subUrl = subItem.Url;
            isAutoUpdate = subItem.IsAutoUpdate;
            autoUpdateInterval = subItem.AutoUpdateInterval;
            saveBtnCmd = new(SaveBtnExcute!);
        }
        private string subName;
        [Required(ErrorMessage = "名称不能为空")]
        public string SubName
        {
            get => subName;
            set
            {
                subName = value;
                ValidationProperty();
                OnPropertyChanged();
            }
        }
        
        private string subUrl;
        [Required(ErrorMessage = "地址不能为空")]
        public string SubUrl
        {
            get => subUrl;
            set
            {
                subUrl = value;
                ValidationProperty();
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
        [Range(0,int.MaxValue, ErrorMessage = "请输入正确的值")]
        public int AutoUpdateInterval
        {
            get => autoUpdateInterval; 
            set
            {
                autoUpdateInterval = value;
                ValidationProperty();
                OnPropertyChanged();
            }
        }
        private RelayCommand<Window> saveBtnCmd;
        public RelayCommand<Window> SaveBtnCmd
        {
            get => saveBtnCmd;
            set => _ = value;
        }
        private void SaveBtnExcute(Window win)
        {
            if (!ValidationAllProperty()) return;
            subItem.SubcriptionName = subName;
            subItem.Url = subUrl;
            subItem.IsAutoUpdate = isAutoUpdate;
            subItem.AutoUpdateInterval = autoUpdateInterval;
            subItem.SaveToDataBase();
            win.DialogResult = true;
            win.Close();
        }

    }
}

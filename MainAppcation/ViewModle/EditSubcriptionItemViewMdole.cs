using CommunityToolkit.Mvvm.Input;
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
        public SubcriptionItemViewModle subItemViewModle;
        public EditSubcriptionItemViewMdole(SubcriptionItemViewModle itemVm)
        {
            subItemViewModle= itemVm;
            subName = subItemViewModle.SubItem.SubcriptionName;
            subUrl = subItemViewModle.SubItem.Url;
            isAutoUpdate = subItemViewModle.SubItem.IsAutoUpdate;
            autoUpdateInterval = subItemViewModle.SubItem.AutoUpdateInterval;
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
            subItemViewModle.SubItem.SubcriptionName = subName;
            subItemViewModle.SubItem.Url = subUrl;
            subItemViewModle.SubItem.IsAutoUpdate = isAutoUpdate;
            subItemViewModle.SubItem.AutoUpdateInterval = autoUpdateInterval;
            subItemViewModle.SubItem.SaveToDataBase();
            subItemViewModle.UpdateData();
            win.DialogResult = true;
            win.Close();
        }

    }
}

using CommunityToolkit.Mvvm.Input;
using NetProxyController.Modle;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
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
            IsProxyUpdate = subItem.IsProxyUpdate;
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
        [RegularExpression(@"^(http|https):\/\/([\w\-]+(\.[\w\-]+)*\/)*([\w\-\.]+[^#?\s]+)(.*)?(#[\w\-]+)?$", ErrorMessage = "请输入正确的Url地址")]
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
        private bool isProxyUpdate;
        public bool IsProxyUpdate
        {
            get => isProxyUpdate;
            set
            {
                isProxyUpdate = value;
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
            subItem.IsProxyUpdate = isProxyUpdate;
            subItem.SaveToDataBase();
            win.DialogResult = true;
            win.Close();
        }
        protected override bool ValidationProperty([CallerMemberName] string? propertyName = null)
        {
            bool ret;
            if ((ret = base.ValidationProperty(propertyName)) && propertyName == nameof(SubName))
            {
                ret = !SubscriptionItem.SubscriptionItemDataList.Any(i => i.SubcriptionName == SubName && i.SubcriptionId != subItem.SubcriptionId);
                if (!ret)
                {
                    _Errors[propertyName!] = new() { new ValidationResult("名称有重复，请重新输入") };
                    OnErrorsChanged(propertyName!);
                }
            }
            return ret;

        }
    }
}

using XrayGUI.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayGUI.ViewModle
{
    internal class TrojanVerifyInfoViewModle: ViewModleBase
    {
        public TrojanInfo info;
        public TrojanVerifyInfoViewModle(TrojanInfo info)
        {
            this.info = info;
        }
        public TrojanVerifyInfoViewModle()
        {
            info = new();
        }
        [Required(ErrorMessage = "密码不能为空")]
        public string Password
        {
            get => info.Password;
            set
            {
                info.Password = value;
                OnPropertyChanged(nameof(Password));
                ValidationProperty();
            }
        }
        public string Email
        {
            get => info.Email;
            set
            {
                info.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
    }
}

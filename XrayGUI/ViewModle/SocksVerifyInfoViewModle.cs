using XrayGUI.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayGUI.ViewModle
{
    internal class SocksVerifyInfoViewModle : ViewModleBase
    {
        public SocksInfo info;
        public SocksVerifyInfoViewModle(SocksInfo info)
        {
            this.info = info;
        }
        public SocksVerifyInfoViewModle()
        {
            info = new();
        }
        public string User
        {
            get => info.User;
            set
            {
                info.User = value;
                OnPropertyChanged(nameof(User));
            }
        }
        public string PassWord
        {
            get => info.Pass;
            set
            {
                info.Pass = value;
                OnPropertyChanged(nameof(PassWord));
            }
        }
    }
}

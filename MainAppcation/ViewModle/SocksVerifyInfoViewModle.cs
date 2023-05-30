using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class SocksVerifyInfoViewModle : INotifyPropertyChanged
    {
        public SocksInfo info;
        public event PropertyChangedEventHandler? PropertyChanged;
        public SocksVerifyInfoViewModle(SocksInfo info)
        {
            this.info = info;
        }
        public SocksVerifyInfoViewModle()
        {
            info = new();
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

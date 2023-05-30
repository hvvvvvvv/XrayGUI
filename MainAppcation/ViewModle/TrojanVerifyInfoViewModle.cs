using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class TrojanVerifyInfoViewModle: INotifyPropertyChanged
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
        private void OnPropertyChanged(string proertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(proertyName)));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public string Password
        {
            get => info.Password;
            set
            {
                info.Password = value;
                OnPropertyChanged(nameof(Password));
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

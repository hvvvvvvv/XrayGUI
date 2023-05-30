using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class WebSocksSettingViewModle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private WebSocketInfo info;
        public WebSocksSettingViewModle(WebSocketInfo info)
        {
            this.info = info;
        }
        public WebSocksSettingViewModle()
        {
            info = new();
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string Path
        {
            get => info.Path;
            set
            {
                info.Path = value;
                OnPropertyChanged(nameof(Path));
            }
        }
    }
}

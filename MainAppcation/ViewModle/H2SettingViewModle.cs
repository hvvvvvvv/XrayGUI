using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class H2SettingViewModle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private H2Info info;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public H2SettingViewModle(H2Info info)
        {
            this.info = info;
        }
        public H2SettingViewModle()
        {
            info = new();
        }
        public string Hosts
        {
            get => info.Hosts;
            set
            {
                info.Hosts = value;
                OnPropertyChanged(nameof(Hosts));
            }
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
        public int ReadIdleTimeout
        { 
            get => info.ReadIdleTimeout; 
            set
            {
                info.ReadIdleTimeout = value;
                OnPropertyChanged(nameof(ReadIdleTimeout));
            }
        }
        public int HealthCheckTimeout
        {
            get => info.HealthCheckTimeout;
            set
            {
                info.HealthCheckTimeout = value;
                OnPropertyChanged(nameof(HealthCheckTimeout));
            }
        }
        public IEnumerable<HttpRequestType> MethodValues { get; private set; } = Enum.GetValues<HttpRequestType>().Cast<HttpRequestType>();
        public HttpRequestType MethodSelectedValue
        {
            get => info.Method;
            set
            {
                info.Method = value;
                OnPropertyChanged(nameof(MethodSelectedValue));
            }
        }
    }
}

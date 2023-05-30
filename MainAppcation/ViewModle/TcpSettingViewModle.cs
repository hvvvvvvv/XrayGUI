using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.EnterpriseData;
using NetProxyController.Modle.Server;
using NetProxyController.Modle;

namespace NetProxyController.ViewModle
{
    internal class TcpSettingViewModle : INotifyPropertyChanged
    {
        private TcpInfo info;
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TcpSettingViewModle(TcpInfo info)
        {
            this.info = info;
        }
        public TcpSettingViewModle()
        {
            info = new();
        }
        public bool AcceptProxyProtocol
        {
            get => info.AcceptProxyProtocol;
            set
            {
                info.AcceptProxyProtocol = value;
                OnPropertyChanged(nameof(AcceptProxyProtocol));
            }
        }
        public string Version
        {
            get => info.Version;
            set
            {
                info.Version = value;
                OnPropertyChanged(nameof(Version));
            }
        }
        public IEnumerable<HttpRequestType> MethodValues { get;private set; } = Enum.GetValues<HttpRequestType>().Cast<HttpRequestType>();
        public HttpRequestType MethodSelectedValue
        {
            get => info.Method;
            set
            {
                info.Method = value;
                OnPropertyChanged(nameof(MethodSelectedValue));
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
        public bool FeignEanbled
        {
            get => info.Feign == FeignType.http;
            set
            {
                info.Feign = value switch
                {
                    true => FeignType.http,
                    false => FeignType.none
                };
                OnPropertyChanged(nameof(FeignEanbled));
            }
        }
    }
}

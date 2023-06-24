using CommunityToolkit.Mvvm.Input;
using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using NetProxyController.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetProxyController.ViewModle
{
    internal class ServerItemViewModle : ViewModleBase
    {
        public ServerItem Server;
        public ServerItemViewModle(ServerItem server)
        {
            Server = server;
            UpdateData();
            doubleClickItemCmd = new RelayCommand(DoubleClickItemExcute);
        }
        public void UpdateData()
        {
            ServerName = Server.Remarks;
            Address = Server.Address;
            Port = Server.Port;
            ProxyProtocol = Server.Protocol;
            TransportProtocol = Server.GetStreamInfo().Transport;
            SecurityPolicy = Server.GetStreamInfo().Security;
        }
        private void SetProperty<T>(ref T property, T value, [CallerMemberName]string? propertyName = null)
        {
            property = value;
            OnPropertyChanged(propertyName);
        }
        private string serverName = default!;
        public string ServerName
        {
            get => serverName;
            set => SetProperty(ref serverName, value);
        }
        private string address = default!;
        public string Address
        {
            get => address;
            set => SetProperty(ref address, value);
        }
        private int port = default!;
        public int Port
        {
            get => port;
            set => SetProperty(ref port, value);
        }
        private OutboundProtocol proxyProtocol;
        public OutboundProtocol ProxyProtocol
        {
            get => proxyProtocol;
            set => SetProperty(ref proxyProtocol, value);
        }
        private TransportType transportProtocol;
        public TransportType TransportProtocol
        {
            get => transportProtocol;
            set => SetProperty(ref transportProtocol, value);
        }
        private TransportSecurity securityPolicy;
        public TransportSecurity SecurityPolicy
        {
            get => securityPolicy;
            set => SetProperty(ref securityPolicy, value);
        }
        private RelayCommand doubleClickItemCmd;
        public RelayCommand DoubleClickItemCmd
        {
            get => doubleClickItemCmd;
            set => _ = value;
        }
        private void DoubleClickItemExcute()
        {
            new ServerSettingWindow(Server).ShowDialog();
        }
    }
}

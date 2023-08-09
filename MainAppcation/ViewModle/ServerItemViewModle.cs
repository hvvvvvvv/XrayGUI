using CommunityToolkit.Mvvm.Input;
using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using NetProxyController.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
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
            doubleClickItemCmd = new RelayCommand(EditServerItem);
            netDelay = -1;
        }
        public ServerItemViewModle() : this(new())
        {

        }
        public void UpdateData()
        {
            ServerName = Server.Remarks;
            Address = Server.Address;
            Port = Server.Port;
            ProxyProtocol = Server.Protocol;
            TransportProtocol = Server.GetStreamInfo().Transport;
            SecurityPolicy = Server.GetStreamInfo().Security;
            IsActivated = Server.IsActivated;
            OnPropertyChanged(nameof(DefaultRoutingFlag));
            OnPropertyChanged(nameof(SubGroupName));
        }
        private void SetProperty<T>(ref T property, T value, [CallerMemberName]string? propertyName = null)
        {
            property = value;
            OnPropertyChanged(propertyName);
        }
        public int ProxyTestPort;
        private int netDelay;
        public int NetDelay
        {
            get => netDelay;
            set
            {
                netDelay = value; 
                OnPropertyChanged();
                OnPropertyChanged(nameof(TestDelayDisplay));
            }
        }
        public string TestDelayDisplay
        {
            get => netDelay switch
            {
                -1 => string.Empty,
                -2 => "测试中",
                -3 => "错误",
                -4 => "超时",
                _ => $"{netDelay}ms"
            };
            set => _ = value;
        }
        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set => isSelected = value;
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
        public string SubGroupName
        {
            get => SubscriptionItem.SubscriptionItemDataList.FirstOrDefault(i => i.SubcriptionId == Server.SubGroupId)?.SubcriptionName ?? "--";
        }
        private OutboundProtocol proxyProtocol;
        public OutboundProtocol ProxyProtocol
        {
            get => proxyProtocol;
            set => SetProperty(ref proxyProtocol, value);
        }
        private Modle.TransportType transportProtocol;
        public Modle.TransportType TransportProtocol
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
        public string DefaultRoutingFlag
        {
            get
            {
                if(Server.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex)
                {
                    return "=>";
                }
                return string.Empty;
            }
        }
        private bool isActivated;
        public bool IsActivated
        {
            get => Server.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex || isActivated;
            set => SetProperty(ref isActivated, value);
        }
        private RelayCommand doubleClickItemCmd;
        public RelayCommand DoubleClickItemCmd
        {
            get => doubleClickItemCmd;
            set => _ = value;
        }
        public void EditServerItem()
        {
            new ServerSettingWindow(Server).ShowDialog();
            UpdateData();
        }
        public void StartTestNetDelay()
        {
            NetDelay = -2;
            try
            {
                if (ProxyTestPort <= 0) throw new Exception();
                int timeOut = 10;
                using var cts = new CancellationTokenSource();
                cts.CancelAfter(TimeSpan.FromSeconds(timeOut));
                WebProxy webProxy = new(Global.LoopBcakAddress, ProxyTestPort);
                using var client = new HttpClient(new SocketsHttpHandler()
                {
                    Proxy = webProxy,
                    UseProxy = true
                });
                var counter = DateTime.Now.Ticks;
                client.GetAsync("https://www.google.com", cts.Token).Wait();
                var elapsed = (int)((DateTime.Now.Ticks - counter) / 10000);
                NetDelay = elapsed;
            }
            catch(AggregateException ex)
            {
                foreach (var innerEx in ex.Flatten().InnerExceptions)
                {
                    if(innerEx is TaskCanceledException)
                    {
                        NetDelay = -4;
                        return;
                    }
                }
                NetDelay = -3;
            }
        }
    }
}

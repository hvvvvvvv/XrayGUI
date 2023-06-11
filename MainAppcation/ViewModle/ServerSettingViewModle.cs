using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NetProxyController.View;
using NetProxyController.Modle;
using SQLite;
using NetProxyController.Modle.Server;

namespace NetProxyController.ViewModle
{
    internal class ServerSettingViewModle: INotifyPropertyChanged
    {
        private Dictionary<OutboundProtocol, UserControl> VerifyInfoView;
        private Dictionary<TransportType, UserControl> transportSettingView;
        private Dictionary<TransportSecurity, UserControl?> securitySettingView;
        private ServerItem Server;
        private StreamInfo StreaminfoObj;
        public ServerSettingViewModle()
        {
            Server = new();
            StreaminfoObj = new();
            VerifyInfoView = new()
            {
                {OutboundProtocol.trojan, new TrojanVerifyInfo() },
                {OutboundProtocol.vmess, new VmessVerifyInfo() },
                {OutboundProtocol.vless, new VlessVerifyInfo() },
                {OutboundProtocol.shadowsocks, new ShadowScoksVerifyInfo() },
                {OutboundProtocol.socks, new SocksVerifyInfo() }
            };
            transportSettingView = new()
            {
                {TransportType.tcp, new TcpSetting()},
                {TransportType.kcp, new KcpSetting()},
                {TransportType.quic, new QuicSetting()},
                {TransportType.http, new H2Setting()},
                {TransportType.grpc, new GrpcSetting()},
                {TransportType.ws, new WebSocketSetting()}
            };
            securitySettingView = new()
            {
                {TransportSecurity.tls, new TlsSetting()},
                {TransportSecurity.xtls, new TlsSetting()},
                {TransportSecurity.reality, new RealitySetting()},
                {TransportSecurity.none, null}
            };

        }
        public IEnumerable<OutboundProtocol> ProxyProtocolValues { get; private set; } = Enum.GetValues(typeof(OutboundProtocol)).Cast<OutboundProtocol>();
        public IEnumerable<TransportType> TransportProtocolValues {get; private set; } = Enum.GetValues<TransportType>().Cast<TransportType>();
        private TransportType transportProtocolSelectedValue;
        public TransportType TransportProtocolSelectedValue
        {
            get => transportProtocolSelectedValue;
            set
            {
                transportProtocolSelectedValue = value;
                OnpropertyChannged(nameof(TransportProtocolSelectedValue));
                OnpropertyChannged(nameof(TransportSettingView));
            }
        }

        public IEnumerable<TransportSecurity> SecurityValues { get; private set; } = Enum.GetValues<TransportSecurity>().Cast<TransportSecurity>();
        private TransportSecurity securitySelectedValue;
        public TransportSecurity SecuritySelectedValue
        {
            get => securitySelectedValue;
            set
            {
                securitySelectedValue = value;
                OnpropertyChannged(nameof(SecuritySelectedValue));
                OnpropertyChannged(nameof(SecuritySettingView));
            }
        }
        public UserControl? ProxyUserSettingView
        {
            get => VerifyInfoView[selectedProtocol];
        }
        public UserControl? TransportSettingView
        {
            get => transportSettingView[TransportProtocolSelectedValue];
        }
        public UserControl? SecuritySettingView
        {
            get => securitySettingView[securitySelectedValue];
        }
        private OutboundProtocol selectedProtocol;
        public OutboundProtocol SelectedProtocol
        {
            get => selectedProtocol;
            set
            {
                selectedProtocol = value;
                OnpropertyChannged(nameof(SelectedProtocol));
                OnpropertyChannged(nameof(ProxyUserSettingView));
            }
        }
        public string Addr
        {
            get => Server.Address;
            set
            {
                Server.Address = value;
                OnpropertyChannged(nameof(Addr));
            }
        }
        public string Remarks
        {
            get => Server.Remarks;
            set
            {
                Server.Remarks = value;
                OnpropertyChannged(nameof(Remarks));
            }
        }

        private string portStr = string.Empty;
        public string PortStr
        {
            get => portStr;
            set
            {
                portStr = value;
                OnpropertyChannged(nameof(PortStr));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnpropertyChannged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

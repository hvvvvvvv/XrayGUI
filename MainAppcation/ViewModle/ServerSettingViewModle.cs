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
        public ServerSettingViewModle(ServerItem server)
        {
            Server = server;
            StreaminfoObj = Server.GetStreamInfo();
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
                {TransportType.tcp, new TcpSetting(){ DataContext = new TcpSettingViewModle(StreaminfoObj.TcpTransport) }},
                {TransportType.kcp, new KcpSetting(){ DataContext = new KcpSettingViewModle(StreaminfoObj.KcpTransport) }},
                {TransportType.quic, new QuicSetting(){ DataContext = new QuicSettingViewModle(StreaminfoObj.QuicTransport) }},
                {TransportType.http, new H2Setting(){ DataContext = new H2SettingViewModle(StreaminfoObj.H2Transport) }},
                {TransportType.grpc, new GrpcSetting(){ DataContext = new GrpcSettingViewModle(StreaminfoObj.GrpcTranport) }},
                {TransportType.ws, new WebSocketSetting(){ DataContext = new WebSocksSettingViewModle(StreaminfoObj.WsTransport) }}
            };
            securitySettingView = new()
            {
                {TransportSecurity.tls, new TlsSetting(){ DataContext = new TlsSettingViewModle(StreaminfoObj.TlsPolicy)}},
                {TransportSecurity.xtls, new TlsSetting(){ DataContext = new TlsSettingViewModle(StreaminfoObj.XTlsPolicy) }},
                {TransportSecurity.reality, new RealitySetting(){ DataContext = new RealityInfoSettingViewModle(StreaminfoObj.RealityPolicy) }},
                {TransportSecurity.none, null}
            };
        }
        public ServerSettingViewModle() : this(new ServerItem())
        {

        }
        public IEnumerable<OutboundProtocol> ProxyProtocolValues { get; private set; } = Enum.GetValues(typeof(OutboundProtocol)).Cast<OutboundProtocol>();
        public IEnumerable<TransportType> TransportProtocolValues {get; private set; } = Enum.GetValues<TransportType>().Cast<TransportType>();
        public TransportType TransportProtocolSelectedValue
        {
            get => StreaminfoObj.Transport;
            set
            {
                StreaminfoObj.Transport = value;
                OnpropertyChannged(nameof(TransportProtocolSelectedValue));
                OnpropertyChannged(nameof(TransportSettingView));
            }
        }

        public IEnumerable<TransportSecurity> SecurityValues { get; private set; } = Enum.GetValues<TransportSecurity>().Cast<TransportSecurity>();
        public TransportSecurity SecuritySelectedValue
        {
            get => StreaminfoObj.Security;
            set
            {
                StreaminfoObj.Security = value;
                OnpropertyChannged(nameof(SecuritySelectedValue));
                OnpropertyChannged(nameof(SecuritySettingView));
            }
        }
        public UserControl? ProxyUserSettingView
        {
            get => VerifyInfoView[SelectedProtocol];
        }
        public UserControl? TransportSettingView
        {
            get => transportSettingView[TransportProtocolSelectedValue];
        }
        public UserControl? SecuritySettingView
        {
            get => securitySettingView[SecuritySelectedValue];
        }
        public OutboundProtocol SelectedProtocol
        {
            get => Server.Protocol;
            set
            {
                Server.Protocol = value;
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

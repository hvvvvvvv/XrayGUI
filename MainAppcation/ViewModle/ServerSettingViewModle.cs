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
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.Networking.Sockets;
using System.Collections;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace NetProxyController.ViewModle
{
    internal class ServerSettingViewModle: ViewModleBase
    {
        private Dictionary<OutboundProtocol, UserControl> VerifyInfoView;
        private Dictionary<OutboundProtocol, OutBoundConfiguration> ProtocolModles;
        private Dictionary<TransportType, UserControl> transportSettingView;
        private Dictionary<TransportSecurity, UserControl?> securitySettingView;
        private ServerItem Server;
        private StreamInfo StreaminfoObj;
        public ServerSettingViewModle(ServerItem server)
        {
            Server = server;
            StreaminfoObj = Server.GetStreamInfo();
            VerifyInfoView = new();
            ProtocolModles = new();
            transportSettingView = new();
            securitySettingView = new();
            SaveBtnCmd = new(SaveBtn);
            InitData();
        }
        public ServerSettingViewModle() : this(new ServerItem())
        {

        }
        private void InitData()
        {
            #region 代理协议验证信息
            VlessInfo vless = Server.Protocol == OutboundProtocol.vless ? Server.GetProtocolInfoObj() as VlessInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.vless, new VlessVerifyInfo() { DataContext = new VlessVerifyInfoViewModle(vless) });
            ProtocolModles.Add(OutboundProtocol.vless, vless);
            VmessInfo vmess = Server.Protocol == OutboundProtocol.vmess ? Server.GetProtocolInfoObj() as VmessInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.vmess, new VmessVerifyInfo() { DataContext = new VmessVeridyInfoViewModle(vmess) });
            ProtocolModles.Add(OutboundProtocol.vmess, vmess);
            ShadowSocksInfo shadowSocks = Server.Protocol == OutboundProtocol.shadowsocks ? Server.GetProtocolInfoObj() as ShadowSocksInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.shadowsocks, new ShadowScoksVerifyInfo() { DataContext = new ShadowSocksVerifyInfoViewModle(shadowSocks) });
            ProtocolModles.Add(OutboundProtocol.shadowsocks, shadowSocks);
            TrojanInfo trojan = Server.Protocol == OutboundProtocol.trojan ? Server.GetProtocolInfoObj() as TrojanInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.trojan, new TrojanVerifyInfo() { DataContext = new TrojanVerifyInfoViewModle(trojan) });
            ProtocolModles.Add(OutboundProtocol.trojan, trojan);
            SocksInfo socks = Server.Protocol == OutboundProtocol.socks ? Server.GetProtocolInfoObj() as SocksInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.socks, new SocksVerifyInfo() { DataContext = new SocksVerifyInfoViewModle(socks) });
            ProtocolModles.Add(OutboundProtocol.socks, socks);
            #endregion

            #region 传输协议
            transportSettingView.Add(TransportType.tcp, new TcpSetting() { DataContext = new TcpSettingViewModle(StreaminfoObj.TcpTransport) });
            transportSettingView.Add(TransportType.kcp, new KcpSetting() { DataContext = new KcpSettingViewModle(StreaminfoObj.KcpTransport) });
            transportSettingView.Add(TransportType.quic, new QuicSetting() { DataContext = new QuicSettingViewModle(StreaminfoObj.QuicTransport) });
            transportSettingView.Add(TransportType.http, new H2Setting() { DataContext = new H2SettingViewModle(StreaminfoObj.H2Transport) });
            transportSettingView.Add(TransportType.grpc, new GrpcSetting() { DataContext = new GrpcSettingViewModle(StreaminfoObj.GrpcTranport) });
            transportSettingView.Add(TransportType.ws, new WebSocketSetting() { DataContext = new WebSocksSettingViewModle(StreaminfoObj.WsTransport) });
            #endregion
            #region 加密方案
            securitySettingView.Add(TransportSecurity.tls, new TlsSetting() { DataContext = new TlsSettingViewModle(StreaminfoObj.TlsPolicy) });
            securitySettingView.Add(TransportSecurity.xtls, new TlsSetting() { DataContext = new TlsSettingViewModle(StreaminfoObj.XTlsPolicy) });
            securitySettingView.Add(TransportSecurity.reality, new RealitySetting() { DataContext = new RealityInfoSettingViewModle(StreaminfoObj.RealityPolicy) });
            securitySettingView.Add(TransportSecurity.none, null);
            #endregion
        }
        private void SaveBtn()
        {
            Server.SetProtocolInfoObj(ProtocolModles[Server.Protocol]);
            Server.SetStreamInfo(StreaminfoObj);
            Server.SaveToDataBase();
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
        public RelayCommand SaveBtnCmd { get; set; }
        [Required(ErrorMessage = "服务器地址不能为空")]
        public string Addr
        {
            get => Server.Address;
            set
            {
                Server.Address = value;
                OnpropertyChannged(nameof(Addr));
                ValidationProperty();
            }
        }
       [Required(ErrorMessage = "名称不能为空")]
        public string Remarks
        {
            get => Server.Remarks;
            set
            {
                Server.Remarks = value;
                OnpropertyChannged();
                ValidationProperty();
            }
        }
        
        private string portStr = string.Empty;
        [Required(ErrorMessage = "端口号不能为空")]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$",ErrorMessage = "请输入正确的端口号")]
        public string PortStr
        {
            get => portStr;
            set
            {
                portStr = value;
                OnpropertyChannged(nameof(PortStr));
                if(ValidationProperty())
                {
                    Server.Port = Convert.ToInt32(PortStr);
                }
            }
        } 
    }
}

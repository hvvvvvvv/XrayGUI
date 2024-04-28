using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using XrayGUI.View;
using XrayGUI.Modle;
using SQLite;
using XrayGUI.Modle.Server;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.Networking.Sockets;
using System.Collections;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using XrayGUI.Handler;
using Windows.Networking.NetworkOperators;
using Windows.ApplicationModel.Search.Core;

namespace XrayGUI.ViewModle
{
    internal class ServerSettingViewModle: ViewModleBase
    {
        private Dictionary<OutboundProtocol, UserControl?> VerifyInfoView;
        private Dictionary<TransportType, UserControl?> transportSettingView;
        private Dictionary<TransportSecurity, UserControl?> securitySettingView;
        private ServerItem Server;
        private StreamInfo StreaminfoObj;
        public ServerSettingViewModle(ServerItem server)
        {
            Server = server;
            StreaminfoObj = Server.GetStreamInfo();
            VerifyInfoView = new();
            transportSettingView = new();
            securitySettingView = new();
            SaveBtnCmd = new(SaveBtn!);
            portStr = Server.Port.ToString();
            addr = Server.Address;
            remarks = Server.Remarks;
            selectedProtocol = Server.Protocol;
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
            VmessInfo vmess = Server.Protocol == OutboundProtocol.vmess ? Server.GetProtocolInfoObj() as VmessInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.vmess, new VmessVerifyInfo() { DataContext = new VmessVeridyInfoViewModle(vmess) });
            ShadowSocksInfo shadowSocks = Server.Protocol == OutboundProtocol.shadowsocks ? Server.GetProtocolInfoObj() as ShadowSocksInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.shadowsocks, new ShadowScoksVerifyInfo() { DataContext = new ShadowSocksVerifyInfoViewModle(shadowSocks) });
            TrojanInfo trojan = Server.Protocol == OutboundProtocol.trojan ? Server.GetProtocolInfoObj() as TrojanInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.trojan, new TrojanVerifyInfo() { DataContext = new TrojanVerifyInfoViewModle(trojan) });
            SocksInfo socks = Server.Protocol == OutboundProtocol.socks ? Server.GetProtocolInfoObj() as SocksInfo ?? new() : new();
            VerifyInfoView.Add(OutboundProtocol.socks, new SocksVerifyInfo() { DataContext = new SocksVerifyInfoViewModle(socks) });
            VerifyInfoView.Add(OutboundProtocol.freedom, null);
            #endregion

            #region 传输协议
            transportSettingView.Add(TransportType.tcp, new TcpSetting() { DataContext = new TcpSettingViewModle(StreaminfoObj.TcpTransport) });
            transportSettingView.Add(TransportType.kcp, new KcpSetting() { DataContext = new KcpSettingViewModle(StreaminfoObj.KcpTransport) });
            transportSettingView.Add(TransportType.quic, new QuicSetting() { DataContext = new QuicSettingViewModle(StreaminfoObj.QuicTransport) });
            transportSettingView.Add(TransportType.http, new H2Setting() { DataContext = new H2SettingViewModle(StreaminfoObj.H2Transport) });
            transportSettingView.Add(TransportType.grpc, new GrpcSetting() { DataContext = new GrpcSettingViewModle(StreaminfoObj.GrpcTranport) });
            transportSettingView.Add(TransportType.ws, new WebSocketSetting() { DataContext = new WebSocksSettingViewModle(StreaminfoObj.WsTransport) });
            transportSettingView.Add(TransportType.none, null);
            #endregion
            #region 加密方案
            securitySettingView.Add(TransportSecurity.tls, new TlsSetting() { DataContext = new TlsSettingViewModle(StreaminfoObj.TlsPolicy) });
            securitySettingView.Add(TransportSecurity.xtls, new TlsSetting() { DataContext = new TlsSettingViewModle(StreaminfoObj.XTlsPolicy) });
            securitySettingView.Add(TransportSecurity.reality, new RealitySetting() { DataContext = new RealityInfoSettingViewModle(StreaminfoObj.RealityPolicy) });
            securitySettingView.Add(TransportSecurity.none, null);
            #endregion
        }
        private bool ValidationData()
        {
            var viewModles = new List<ViewModleBase?>()
            {
                this,
                ProxyUserSettingView?.DataContext is ViewModleBase vm1 ? vm1 : null,
                TransportSettingView?.DataContext is ViewModleBase vm2 ? vm2 : null,
                SecuritySettingView?.DataContext is ViewModleBase vm3 ? vm3 : null,
            };
            foreach(var vm in viewModles)
            {
                if(vm is not null)
                {
                    if(!vm.ValidationAllProperty())
                    {
                        return false;
                    }
                }
            }
            return true;               
        }
        private void SaveBtn(Window win)
        {
            if(!ValidationData())
            {
                return;
            }
            Server.Address = Addr;
            Server.Remarks = remarks;
            Server.Port = IsNotCheckedFreedom ? int.Parse(PortStr) : 0;
            Server.Protocol = selectedProtocol;
            Server.SetStreamInfo(StreaminfoObj);
            OutBoundConfiguration? ProtocolInfoObj = selectedProtocol switch
            {
                OutboundProtocol.vless => (VerifyInfoView[OutboundProtocol.vless]!.DataContext as VlessVerifyInfoViewModle)!.info,
                OutboundProtocol.vmess => (VerifyInfoView[OutboundProtocol.vmess]!.DataContext as VmessVeridyInfoViewModle)!.Info,
                OutboundProtocol.shadowsocks => (VerifyInfoView[OutboundProtocol.shadowsocks]!.DataContext as ShadowSocksVerifyInfoViewModle)!.info,
                OutboundProtocol.trojan => (VerifyInfoView[OutboundProtocol.trojan]!.DataContext as TrojanVerifyInfoViewModle)!.info,
                OutboundProtocol.socks => (VerifyInfoView[OutboundProtocol.socks]!.DataContext as SocksVerifyInfoViewModle)!.info,
                _ => null
            };
            Server.SetProtocolInfoObj(ProtocolInfoObj);
            
            Server.SaveToDataBase();
            XrayHanler.Instance.ReloadConfig();
            win.DialogResult = true;
            win.Close();
        }
        public IEnumerable<OutboundProtocol> ProxyProtocolValues { get; private set; } = Enum.GetValues(typeof(OutboundProtocol)).Cast<OutboundProtocol>();
        public IEnumerable<TransportType> TransportProtocolValues
        {
            get
            {
                var ret = SelectedProtocol switch
                {
                    OutboundProtocol.freedom => Enum.GetValues<TransportType>().Cast<TransportType>().Where(i => i == TransportType.none),
                    _ => Enum.GetValues<TransportType>().Cast<TransportType>().Where(i => i != TransportType.none)
                };
                TransportProtocolSelectedValue = ret.Contains(TransportProtocolSelectedValue) ? TransportProtocolSelectedValue : ret.FirstOrDefault();
                return ret;
            }
        }
        public TransportType TransportProtocolSelectedValue
        {
            get => StreaminfoObj.Transport;
            set
            {
                StreaminfoObj.Transport = value;
                OnPropertyChanged(nameof(TransportProtocolSelectedValue));
                OnPropertyChanged(nameof(TransportSettingView));
            }
        }

        public IEnumerable<TransportSecurity> SecurityValues
        {
            get
            {
                var ret = Server.Protocol switch
                {
                    OutboundProtocol.freedom => Enum.GetValues<TransportSecurity>().Cast<TransportSecurity>().Where(i => i == TransportSecurity.none),
                    _ => Enum.GetValues<TransportSecurity>().Cast<TransportSecurity>()
                };
                SecuritySelectedValue = ret.Contains(SecuritySelectedValue) ? SecuritySelectedValue : ret.FirstOrDefault();
                return ret;                   
            }
        }
            
        public TransportSecurity SecuritySelectedValue
        {
            get => StreaminfoObj.Security;
            set
            {
                StreaminfoObj.Security = value;
                OnPropertyChanged(nameof(SecuritySelectedValue));
                OnPropertyChanged(nameof(SecuritySettingView));
            }
        }
        public UserControl? ProxyUserSettingView => VerifyInfoView[SelectedProtocol];
        public UserControl? TransportSettingView
        {
            get => transportSettingView[TransportProtocolSelectedValue];
        }
        public UserControl? SecuritySettingView
        {
            get => securitySettingView[SecuritySelectedValue];
        }
        private OutboundProtocol selectedProtocol;
        public OutboundProtocol SelectedProtocol
        {
            get => selectedProtocol;
            set
            {
                selectedProtocol = value;
                OnPropertyChanged(nameof(SelectedProtocol));
                OnPropertyChanged(nameof(ProxyUserSettingView));
                OnPropertyChanged(nameof(IsNotCheckedFreedom));
                OnPropertyChanged(nameof(SecurityValues));
                OnPropertyChanged(nameof(TransportProtocolValues));
                if(value == OutboundProtocol.freedom)
                {
                    ClearErrors(nameof(PortStr));
                    ClearErrors(nameof(Addr));
                }
            }
        }
        public RelayCommand<Window> SaveBtnCmd { get; set; }
        [Required(ErrorMessage = "服务器地址不能为空")]
        private string addr;
        public string Addr
        {
            get => addr;
            set
            {
                addr = value;
                OnPropertyChanged(nameof(Addr));
                ValidationProperty();
            }
        }
       [Required(ErrorMessage = "名称不能为空")]
       private string remarks;
        public string Remarks
        {
            get => remarks;
            set
            {
                remarks = value;
                OnPropertyChanged();
                ValidationProperty();
            }
        }

        private string portStr;
        [Required(ErrorMessage = "端口号不能为空")]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$",ErrorMessage = "请输入正确的端口号(1-65535)")]
        public string PortStr
        {
            get => portStr;
            set
            {
                portStr = value;
                OnPropertyChanged(nameof(PortStr));
                ValidationProperty();
            }
        }
        public bool IsNotCheckedFreedom => SelectedProtocol != OutboundProtocol.freedom;

        protected override bool ValidationProperty([CallerMemberName] string? propertyName = null)
        {
            if(Server.Protocol == OutboundProtocol.freedom && (propertyName == nameof(PortStr) || propertyName == nameof(Addr)))
            {
                return true;
            }
            return base.ValidationProperty(propertyName);
        }
    }
}

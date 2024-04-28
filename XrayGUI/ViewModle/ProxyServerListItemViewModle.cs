using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using XrayGUI.Modle;

namespace XrayGUI.ViewModle
{
    public class ProxyServerListItemViewModle : ViewModleBase
    {
        private OutboundServerItem serverData;
        private bool isPropertyChanged = false;
        public ProxyServerListItemViewModle(OutboundServerItem serverData)
        {
            this.serverData = serverData;
            LoadData();
        }
        public ProxyServerListItemViewModle()
        {
            serverData = new OutboundServerItem();
            LoadData();
        }
        private void LoadData()
        {
            remarks = serverData.Remarks ?? string.Empty;
            groupId = serverData.GroupId;
            address = serverData.Address ?? string.Empty;
            port = serverData.Port == 0 ? string.Empty : serverData.Port.ToString();
            protocol = ProxyProtocols.FirstOrDefault(x => x == serverData.Protocol) ?? ProxyProtocols[0];
            tlsEnbled = serverData.TlsEnbled ?? false;
            uotEnbled = serverData.UdpOuverTcpEnbled ?? false;
            uotVision = UotVisions.FirstOrDefault(x => x == (serverData.UotVision ?? default));
            uotVision = uotVision == default ? UotVisions[0] : uotVision;
            serverName = serverData.ServerName ?? string.Empty;
            alpn = serverData.Alpn ?? string.Empty;
            allowInsecure = serverData.AllowInsecure ?? false;
            utlsEnabled = serverData.UtlsEnbled ?? false;
            utlsFingerPrint = FingerPrinters.FirstOrDefault(x => x == serverData.UtlsFingerPrint) ?? FingerPrinters[0];
            realityEnbled = serverData.RealityEnbled ?? false;
            shortId = serverData.ShortId ?? string.Empty;
            publicKey = serverData.PublicKey ?? string.Empty;
            v2RayTransportEnbled = serverData.V2RayTransportEnbled;
            transportType = TransportTypes.FirstOrDefault(x => x == serverData.TransportType) ?? TransportTypes[0];
            transportHost = serverData.TransportHost ?? string.Empty;
            transportPath = serverData.TransportPath ?? string.Empty;
            socksUsername = serverData.SocksUsername ?? string.Empty;
            socksPassword = serverData.SocksPassword ?? string.Empty;
            socksVision = SocksVisions.FirstOrDefault(x => x == serverData.SocksVision) ?? SocksVisions[0];
            ssMethod = SSMethods.FirstOrDefault(x => x == serverData.SSMethod) ?? SSMethods[0];
            ssPassword = serverData.SSPassword ?? string.Empty;
            ssPlugin = serverData.SSPlugin ?? string.Empty;
            ssPluginOpts = serverData.SSPluginOpts ?? string.Empty;
            vmessId = serverData.VmessId ?? string.Empty;
            vmessAlterId = serverData.VmessAlterId.ToString();
            vmessSecurity = VMessSecurities.FirstOrDefault(x => x == serverData.VmessSecurity) ?? VMessSecurities[0];
            trojanPassword = serverData.TrojanPassword ?? string.Empty;
            vlessId = serverData.VlessId ?? string.Empty;
            vlessFlow = SSMethods.FirstOrDefault(x => x == serverData.VlessFlow) ?? SSMethods[0];
        }
        private void RefreshViewData()
        {
            foreach (var property in GetType().GetProperties())
            {
                if (property.Name == nameof(HasErrors)) continue;
                OnPropertyChanged(property.Name);
            }
        }
        public void ReloadData()
        {
            LoadData();
            RefreshViewData();
        }
        public void SaveData()
        {
            if (isPropertyChanged)
            {
                serverData.UpdatedTime = DateTimeOffset.Now.Ticks;
                serverData.Save();
                isPropertyChanged = false;
            }
        }      
        public bool IsChanged() => isPropertyChanged || serverData.Identity == Guid.Empty;
        private bool ValidateData()
        {
            List<(string errProperty, string errMsg)> errs = new();
                        
            ClearAllErrors();
            if(string.IsNullOrWhiteSpace(remarks))
            {
                errs.Add((nameof(Remarks), "别名不能为空"));
            }
            if(string.IsNullOrWhiteSpace(address))
            {
                errs.Add((nameof(Address), "地址不能为空"));
            }
            if(string.IsNullOrWhiteSpace(port))
            {
                errs.Add((nameof(Port), "端口号不能为空"));
            }
            else if(!uint.TryParse(port, out uint port_ ) || port_ == 0)
            {
                errs.Add((nameof(Port), "请输入整数1-65535"));
            }
            void ValidateReality()
            {
                if(tlsEnbled && realityEnbled)
                {
                    if(string.IsNullOrWhiteSpace(shortId))
                    {
                        errs.Add((nameof(ShortId), "ShortId不能为空"));
                    }
                    if(string.IsNullOrWhiteSpace(publicKey))
                    {
                        errs.Add((nameof(PublicKey), "PublicKey不能为空"));
                    }
                    if(!utlsEnabled)
                    {
                        errs.Add((nameof(UtlsPrintEnabled), "Reality启用时Utls必须启用"));
                    }
                }
              
            }
            switch(protocol)
            {
                case Modle.SingboxOptions.OutboundTypes.Shadowsocks:
                    if(string.IsNullOrWhiteSpace(ssPassword))
                    {
                        errs.Add((nameof(SSPassword), "密码不能为空"));
                    }
                    break;
                case Modle.SingboxOptions.OutboundTypes.VMss:
                    if(string.IsNullOrWhiteSpace(vmessId))
                    {
                        errs.Add((nameof(VmessId), "用户ID不能为空"));
                    }
                    if(string.IsNullOrWhiteSpace(vmessAlterId))
                    {
                        errs.Add((nameof(VmessAlterId), "AlterId不能为空"));
                    }
                    else if(!int.TryParse(vmessAlterId, out _))
                    {
                        errs.Add((nameof(VmessAlterId), "请输入整数"));
                    }
                    ValidateReality();
                    break;
                case Modle.SingboxOptions.OutboundTypes.Trojan:
                    if(string.IsNullOrWhiteSpace(trojanPassword))
                    {
                        errs.Add((nameof(TrojanPassword), "密码不能为空"));
                    }
                    ValidateReality();
                    break;
                case Modle.SingboxOptions.OutboundTypes.VLESS:
                    if(string.IsNullOrWhiteSpace(vlessId))
                    {
                        errs.Add((nameof(VlessId), "用户ID不能为空"));
                    }
                    ValidateReality();
                    break;
            }
            errs.ForEach(err => PropertyError(err.errMsg, err.errProperty));
            if(errs.Count> 0)
            {
                SnackBarMsg.Clear();
                SnackBarMsg.Enqueue(errs[0].errMsg);
            }
            return errs.Count == 0;
        }
        private void SetServerData()
        {
            serverData.Remarks = remarks;
            serverData.GroupId = groupId;
            serverData.Address = address;
            serverData.Port = uint.Parse(port);            
            serverData.Protocol = protocol;
            switch (protocol)
            {
                case Modle.SingboxOptions.OutboundTypes.Shadowsocks:
                    serverData.SSMethod = ssMethod;
                    serverData.SSPassword = ssPassword;
                    serverData.SSPlugin = ssPlugin;
                    serverData.SSPluginOpts = ssPluginOpts;
                    serverData.UdpOuverTcpEnbled = uotEnbled;
                    serverData.UotVision = uotVision;
                    serverData.TlsEnbled = null;
                    serverData.V2RayTransportEnbled = false;
                    break;
                case Modle.SingboxOptions.OutboundTypes.VMss:
                    serverData.VmessId = vmessId;
                    serverData.VmessAlterId = int.Parse(vmessAlterId);
                    serverData.VmessSecurity = vmessSecurity;
                    SetTlsProperties();
                    SetV2RayTransportProperties();
                    break;
                case Modle.SingboxOptions.OutboundTypes.Trojan:
                    serverData.TrojanPassword = trojanPassword;
                    SetTlsProperties();
                    SetV2RayTransportProperties();
                    break;
                case Modle.SingboxOptions.OutboundTypes.VLESS:
                    serverData.VlessId = vlessId;
                    serverData.VlessFlow = vlessFlow;
                    SetTlsProperties();
                    SetV2RayTransportProperties();
                    break;
                case Modle.SingboxOptions.OutboundTypes.Socks:
                    serverData.SocksUsername = socksUsername;
                    serverData.SocksPassword = socksPassword;
                    serverData.SocksVision = socksVision;
                    serverData.UdpOuverTcpEnbled = uotEnbled;
                    serverData.UotVision = uotVision;
                    serverData.TlsEnbled = null;
                    serverData.V2RayTransportEnbled = false;
                    break;
            }
            isPropertyChanged = true;
        }
        private void SetTlsProperties()
        {
            serverData.TlsEnbled = tlsEnbled;
            if(tlsEnbled)
            {
                serverData.ServerName = serverName;
                serverData.Alpn = alpn;
                serverData.AllowInsecure = allowInsecure;
                serverData.UtlsEnbled = utlsEnabled;
                if(utlsEnabled)
                {
                    serverData.UtlsFingerPrint = utlsFingerPrint;
                }
                serverData.RealityEnbled = realityEnbled;
                if(realityEnbled)
                {
                    serverData.ShortId = shortId;
                    serverData.PublicKey = publicKey;
                }
            }
        }
        private void SetV2RayTransportProperties()
        {
            serverData.V2RayTransportEnbled = v2RayTransportEnbled;
            if(v2RayTransportEnbled)
            {
                serverData.TransportType = transportType;
                serverData.TransportHost = transportHost;
                serverData.TransportPath = transportPath;
            }
        }
        protected override void SetProerty<T>(ref T property, T value, [CallerMemberName] string? propertyName = null)
        {
            if (property?.Equals(value) ?? false) return;
            base.SetProerty(ref property, value, propertyName);
            ClearErrors(propertyName);
        }
        public RelayCommand<Window> ConfirmBtnCmd => new((win) =>
        {
            if(ValidateData() && win is not null)
            {
                SetServerData();
                win.DialogResult = IsChanged();
                win.Close();
            }            
        });
        public RelayCommand<Window> CancelBtnCmd => new((win) =>
        {
            if(win is not null)
            {
                win.DialogResult = false;
                win.Close();
            }
        });
        private string remarks = string.Empty;
        public string Remarks
        {
            get => remarks;
            set => SetProerty(ref remarks, value);
        }
        private Guid groupId;
        public Guid GroupId
        {
            get => groupId;
            set => SetProerty(ref groupId, value);
        }
        public string GroupName { get; set; } = "默认分组";
        private string address = string.Empty;
        public string Address
        {
            get => address;
            set => SetProerty(ref address, value);
        }
        private string port = string.Empty;
        public string Port
        {
            get => port;
            set => SetProerty(ref port, value);
        }
        private string protocol = string.Empty;
        public string Protocol
        {
            get => protocol;
            set => SetProerty(ref protocol, value);
        }
        private bool tlsEnbled;
        public bool TlsEnbled
        {
            get => tlsEnbled;
            set => SetProerty(ref tlsEnbled, value);
        }
        private bool uotEnbled;
        public bool UotEnbled
        {
            get => uotEnbled;
            set => SetProerty(ref uotEnbled, value);
        }
        private byte uotVision;
        public byte UotVision
        {
            get => uotVision;
            set => SetProerty(ref uotVision, value);
        }
        private string serverName = string.Empty;
        public string ServerName
        {
            get => serverName;
            set => SetProerty(ref serverName, value);
        }
        private string alpn = string.Empty;
        public string Alpn
        {
            get => alpn;
            set => SetProerty(ref alpn, value);
        }
        private bool allowInsecure;
        public bool AllowInsecure
        {
            get => allowInsecure;
            set => SetProerty(ref allowInsecure, value);
        }
        private bool utlsEnabled;
        public bool UtlsPrintEnabled
        {
            get => utlsEnabled;
            set => SetProerty(ref utlsEnabled, value);
        }
        private string utlsFingerPrint = string.Empty;
        public string UtlsFingerPrint
        {
            get => utlsFingerPrint;
            set => SetProerty(ref utlsFingerPrint, value);
        }
        private bool realityEnbled;
        public bool RealityEnbled
        {
            get => realityEnbled;
            set
            {
                SetProerty(ref realityEnbled, value);
                if(value == false) ClearErrors(nameof(UtlsPrintEnabled));
            }
        }
        private string shortId = string.Empty;
        public string ShortId
        {
            get => shortId;
            set => SetProerty(ref shortId, value);
        }
        private string publicKey = string.Empty;
        public string PublicKey
        {
            get => publicKey;
            set => SetProerty(ref publicKey, value);
        }
        private bool v2RayTransportEnbled;
        public bool V2RayTransportEnbled
        {
            get => v2RayTransportEnbled;
            set => SetProerty(ref v2RayTransportEnbled, value);
        }
        private string transportType = string.Empty;
        public string TransportType
        {
            get => transportType;
            set => SetProerty(ref transportType, value);
        }
        private string transportHost = string.Empty;
        public string TransportHost
        {
            get => transportHost;
            set => SetProerty(ref transportHost, value);
        }
        private string transportPath = string.Empty;
        public string TransportPath
        {
            get => transportPath;
            set => SetProerty(ref transportPath, value);
        }
        private string socksUsername = string.Empty;
        public string SocksUsername
        {
            get => socksUsername;
            set => SetProerty(ref socksUsername, value);
        }
        private string socksPassword = string.Empty;
        public string SocksPassword
        {
            get => socksPassword;
            set => SetProerty(ref socksPassword, value);
        }
        private string socksVision = string.Empty;
        public string SocksVision
        {
            get => socksVision;
            set => SetProerty(ref socksVision, value);
        }
        private string ssMethod = string.Empty;
        public string SSMethod
        {
            get => ssMethod;
            set => SetProerty(ref ssMethod, value);
        }
        private string ssPassword = string.Empty;
        public string SSPassword
        {
            get => ssPassword;
            set => SetProerty(ref ssPassword, value);
        }
        private string ssPlugin = string.Empty;
        public string SSPlugin
        {
            get => ssPlugin;
            set => SetProerty(ref ssPlugin, value);
        }
        private string ssPluginOpts = string.Empty;
        public string SSPluginOpts
        {
            get => ssPluginOpts;
            set => SetProerty(ref ssPluginOpts, value);
        }
        private string vmessId = string.Empty;
        public string VmessId
        {
            get => vmessId;
            set => SetProerty(ref vmessId, value);
        }
        private string vmessAlterId = string.Empty;
        public string VmessAlterId
        {
            get => vmessAlterId.ToString();
            set => SetProerty(ref vmessAlterId, value);
        }
        private string vmessSecurity = string.Empty;
        public string VmessSecurity
        {
            get => vmessSecurity;
            set => SetProerty(ref vmessSecurity, value);
        }
        private string trojanPassword = string.Empty;
        public string TrojanPassword
        {
            get => trojanPassword;
            set => SetProerty(ref trojanPassword, value);
        }
        private string vlessId = string.Empty;
        public string VlessId
        {
            get => vlessId;
            set => SetProerty(ref vlessId, value);
        }
        private string vlessFlow = string.Empty;
        public string VlessFlow
        {
            get => vlessFlow;
            set => SetProerty(ref vlessFlow, value);
        }

        public SnackbarMessageQueue SnackBarMsg { get;private set; } = new();
        public static readonly ReadOnlyCollection<string> ProxyProtocols = new(new List<string>
        {
            Modle.SingboxOptions.OutboundTypes.Shadowsocks,
            Modle.SingboxOptions.OutboundTypes.VMss,
            Modle.SingboxOptions.OutboundTypes.Trojan,
            Modle.SingboxOptions.OutboundTypes.VLESS,
            Modle.SingboxOptions.OutboundTypes.Socks
        });
        public static readonly ReadOnlyCollection<string> TransportTypes = new(new List<string>
        {
            Modle.SingboxOptions.V2RayTransportTypes.HTTP,
            Modle.SingboxOptions.V2RayTransportTypes.WebSocket,
            Modle.SingboxOptions.V2RayTransportTypes.HTTPUpgrade
        });
        public static readonly ReadOnlyCollection<string> SSMethods = new(new List<string>
        {
            "none",
            "2022-blake3-aes-128-gcm",
            "2022-blake3-aes-256-gcm",
            "2022-blake3-chacha20-poly1305",
            "aes-128-gcm",
            "aes-192-gcm",
            "aes-256-gcm",
            "chacha20-ietf-poly1305",
            "xchacha20-ietf-poly1305",
            "aes-128-ctr",
            "aes-192-ctr",
            "aes-256-ctr",
            "aes-128-cfb",
            "aes-192-cfb",
            "aes-256-cfb",
            "rc4-md5",
            "chacha20-ietf",
            "xchacha20"
        });
        public static readonly ReadOnlyCollection<string> VMessSecurities = new(new List<string>
        {
            "auto",
            "none",
            "zero",
            "aes-128-gcm",
            "chacha20-poly1305",
            "aes-128-ctr"
        });
        public static readonly ReadOnlyCollection<string> VlessFlows = new(new List<string>
        {
            Modle.SingboxOptions.VlessFlows.None,
            Modle.SingboxOptions.VlessFlows.XTLS_RPRX_VISION
        });
        public static readonly ReadOnlyCollection<byte> UotVisions = new(new List<byte>
        {
            2,
            1,
        });
        public static readonly ReadOnlyCollection<string> SocksVisions = new(new List<string>
        {
            "5",
            "4a",
            "4",
        });
        public static readonly ReadOnlyCollection<string> FingerPrinters = new(new List<string>
        {
            "chrome",
            "chrome_psk",
            "chrome_psk_shuffle",
            "chrome_padding_psk_shuffle",
            "chrome_pq",
            "chrome_pq_psk",
            "firefox",
            "edge",
            "safari",
            "360",
            "qq",
            "ios",
            "android",
            "random",
            "randomized",
        });
    }
}

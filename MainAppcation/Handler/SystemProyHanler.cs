using XrayGUI.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsProxy;

namespace XrayGUI.Handler
{
    internal class SystemProxyHanler
    {
        private ProxyService _ProxyService;
        private SystemProxySettingObject _Setting = new();
        private LocalPortObect LocalPort;
        public SystemProxyHanler(SystemProxySettingObject setting, LocalPortObect localPort)
        {
            _ProxyService = new();
            _Setting = setting;
            LocalPort = localPort;
            LoadConfig();
        }
        public SystemProxyHanler() : this(ConfigObject.Instance.SystemProxySetting, ConfigObject.Instance.localPort)
        {

        }
        public void LoadConfig()
        {
            if(ConfigObject.Instance.ProxyEnable)
            {
                OnProxy();
            }
            else
            {
                OffProxy();
            }
        }

        private void OnProxy()
        {
            string serverAddr = _Setting.UseProtocol switch
            {
                SystemProtocol.Http => $"{Global.LoopBcakAddress}:{LocalPort.Http}",
                SystemProtocol.Socks => $"socks={Global.LoopBcakAddress}:{LocalPort.Scoks}",
                _ => throw new Exception($"{nameof(SystemProtocol)} cannot setting")
            };
            var customBypass = string.IsNullOrEmpty(_Setting.ByPassUrl) ? string.Empty : ";" + _Setting.ByPassUrl;
            _ProxyService.Server = serverAddr;
            _ProxyService.Bypass = string.Join(';', ProxyService.LanIp) + customBypass;
            _ProxyService.Global();
        }
        private void OffProxy() => _ProxyService.Direct();
        private static SystemProxyHanler? _instance;
        public static SystemProxyHanler Instance => _instance ??= new SystemProxyHanler();
    }
}

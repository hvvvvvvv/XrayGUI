using NetProxyController.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsProxy;

namespace NetProxyController.Handler
{
    internal class SystemProyHanler
    {
        private ProxyService _ProxyService;
        private SystemProxySettingObject _Setting = new();
        private LocalPortObect LocalPort;
        public SystemProyHanler(SystemProxySettingObject setting, LocalPortObect localPort)
        {
            _ProxyService = new();
            _Setting = setting;
            LocalPort = localPort;
        }

        public void OnProxy()
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
        public void OffProxy() => _ProxyService.Direct();
        
    }
}

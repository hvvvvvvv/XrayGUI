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
        private Modle.SystemProxySettingObject _Setting = new();
        private const string _serverHost= "127.0.0.1";
        public SystemProyHanler(Modle.SystemProxySettingObject setting)
        {
            _ProxyService = new();
            _Setting = setting;
        }

        public void OnProxy()
        {
            string serverAddr = _Setting.UseProtocol switch
            {
                Modle.SystemProtocol.Http => $"{_serverHost}:{_Setting.HttpPort}",
                Modle.SystemProtocol.Socks => $"socks={_serverHost}:{_Setting.SocksPort}",
                _ => throw new Exception($"{nameof(Modle.SystemProtocol)} cannot setting")
            };
            _ProxyService.Server = serverAddr;
            _ProxyService.Bypass = _Setting.ByPassUrl;
            _ProxyService.Global();
        }
        public void OffProxy() => _ProxyService.Direct();
        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Modle
{
    internal class SystemProxySettingObject
    {
        public int HttpPort { get; set; } = 10881;

        public int SocksPort { get; set; } = 10882;

        public SystemProtocol? UseProtocol { get; set; } = SystemProtocol.Http;

        public string ByPassUrl { get; set; } = string.Empty;

    }
    
}


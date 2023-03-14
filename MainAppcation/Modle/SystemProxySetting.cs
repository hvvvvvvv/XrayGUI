using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Modle
{
    internal class SystemProxySetting
    {
        public int? HttpPort { get; set; }

        public int? SocksPort { get; set; }

        public SystemProtocol? UseProtocol { get; set; }

        public string? ByPassUrl { get; set; }

    }
    enum SystemProtocol
    {
        None = 0,
        Http = 1,
        Socks = 2
    }
}


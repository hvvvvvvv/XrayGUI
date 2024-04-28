using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using XrayCoreConfigModle.OutBound;

namespace XrayGUI.Modle.Server
{
    internal class TrojanInfo: OutBoundConfiguration
    {
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? Level { get; set; }

        public override OutboundConfigurationObject ToOutboundConfigurationObject(string addr, int port)
        {
            return new TrojanConfigurationObject()
            {
                servers = new()
                {
                    new TrojanServerObject()
                    {
                        address = addr,
                        port = port,
                        password = Password,
                        email = Email,
                        level = Level
                    }
                }
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using XrayCoreConfigModle.OutBound;

namespace XrayGUI.Modle.Server
{
    internal class VmessInfo: OutBoundConfiguration
    {
        public string Id { get; set; } = string.Empty;
        public int AlterId { get; set; } = 0;
        public SecurityMode Security { get; set; } = SecurityMode.Auto;
        public int? Level { get; set; }

        public override OutboundConfigurationObject ToOutboundConfigurationObject(string addr, int port)
        {
            return new VMessConfigurationObject()
            {
                vnext = new()
                {
                    new VMessServerObject()
                    {
                        address = addr,
                        port = port,
                        users = new()
                        {
                            new VMessUserObject()
                            {
                                id = Id,
                                alterId = AlterId,
                                security = Security.GetStringValue(),
                                level = Level
                            }
                        }
                    }
                }
            };
        }
    }
}

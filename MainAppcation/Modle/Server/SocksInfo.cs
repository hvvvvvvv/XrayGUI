using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using XrayCoreConfigModle.OutBound;

namespace NetProxyController.Modle.Server
{
    internal class SocksInfo: OutBoundConfiguration
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string? User { get; set; }
        public string? Pass { get; set; }
        public int? Level { get; set; }

        public override OutboundConfigurationObject ToOutboundConfigurationObject(string addr, int port)
        {
            return new SocksConfigurationObject()
            {
                servers = new()
                {
                    new SocksServerObject()
                    {
                        address = addr,
                        port = port,
                        users = new()
                        {
                            new ScoksUserObject()
                            {
                                user = User,
                                pass = Pass,
                                level = Level
                            }
                        }
                    }
                }
            };
        }
    }
}

using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle;

namespace NetProxyController.Modle.Server
{
    internal class ServerItem
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public OutboundProtocol Protocol { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public int ProtocolInfoIndex { get; set; }
        public int StreamInfoIndex { get; set; }
        public OutboundServerItemObject ToOutboundServerItemObject()
        {
            OutBoundConfiguration? outBoundServerConf = Protocol switch
            {
                OutboundProtocol.socks => Global.DBService.Find<SocksInfo>(ProtocolInfoIndex),
                OutboundProtocol.trojan => Global.DBService.Find<TrojanInfo>(ProtocolInfoIndex),
                OutboundProtocol.shadowsocks => Global.DBService.Find<ShadowSocksInfo>(ProtocolInfoIndex),
                OutboundProtocol.vmess => Global.DBService.Find<VmessInfo>(ProtocolInfoIndex),
                OutboundProtocol.vless => Global.DBService.Find<VlessInfo>(ProtocolInfoIndex),
                _ => null
            };
            return new()
            {
                protocol = Protocol.ToString(),
                tag = Index.ToString(),
                settings = outBoundServerConf?.ToOutboundConfigurationObject(Address, Port),
                streamSettings = Global.DBService.Find<StreamInfo>(StreamInfoIndex)?.ToStreamSettingsObject(),
            };
        }
    }
}

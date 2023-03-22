using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Modle
{
    internal class ServerItem
    {
        [PrimaryKey]
        [AutoIncrement]
        int Index { get; set; }
        OutboundProtocol Protocol { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public int ProtocolInfoIndex { get; set; }
        public int StreamInfoIndex { get; set; }
    }
}

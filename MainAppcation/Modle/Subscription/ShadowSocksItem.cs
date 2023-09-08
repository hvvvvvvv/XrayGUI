using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayGUI.Modle.Subscription
{
    internal class ShadowSocksItem
    {
        public string remarks { get; set; } = string.Empty;
        public string server { get; set; } = string.Empty;
        public int server_port { get; set; }
        public string method { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string plugin { get; set; } = string.Empty;
    }
}

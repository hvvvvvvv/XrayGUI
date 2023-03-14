using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle;

namespace NetProxyController.Modle
{
    internal class XrayCoreSettingObject
    {
        public string ExePath { get; set; } = AppContext.BaseDirectory + @"bin\xray.exe";
        public List<OutboundServerItemObject>? OutBoundServers { get; set; }
        public RoutingObject? RoutingSetting { get; set; }


    }
}

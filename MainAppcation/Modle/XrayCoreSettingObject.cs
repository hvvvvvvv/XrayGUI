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
        public List<OutboundServerItemObject> OutBoundServers { get; set; } = new();
        public RouteMatchSettingObject RouteMatchSetting { get; set; } = new();
        public List<XrayCoreConfigModle.Routing.RuleObject> RoutingRules{ get; set; } = new();
        public string? DefaultOutBoundServerTag { get; set; }

    }
    internal class RouteMatchSettingObject
    {
        public string domainStrategy { get; set; } = "AsIs";

        public string domainMatcher { get; set; } = "hybrid";
    }
}

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
        public RouteMatchSettingObject RouteMatchSetting { get; set; } = new();
        public List<XrayCoreConfigModle.Routing.RuleObject> RoutingRules{ get; set; } = new();
        public int? DefaultOutboundServerIndex { get; set; }
    }
    internal class RouteMatchSettingObject
    {
        public string domainStrategy { get; set; } = "AsIs";

        public string domainMatcher { get; set; } = "hybrid";
    }
}

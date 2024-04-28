using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle;

namespace XrayGUI.Modle
{
    internal class XrayCoreSettingObject
    {
        public RouteMatchSettingObject RouteMatchSetting { get; set; } = new();
        public List<XrayCoreConfigModle.Routing.RuleObject> RoutingRules{ get; set; } = new();
        public int? DefaultOutboundServerIndex { get; set; }
    }
    internal class RouteMatchSettingObject
    {
        public DomainStrategy domainStrategy { get; set; } = DomainStrategy.AsIs;

        public DomainMacher domainMatcher { get; set; } = DomainMacher.hybrid;
    }
}

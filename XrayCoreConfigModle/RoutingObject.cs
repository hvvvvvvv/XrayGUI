using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle
{
    public class RoutingObject 
    {
        /// <summary>
        /// 域名解析策略，根据不同的设置使用不同的策略。
        /// "AsIs"：只使用域名进行路由选择。默认值。
        /// "IPIfNonMatch"：当域名没有匹配任何规则时，将域名解析成 IP（A 记录或 AAAA 记录）再次进行匹配；
        /// 当一个域名有多个 A 记录时，会尝试匹配所有的 A 记录，直到其中一个与某个规则匹配为止；
        /// 解析后的 IP 仅在路由选择时起作用，转发的数据包中依然使用原始域名；
        /// "IPOnDemand"：当匹配时碰到任何基于 IP 的规则，将域名立即解析为 IP 进行匹配
        /// </summary>
        public string? domainStrategy { get; set; }
        /// <summary>
        /// 域名匹配算法，根据不同的设置使用不同的算法。此处选项会影响所有未单独指定匹配算法的 RuleObject。
        /// "hybrid"：使用新的域名匹配算法，速度更快且占用更少。默认值。
        /// "linear"：使用原来的域名匹配算法。
        /// </summary>
        public string? domainMatcher { get; set; }
        /// <summary>
        /// 数组中每一项是一个规则。
        /// 对于每一个连接，路由将根据这些规则从上到下依次进行判断，
        /// 当遇到第一个生效规则时，即将这个连接转发至它所指定的 outboundTag或 balancerTag。
        /// </summary>
        public List<Routing.RuleObject>? rules { get; set; }
        /// <summary>
        /// 数组中每一项是一个负载均衡器的配置。
        /// 当一个规则指向一个负载均衡器时，Xray 会通过此负载均衡器选出一个 outbound, 然后由它转发流量。
        /// </summary>
        public List<Routing.BalancerObject>? balancers { get; set; }
    }
}

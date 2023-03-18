using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle
{
    public class MainConfiguration 
    {
        /// <summary>
        /// 日志配置，控制 Xray 输出日志的方式.
        /// </summary>
        public LogObject? log { get; set; }
        /// <summary>
        /// 提供了一些 API 接口供远程调用。
        /// </summary>
        public ApiObject? api { get; set; }
        /// <summary>
        /// 内置的 DNS 服务器. 如果没有配置此项，则使用系统的 DNS 设置。
        /// </summary>
        public DnsObject? dns { get; set; }
        /// <summary>
        /// 路由功能。可以设置规则分流数据从不同的 outbound 发出.
        /// </summary>
        public RoutingObject? routing { get; set; }
        /// <summary>
        /// 本地策略，可以设置不同的用户等级和对应的策略设置。
        /// </summary>
        public PolicyObject? policy { get; set; }
        /// <summary>
        /// 每个元素是一个入站连接配置。
        /// </summary>
        public List<InboundServerItemObject>? inbounds { get; set; }
        /// <summary>
        /// 每个元素是一个出站连接配置。
        /// </summary>
        public List<OutboundServerItemObject>? outbounds { get; set; }
        /// <summary>
        /// 用于配置 Xray 其它服务器建立和使用网络连接的方式。
        /// </summary>
        public TransportObject? transport { get; set; }
        /// <summary>
        /// 用于配置流量数据的统计。
        /// </summary>
        public StatsObject? stats { get; set; }
        /// <summary>
        /// 反向代理。可以把服务器端的流量向客户端转发，即逆向流量转发。
        /// </summary>
        public ReverseObject? reverse { get; set; }
        /// <summary>
        /// FakeDNS 配置。可配合透明代理使用，以获取实际域名。
        /// </summary>
        public FakeDNSObject? fakedns { get; set; }
    }
}

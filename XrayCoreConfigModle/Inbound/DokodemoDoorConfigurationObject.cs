using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    /// <summary>
    /// Dokodemo door（任意门）可以监听一个本地端口，
    /// 并把所有进入此端口的数据发送至指定服务器的一
    /// 个端口，从而达到端口映射的效果。
    /// </summary>
    public class DokodemoDoorConfigurationObject: InboundConfigurationObject
    {
        /// <summary>
        /// 将流量转发到此地址。可以是一个 IP 地址，形如 "1.2.3.4"，或者一个域名，形如 "xray.com"。字符串类型。
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// 将流量转发到目标地址的指定端口，范围 [1, 65535]，数值类型。必填参数。
        /// </summary>
        public int? port { get; set; }
        /// <summary>
        /// 可接收的网络协议类型。比如当指定为 "tcp" 时，仅会接收 TCP 流量。默认值为 "tcp"。
        /// 可选值："tcp" | "udp" | "tcp,udp"
        /// </summary>
        public string? network { get; set; }
        /// <summary>
        /// 连接空闲的时间限制。单位为秒。默认值为 300。处理一个连接时，如果在 timeout 时间内，没有任何数据被传输，则中断该连接。
        /// </summary>
        public int? timeout { get; set; }
        /// <summary>
        /// 当值为 true 时，dokodemo-door 会识别出由 iptables 转发而来的数据，并转发到相应的目标地址。
        /// </summary>
        public bool? followRedirect { get; set; }
        /// <summary>
        /// userLevel 的值, 对应 policy 中 level 的值. 如不指定, 默认为 0。
        /// </summary>
        public int? userLevel { get; set; }
    }
}

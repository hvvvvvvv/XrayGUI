using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Inbound;

namespace XrayCoreConfigModle
{
    [JsonConverter(typeof(JsonConverters.InboundServerConverter))]
    public class InboundServerItemObject 
    {
        /// <summary>
        /// 监听地址，IP 地址或 Unix domain socket，默认值为 "0.0.0.0"，表示接收所有网卡上的连接.
        /// </summary>
        public string? listen { get; set; }
        /// <summary>
        /// 监听端口
        /// </summary>
        public int? port { get; set; }
        /// <summary>
        /// 连接协议名称
        /// </summary>
        public string? protocol { get; set; }
        /// <summary>
        /// 具体的配置内容，protocol的值不同而不同
        /// </summary>
        public InboundConfigurationObject? settings { get; set; }
        /// <summary>
        /// 底层传输方式（transport）是当前 Xray 节点和其它节点对接的方式
        /// </summary>
        public ProtocolSetting.StreamSettingsObject? streamSettings { get; set; }
        /// <summary>
        /// 此入站连接的标识，用于在其它的配置中定位此连接。
        /// </summary>
        public string? tag { get; set; }
        /// <summary>
        /// 流量探测
        /// 主要作用于在透明代理等用途. 比如一个典型流程如下:
        ///     1、如有一个设备上网,去访问 abc.com,首先设备通过 DNS 查询得到 abc.com 的 IP 是 1.2.3.4,然后设备会向 1.2.3.4 去发起连接.
        ///     2、如果不设置嗅探,Xray 收到的连接请求是 1.2.3.4,并不能用于域名规则的路由分流.
        ///     3、当设置了 sniffing 中的 enable 为 true,Xray 处理此连接的流量时,会从流量的数据中,嗅探出域名,即 abc.com
        ///     4、Xray 会把 1.2.3.4 重置为 abc.com.路由就可以根据域名去进行路由的域名规则的分流
        /// </summary>
        public SniffingObject? sniffing { get; set; }
        /// <summary>
        /// 当设置了多个 port 时, 端口分配的具体设置
        /// </summary>
        public AllocateObject? allocate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.JsonConverters;

namespace XrayCoreConfigModle
{
    [JsonConverter(typeof(OutboundServerItemObjectConverter))]
    public class OutboundServerItemObject 
    {
        /// <summary>
        /// 用于发送数据的 IP 地址，当主机有多个 IP 地址时有效，默认值为 "0.0.0.0"。
        /// </summary>
        public string? sendThrough { get; set; }
        /// <summary>
        /// 连接协议名称，详情参阅：https://github.com/XTLS/Xray-docs-next/tree/main/docs/config/outbounds
        /// </summary>
        public string? protocol { get; set; }
        /// <summary>
        /// 具体的配置内容，视协议不同而不同。
        /// </summary>
        public OutBound.OutboundConfigurationObject? settings { get; set; }
        /// <summary>
        /// 此出站连接的标识，用于在其它的配置中定位此连接。
        /// </summary>
        public string? tag { get; set; }
        /// <summary>
        /// 底层传输方式（transport）是当前 Xray 节点和其它节点对接的方式
        /// </summary>
        public ProtocolSetting.StreamSettingsObject? streamSettings { get; set; }
        /// <summary>
        /// 出站代理配置。当出站代理生效时，此 outbound 的 streamSettings 将不起作用。
        /// </summary>
        public OutboundProxySettingsObject? proxySettings { get; set; }
        /// <summary>
        /// Mux 相关的具体配置。
        /// </summary>
        public OutboundMuxObject? mux { get; set; }
    }
    public class OutboundProxySettingsObject
    {
        /// <summary>
        /// 当指定另一个 outbound 的标识时，此 outbound 发出的数据，将被转发至所指定的 outbound 发出。
        /// </summary>
        public string? tag { get; set; }
    }
    /// <summary>
    /// Mux 功能是在一条 TCP 连接上分发多个 TCP 连接的数据。实现细节详见 Mux.Cool。
    /// Mux 是为了减少 TCP 的握手延迟而设计，而非提高连接的吞吐量。使用 Mux 看视频、下载或者测速通常都有反效果。Mux 只需要在客户端启用，服务器端自动适配。
    /// </summary>
    public class OutboundMuxObject
    {
        /// <summary>
        /// 是否启用 Mux 转发请求，默认值 false。
        /// </summary>
        public bool? enabled { get; set; }
        /// <summary>
        /// 最大并发连接数。最小值 1，最大值 1024，默认值 8。
        /// </summary>
        public int? concurrency { get; set; }
    }
}

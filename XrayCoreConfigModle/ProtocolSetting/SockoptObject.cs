using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class SockoptObject 
    {
        /// <summary>
        /// 当其值非零时，在 ountbound 连接以此数值上标记 SO_MARK
        ///     仅适用于 Linux 系统。
        ///     需要 CAP_NET_ADMIN 权限。
        /// </summary>
        public int? mark { get; set; }
        /// <summary>
        /// 是否启用 TCP Fast Open(详情参阅：https://zh.wikipedia.org/wiki/TCP%E5%BF%AB%E9%80%9F%E6%89%93%E5%BC%80)
        /// 当其值为 true 或正整数时，启用 TFO；当其值为 false 或负数时，强制关闭 TFO；
        /// 当此项不存在或为 0 时，使用系统默认设置。 可用于 inbound/outbound。
        /// </summary>
        public bool? tcpFastOpen { get; set; }
        /// <summary>
        /// "redirect" | "tproxy" | "off"
        /// 是否开启透明代理（仅适用于 Linux）
        ///     "redirect"：使用 Redirect 模式的透明代理。支持所有基于 IPv4/6 的 TCP 和 UDP 连接。
        ///     "tproxy"：使用 TProxy 模式的透明代理。支持所有基于 IPv4/6 的 TCP 和 UDP 连接。
        ///     "off"：关闭透明代理。
        /// </summary>
        public string? tproxy { get; set; }
        /// <summary>
        /// "AsIs" | "UseIP" | "UseIPv4" | "UseIPv6"
        ///     "AsIs": 通过系统 DNS 服务器解析获取 IP, 向此域名发出连接
        ///     "UseIP"、"UseIPv4" 和 "UseIPv6": 使用内置 DNS 服务器解析获取 IP 后, 直接向此 IP 发出连接。
        ///     默认值为 "AsIs"
        /// 在之前的版本中，当 Xray 尝试使用域名建立系统连接时，域名的解析由系统完成，不受 Xray 控制。
        /// 这导致了在 非标准 Linux 环境中无法解析域名 等问题。为此，Xray 1.3.1 为 Sockopt 引入了 
        ///     Freedom 中的 domainStrategy，解决了此问题。
        /// </summary>
        public string? domainStrategy { get; set; }
        /// <summary>
        /// 一个出站代理的标识。当值不为空时，将使用指定的 outbound 发出连接。 此选项可用于支持底层传输方式的链式转发。
        /// </summary>
        public string? dialerProxy { get; set; }
        /// <summary>
        /// 仅用于 inbound，指示是否接收 PROXY protocol。
        /// PROXY protocol 专用于传递请求的真实来源 IP 和端口，若你不了解它，请先忽略该项。
        /// </summary>
        public bool? acceptProxyProtocol { get; set; }
        /// <summary>
        /// TCP 保持活跃的数据包发送间隔，单位为秒。该设置仅适用于 Linux 下(存疑)。
        /// 不配置此项或配置为 0 表示使用 Go 默认值。
        /// </summary>
        public int? tcpKeepAliveInterval { get; set; }
        /// <summary>
        /// TCP 拥塞控制算法。仅支持 Linux。 不配置此项表示使用系统默认值。
        /// </summary>
        public string? tcpcongestion { get; set; }
        /// <summary>
        /// 指定绑定出口网卡名称 仅支持 linux。
        /// </summary>
        [JsonPropertyName("interface")]
        public string? interface_ { get; set; }
    }
}

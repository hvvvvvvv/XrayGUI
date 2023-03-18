using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class FallbackObject 
    {
        /// <summary>
        /// 尝试匹配 TLS SNI(Server Name Indication)，空为任意，默认为 ""
        /// </summary>
        public string? name { get; set; }
        /// <summary>
        /// 尝试匹配 TLS ALPN 协商结果，空为任意，默认为 ""
        /// </summary>
        public string? alpn { get; set; }
        /// <summary>
        /// 尝试匹配首包 HTTP PATH，空为任意，默认为空，非空则必须以 / 开头，不支持 h2c。
        /// </summary>
        public string? path { get; set; }
        /// <summary>
        /// 决定 TLS 解密后 TCP 流量的去向，目前支持两类地址：（该项必填，否则无法启动）
        /// 1、格式为 "addr:port"，其中 addr 支持 IPv4、域名、IPv6，若填写域名，也将直接发起 TCP 连接（而不走内置的 DNS）。
        /// 2、Unix domain socket，格式为绝对路径，形如 "/dev/shm/domain.socket"，可在开头加 @ 代表 abstract，@@ 则代表带 padding 的 abstract。
        /// </summary>
        public int? dest { get; set; }
        /// <summary>
        /// 发送 PROXY protocol，专用于传递请求的真实来源 IP 和端口，填版本 1 或 2，默认为 0，即不发送。若有需要建议填 1。
        /// </summary>
        public int? xver { get; set; }
    }
}

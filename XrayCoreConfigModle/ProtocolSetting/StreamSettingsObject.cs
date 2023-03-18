using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class StreamSettingsObject 
    {
        /// <summary>
        /// "tcp" | "kcp" | "ws" | "http" | "domainsocket" | "quic" | "grpc"
        /// 连接的数据流所使用的传输方式类型，默认值为 "tcp"
        /// </summary>
        public string? network { get; set; }
        /// <summary>
        /// 是否启用传输层加密，支持的选项有
        /// "none" | "tls" | "xtls"| "reality"
        /// </summary>
        public string? security { get; set; }
        /// <summary>
        /// TLS 配置。TLS 由 Golang 提供，通常情况下 TLS 协商的结果为使用 TLS 1.3，不支持 DTLS。
        /// </summary>
        public TLSObject? tlsSettings { get; set; }
        /// <summary>
        /// XTLS 配置。security 为"xtls"时生效
        /// </summary>
        public TLSObject? xtlsSettings { get; set; }
        /// <summary>
        /// reality 配置。security 为"reality"时生效
        /// </summary>
        public RealityObject? realitySettings { get; set; }
        /// <summary>
        /// tcp 配置。network 为"tcp"时生效
        /// </summary>
        public TcpObject? tcpSettings { get; set; }
        /// <summary>
        /// kcp 配置。network 为"kcp"时生效
        /// </summary>
        public KcpObject? kcpSettings { get; set; }
        /// <summary>
        /// WebSocket 配置。network 为"ws"时生效
        /// </summary>
        public WebSocketObject? wsSettings { get; set; }
        /// <summary>
        /// http 配置。network 为"http"时生效
        /// </summary>
        public HttpObject? httpSettings { get; set; }
        /// <summary>
        /// QUIC 配置。network 为"quic"时生效
        /// </summary>
        public QuicObject? quicSettings { get; set; }
        /// <summary>
        /// Domain socket配置，network 为"domainsocket"时生效
        /// </summary>
        public DomainSocketObject? dsSettings { get; set; }
        /// <summary>
        /// gRPC 配置，network 为"grpc"时生效
        /// </summary>
        public GRPCObject? grpcSettings { get; set; }
        /// <summary>
        /// 透明代理相关的具体配置
        /// </summary>
        public SockoptObject? sockopt { get; set; }
    }
}

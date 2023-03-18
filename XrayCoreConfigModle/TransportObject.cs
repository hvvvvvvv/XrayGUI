using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle.ProtocolSetting;

namespace XrayCoreConfigModle
{
    public class TransportObject 
    {
        /// <summary>
        /// 针对 TCP 连接的配置
        /// </summary>
        public TcpObject? tcpSettings { get; set; }
        /// <summary>
        /// 针对 mKCP 连接的配置
        /// </summary>
        public KcpObject? kcpSettings { get; set; }
        /// <summary>
        /// 针对 WebSocket 连接的配置
        /// </summary>
        public WebSocketObject? wsSettings { get; set; }
        /// <summary>
        /// 针对 HTTP/2 连接的配置
        /// </summary>
        public HttpObject? httpSettings { get; set; }
        /// <summary>
        /// 针对 QUIC 连接的配置
        /// </summary>
        public QuicObject? quicSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DomainSocketObject? dsSettings { get; set; }
        /// <summary>
        /// 针对 gRPC 连接的配置
        /// </summary>
        public GRPCObject? grpcSettings { get; set; }
    }
}

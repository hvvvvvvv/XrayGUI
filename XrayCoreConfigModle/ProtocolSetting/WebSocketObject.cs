using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class WebSocketObject 
    {
        /// <summary>
        /// 仅用于 inbound，指示是否接收 PROXY protocol。
        /// PROXY protocol 专用于传递请求的真实来源 IP 和端口，若你不了解它，请先忽略该项。
        /// 常见的反代软件（如 HAProxy、Nginx）都可以配置发送它，VLESS fallbacks xver 也可以发送它。
        /// 填写 true 时，最底层 TCP 连接建立后，请求方必须先发送 PROXY protocol v1 或 v2，否则连接会被关闭。
        /// 默认值为 false。
        /// </summary>
        public bool? acceptProxyProtocol { get; set; }
        /// <summary>
        /// WebSocket 所使用的 HTTP 协议路径，默认值为 "/"。
        /// </summary>
        public string? path { get; set; }
        /// <summary>
        /// 自定义 HTTP 头，每个键表示一个 HTTP 头的名称，对应的值是字符串。
        /// 默认值为空。
        /// </summary>
        public Dictionary<string,string>? headers { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class TcpObject 
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
        /// 数据包头部伪装设置，默认值为 NoneHeaderObject。
        /// </summary>
        public HeaderObject? header { get; set; }
    }
    public class HeaderObject 
    {
        public HeaderObject(string _type)
        {
            type = _type;
        }
        public string type { get; set; }
    }
    public class NoneHeaderObject: HeaderObject
    {
        public NoneHeaderObject() :base("none")
        {

        }
    }
    public class HttpHeaderObject: HeaderObject
    {
        public HttpHeaderObject():base("http")
        {

        }
        public Http.HTTPRequestObject? request { get; set; }
        public Http.HTTPResponseObject? response { get; set; }
    }

}

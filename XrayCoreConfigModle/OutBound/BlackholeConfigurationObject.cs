using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class BlackholeConfigurationObject: OutboundConfigurationObject
    {
        /// <summary>
        /// 配置黑洞的响应数据。
        /// Blackhole 会在收到待转发数据之后，发送指定的响应数据，然后关闭连接，待转发的数据将被丢弃。
        /// 如不指定此项，Blackhole 将直接关闭连接。
        /// </summary>
        public Response? response { get; set; }
    }
    public class Response 
    {
        /// <summary>
        /// "http" | "none"
        /// 当 type 为 "none"（默认值）时，Blackhole 将直接关闭连接。
        /// 当 type 为 "http" 时，Blackhole 会发回一个简单的 HTTP 403 数据包，然后关闭连接。
        /// </summary>
        public string? type { get; set; }
    }
}

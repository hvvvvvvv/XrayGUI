using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting.Http
{
    public class HTTPResponseObject 
    {
        /// <summary>
        /// HTTP 版本，默认值为 "1.1"。
        /// </summary>
        public string? version { get; set; }
        /// <summary>
        /// HTTP 状态，默认值为 "200"。
        /// </summary>
        public string? status { get; set; }
        /// <summary>
        /// HTTP 状态说明，默认值为 "OK"。
        /// </summary>
        public string? reason { get; set; }
        /// <summary>
        /// HTTP 头，每个键表示一个 HTTP 头的名称，对应的值是一个数组。
        /// 详情参阅：https://github.com/XTLS/Xray-docs-next/blob/main/docs/config/transports/tcp.md#httpresponseobject
        /// </summary>
        public Dictionary<string,List<string>>? headers { get; set; }
    }
}

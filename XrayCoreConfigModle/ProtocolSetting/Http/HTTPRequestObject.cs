using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting.Http
{
    public class HTTPRequestObject 
    {
        /// <summary>
        /// HTTP 版本，默认值为 "1.1"。
        /// </summary>
        public string? version { get; set; }
        /// <summary>
        /// HTTP 方法，默认值为 "GET"。
        /// </summary>
        public string? method { get; set; }
        /// <summary>
        /// 路径，一个字符串数组。默认值为 ["/"]。当有多个值时，每次请求随机选择一个值。
        /// </summary>
        public List<string>? path { get; set; }
        /// <summary>
        /// HTTP 头,每个键表示一个 HTTP 头的名称，对应的值是一个数组。
        /// 每次请求会附上所有的键，并随机选择一个对应的值。
        /// 详情参阅 https://github.com/XTLS/Xray-docs-next/blob/main/docs/config/transports/tcp.md#httprequestobject
        /// </summary>
        public Dictionary<string,List<string>>? headers { get; set; }
    }
}

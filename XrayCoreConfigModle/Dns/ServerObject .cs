using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Dns
{
    /// <summary>
    /// DNS服务器定义，支持的形式较多
    /// 详情参阅 https://github.com/XTLS/Xray-docs-next/blob/main/docs/config/dns.md#serverobject
    /// </summary>
    public class ServerObject: DnsServer
    {
        /// <summary>
        ///
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? port { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string>? domains { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string>? expectIPs { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? skipFallback { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string? clientIP { get; set; }
    }
}

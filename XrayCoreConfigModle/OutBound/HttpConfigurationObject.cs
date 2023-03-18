using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class HttpConfigurationObject: OutboundConfigurationObject
    {
        /// <summary>
        /// HTTP 服务器列表，其中每一项是一个服务器配置，若配置多个，循环使用 (RoundRobin)。
        /// </summary>
        public List<HttpServerObject>? servers { get; set; }
    }

    public class HttpServerObject 
    {
        /// <summary>
        /// HTTP 代理服务器地址，必填。
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// HTTP 代理服务器端口，必填。
        /// </summary>
        public int? port { get; set; }
        /// <summary>
        /// ，数组中每个元素为一个用户帐号。默认值为空。
        /// </summary>
        public List<HttpAccountObject>? users { get; set; }
    }
    public class HttpAccountObject 
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? user { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? pass { get; set; }
    }
}

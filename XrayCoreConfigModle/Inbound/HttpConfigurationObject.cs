using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class HttpConfigurationObject: InboundConfigurationObject
    {
        /// <summary>
        /// 连接空闲的时间限制。单位为秒。默认值为 300, 0 表示不限时
        /// </summary>
        public int? timeout { get; set; }

        /// <summary>
        /// 数组中每个元素为一个用户帐号。默认值为空。
        /// </summary>
        public List<HttpAccountObject>? accounts { get; set; }
        /// <summary>
        /// 当为 true 时，会转发所有 HTTP 请求，而非只是代理请求。
        /// </summary>
        public bool? allowTransparent { get; set; }
        /// <summary>
        /// 对应 policy 中 level 的值。 如不指定, 默认为 0。
        /// </summary>
        public int? userLevel { get; set; }
    }
    public class HttpAccountObject 
    {       
        public string? user { get; set; }
        public string? pass { get; set; }
    }
}

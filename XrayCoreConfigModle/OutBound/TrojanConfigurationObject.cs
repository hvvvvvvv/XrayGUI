using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class TrojanConfigurationObject: OutboundConfigurationObject
    {
        public List<TrojanServerObject>? servers { get; set; }
    }
    public class TrojanServerObject 
    {
        /// <summary>
        /// 服务端地址，支持 IPv4、IPv6 和域名。必填。
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// 服务端端口，通常与服务端监听的端口相同。
        /// </summary>
        public int? port { get; set; }
        /// <summary>
        /// 密码. 必填，任意字符串。
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// 邮件地址，可选，用于标识用户
        /// </summary>
        public string? email { get; set; }
        /// <summary>
        /// 用户等级，连接会使用这个用户等级对应的 本地策略。
        /// </summary>
        public int? level { get; set; }
    }
}

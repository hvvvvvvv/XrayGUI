using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class SocksConfigurationObject: InboundConfigurationObject
    {
        /// <summary>
        /// Socks 协议的认证方式，支持 "noauth" 匿名方式和 "password" 用户密码方式。
        /// </summary>
        public string? auth { get; set; }
        /// <summary>
        /// 数组中每个元素为一个用户帐号,此选项仅当 auth 为 password 时有效。
        /// </summary>
        public List<SocksAccounts>? accounts { get; set; }
        /// <summary>
        /// 是否开启 UDP 协议的支持,默认值为 false。
        /// </summary>
        public bool? udp { get; set; }
        /// <summary>
        /// 当开启 UDP 时，Xray 需要知道本机的 IP 地址,默认值为 "127.0.0.1"
        /// </summary>
        public string? ip { get; set; }
        /// <summary>
        /// 对应 policy 中 level 的值。 如不指定, 默认为 0。
        /// </summary>
        public int? userLevel { get; set; }
    }
    public class SocksAccounts 
    {
        public string? user { get; set; }
        public string? pass { get; set; }
    }
}

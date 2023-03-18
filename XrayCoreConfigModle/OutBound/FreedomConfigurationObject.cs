using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class FreedomConfigurationObject: OutboundConfigurationObject
    {
        /// <summary>
        ///  "AsIs" | "UseIP" | "UseIPv4" | "UseIPv6"
        ///  在目标地址为域名时, 配置相应的值, Freedom 的行为模式如下:
        ///  "AsIs": Freedom 通过系统 DNS 服务器解析获取 IP, 向此域名发出连接.
        ///  "UseIP"、"UseIPv4" 和 "UseIPv6": Xray 使用 内置 DNS 服务器 解析获取 IP, 向此域名发出连接. 默认值为 "AsIs"。
        /// </summary>
        public string? domainStrategy { get; set; }
        /// <summary>
        /// Freedom 会强制将所有数据发送到指定地址（而不是 inbound 指定的地址）。
        /// </summary>
        public string? redirect { get; set; }
        /// <summary>
        /// 用户等级，连接会使用这个用户等级对应的 本地策略。
        /// </summary>
        public int? userLevel { get; set; }
    }
}

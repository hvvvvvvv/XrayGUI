using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class ShadowsocksConfigurationObject: InboundConfigurationObject
    {
        /// <summary>
        /// 密码（必填）
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// 加密方式（必填）
        /// </summary>
        public string? method { get; set; }
        /// <summary>
        /// 对应 policy 中 level 的值。 如不指定, 默认为 0。
        /// </summary>
        public int? level { get; set; }
        /// <summary>
        /// 用户邮箱，用于区分不同用户的流量（日志、统计）
        /// </summary>
        public string? email { get; set; }
        /// <summary>
        /// 可接收的网络协议类型。比如当指定为 "tcp" 时，仅会接收 TCP 流量。默认值为 "tcp"
        /// 可选值："tcp" | "udp" | "tcp,udp"
        /// </summary>
        public string? network { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class ShadowsocksConfigurationObject: OutboundConfigurationObject
    {
        /// <summary>
        /// 代表一组 Shadowsocks 服务端设置
        /// </summary>
        public List<ShadowsocksServerObject>? servers { get; set; }
    }
    public class ShadowsocksServerObject 
    {
        /// <summary>
        /// 邮件地址，可选，用于标识用户
        /// </summary>
        public string? email { get; set; }
        /// <summary>
        /// Shadowsocks 服务端地址，支持 IPv4、IPv6 和域名。必填。
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// Shadowsocks 服务端端口。必填。
        /// </summary>
        public int? port { get; set; }
        /// <summary>
        /// 加密方式
        /// </summary>
        public string? method { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// 当开启后，会启用udp over tcp。
        /// </summary>
        public string? uot { get; set; }
        /// <summary>
        /// 用户等级，连接会使用这个用户等级对应的 本地策略。
        /// </summary>
        public int? level { get; set; }
    }
}

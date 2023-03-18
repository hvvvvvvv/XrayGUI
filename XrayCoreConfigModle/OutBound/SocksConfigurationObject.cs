using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class SocksConfigurationObject: OutboundConfigurationObject
    {
        /// <summary>
        /// Socks 服务器列表，其中每一项是一个服务器配置。
        /// </summary>
        public List<SocksServerObject>? servers { get; set; }
    }

    public class SocksServerObject 
    {
        /// <summary>
        /// 服务器地址, 必填
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// 服务器端口, 必填
        /// </summary>
        public int? port { get; set; }
        /// <summary>
        /// 当列表不为空时，Socks 客户端会使用用户信息进行认证；如未指定，则不进行认证。
        /// </summary>
        public List<ScoksUserObject>? users { get; set; }
    }
    public class ScoksUserObject 
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string? user { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string? pass { get; set; }
        /// <summary>
        /// 用户等级，连接会使用这个用户等级对应的 本地策略。
        /// </summary>
        public int? level { get; set; }
    }
}

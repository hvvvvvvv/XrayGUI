using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class VlessConfigurationObject: OutboundConfigurationObject
    {
        /// <summary>
        /// 表示 VLESS 服务器列表，包含一组指向服务端的配置, 其中每一项是一个服务器配置。
        /// </summary>
        public List<VlessServerObject>? vnext { get; set; }
    }

    public class VlessServerObject 
    {
        /// <summary>
        /// 服务端地址，指向服务端，支持域名、IPv4、IPv6。
        /// </summary>
        public string? address { get; set; }
        /// <summary>
        /// 服务端端口，通常与服务端监听的端口相同。
        /// </summary>
        public int? port { get; set; }
        /// <summary>
        /// 数组, 一组服务端认可的用户列表, 其中每一项是一个用户配置
        /// </summary>
        public List<VlessUserObject>? users { get; set; }
    }
    public class VlessUserObject 
    {
        /// <summary>
        /// VLESS 的用户 ID，可以是任意小于 30 字节的字符串, 也可以是一个合法的 UUID. 自定义字符串和其映射的 UUID 是等价的
        /// </summary>
        public string? id { get; set; }
        /// <summary>
        /// 需要填 "none"，不能留空。
        /// </summary>
        public string? encryption { get; set; }
        /// <summary>
        /// 流控模式，用于选择 XTLS 的算法。
        /// 目前出站协议中有以下流控模式可选：
        ///     无 flow，空字符或者 none：使用普通 TLS 代理
        ///     xtls-rprx-vision：使用新 XTLS 模式 包含内层握手随机填充 支持 uTLS 模拟客户端指纹
        ///     xtls-rprx-vision-udp443：同 xtls-rprx-vision, 但是放行了目标为 443 端口的 UDP 流量
        /// </summary>
        public string? flow { get; set; }
        /// <summary>
        /// 用户等级，连接会使用这个用户等级对应的 本地策略。
        /// </summary>
        public int? level { get; set; }
    }
}

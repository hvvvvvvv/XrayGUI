using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class VlessConfigurationObject: InboundConfigurationObject
    {
        /// <summary>
        /// 代表一组服务端认可的用户.
        /// </summary>
        public List<VlessClientsObject>? clients { get; set; }
        /// <summary>
        /// 现阶段需要填 "none"，不能留空。 若未正确设置 decryption 的值，使用 Xray 或 -test 时会收到错误信息。        /// 
        /// </summary>
        public string? decryption { get; set; } = "none";
        /// <summary>
        /// 包含一系列强大的回落分流配置（可选）
        /// </summary>
        public List<FallbackObject>? fallbacks { get; set; }
    }
    public class VlessClientsObject 
    {
        /// <summary>
        /// VLESS 的用户 ID，可以是任意小于 30 字节的字符串, 也可以是一个合法的 UUID.
        /// </summary>
        public string? id { get; set; }
        /// <summary>
        /// VLESS 的用户 ID，可以是任意小于 30 字节的字符串, 也可以是一个合法的 UUID.
        /// </summary>
        public int? level { get; set; }
        /// <summary>
        /// 用户邮箱，用于区分不同用户的流量（会体现在日志、统计中）。
        /// </summary>
        public string? email { get; set; }
        /// <summary>
        /// 流控模式，用于选择 XTLS 的算法。
        /// 目前入站协议中有以下流控模式可选：
        ///     无 flow，空字符或者 none：使用普通 TLS 代理
        ///     xtls-rprx-vision：使用新 XTLS 模式 包含内层握手随机填充
        ///     此外，目前 XTLS 仅支持 TCP、mKCP、DomainSocket 这三种传输方式。
        /// </summary>
        public string? flow { get; set; }
    }
}

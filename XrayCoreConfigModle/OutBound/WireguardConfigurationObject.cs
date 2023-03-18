using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class WireguardConfigurationObject: OutboundConfigurationObject
    {
        /// <summary>
        /// 用户私钥。必填。
        /// </summary>
        public string? secretKey { get; set; }
        /// <summary>
        /// Wireguard 会在本地开启虚拟网卡 tun。使用一个或多个 IP 地址，支持 IPv6
        /// </summary>
        public List<string>? address { get; set;}
        /// <summary>
        /// Wireguard 服务器列表，其中每一项是一个服务器配置。
        /// </summary>
        public List<WireguardPeers>? peers { get; set; }
        /// <summary>
        /// Wireguard 底层 tun 的分片大小
        /// </summary>
        public int? mtu { get; set; }
        /// <summary>
        /// Wireguard 使用线程数
        /// </summary>
        public int? workers { get; set; }
    }
    public class WireguardPeers 
    {
        /// <summary>
        /// 服务器地址, 必填
        /// </summary>
        public string? endpoint { get; set; }
        /// <summary>
        /// 服务器公钥，用于验证, 必填
        /// </summary>
        public string? publicKey { get; set; }
        /// <summary>
        /// 额外的对称加密密钥
        /// </summary>
        public string? preSharedKey { get; set; }
        /// <summary>
        /// 心跳包时间间隔，单位为秒，默认为 0 表示无心跳
        /// </summary>
        public int? keepAlive { get; set; }
        /// <summary>
        /// Wireguard 仅允许特定源 IP 的流量
        /// </summary>
        public List<string>? allowedIPs { get; set; }
    }
}

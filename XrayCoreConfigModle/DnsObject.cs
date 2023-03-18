using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle
{
    /// <summary>
    /// XrayCore内置DNS服务器配置
    /// 详情参阅 https://github.com/XTLS/Xray-docs-next/blob/main/docs/config/dns.md#dnsobject
    /// </summary>
    public class DnsObject 
    {
        /// <summary>
        /// 静态 IP 列表，其值为一系列"域名": ["地址 1","地址 2"]。其中地址可以是 IP 或者域名。
        /// 在解析域名时，如果域名匹配这个列表中的某一项:
        /// 当该项的地址为 IP 时，则解析结果为该项的 IP
        /// 当该项的地址为域名时，会使用此域名进行 IP 解析，而不使用原始域名。
        /// 当地址中同时设置了多个 IP 和域名，则只会返回第一个域名，其余 IP 和域名均被忽略。
        /// </summary>
        public Dictionary<string, List<string>>? hosts { get; set; }

        /// <summary>
        /// 一个 DNS 服务器列表,目前字符串隐式转换只支持"address:port"的形式，其他形式太复杂，暂时没做相应的隐式转换
        /// </summary>
        public List<Dns.DnsServer>? servers { get; set; }
        /// <summary>
        /// 用于 DNS 查询时通知服务器以指定 IP 的地理位置。不能是私有地址。
        /// </summary>
        public string? clientIp { get; set; }
        /// <summary>
        /// 指定能查询的DNS记录类型
        /// Vlues:
        ///     "UseIP":查询A记录(即IPV4地址)和AAAA记录(即IPV6地址)
        ///     "UseIPv4":只查询A记录
        ///     "UseIPv6"只查询AAAA记录
        /// </summary>
        public string? queryStrategy { get; set; }
        /// <summary>
        /// true 禁用 DNS 缓存，默认为 false，即不禁用
        /// </summary>
        public bool? disableCache { get; set; }
        /// <summary>
        /// true 禁用 DNS 的 fallback 查询，默认为 false，即不禁用。
        /// </summary>
        public bool? disableFallback { get; set; }
        /// <summary>
        /// true 当 DNS 服务器的优先匹配域名列表命中时，禁用 fallback 查询，默认为 false，即不禁用。
        /// </summary>
        public bool? disableFallbackIfMatch { get; set; }
        /// <summary>
        /// 由内置 DNS 发出的查询流量，除 localhost、fakedns、TCPL、DOHL 和 DOQL 模式外，都可以用此标识在路由使用 inboundTag 进行匹配。
        /// </summary>
        public string? tag { get; set; }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Routing
{
    public class RuleObject 
    {
        /// <summary>
        /// 域名匹配算法，根据不同的设置使用不同的算法。此处选项优先级高于 RoutingObject 中配置的 domainMatcher。
        /// "hybrid"：使用新的域名匹配算法，速度更快且占用更少。默认值。
        /// "linear"：使用原来的域名匹配算法。
        /// </summary>
        public string? domainMatcher { get; set; }
        /// <summary>
        /// 目前只支持"field"这一个选项。
        /// </summary>
        public string? type { get; set; }
        /// <summary>
        /// 一个数组，数组每一项是一个域名的匹配。有以下几种形式：
        /// 纯字符串：当此字符串匹配目标域名中任意部分，该规则生效。比如 "sina.com" 
        ///     可以匹配 "sina.com"、"sina.com.cn" 和 "www.sina.com"，但不匹配 "sina.cn"。
        /// 正则表达式：
        ///      由 "regexp:" 开始，余下部分是一个正则表达式。当此正则表达式匹配目标域名时，
        ///      该规则生效。例如 "regexp:\\.goo.*\\.com$" 匹配 "www.google.com" 或 "fonts.googleapis.com"，
        ///      但不匹配 "google.com"。
        /// 子域名（推荐）：
        ///      由 "domain:" 开始，余下部分是一个域名。当此域名是目标域名或其子域名时，该规则生效。
        ///      例如 "domain:xray.com" 匹配 "www.xray.com"、"xray.com"，但不匹配 "wxray.com"。
        /// 完整匹配：由 "full:"
        ///         开始，余下部分是一个域名。当此域名完整匹配目标域名时，该规则生效。
        ///         例如 "full:xray.com" 匹配 "xray.com" 但不匹配 "www.xray.com"。
        /// 预定义域名列表：
        ///         由 "geosite:" 开头，余下部分是一个名称，如 geosite:google 或者 geosite:cn。
        ///         名称及域名列表参考 预定义域名列表。
        /// 从文件中加载域名：
        ///          形如 "ext:file:tag"，必须以 ext:（小写）开头，后面跟文件名和标签，文件存放在 资源目录 中，
        ///          文件格式与 geosite.dat 相同，标签必须在文件中存在。
        /// </summary>
        public List<string>? domain { get; set; }
        /// <summary>
        /// 一个数组，数组内每一项代表一个 IP 范围。当某一项匹配目标 IP 时，此规则生效。有以下几种形式
        /// IP 形如 "127.0.0.1"。
        /// CIDR：形如 "10.0.0.0/8"。
        /// 预定义 IP 列表：此列表预置于每一个 Xray 的安装包中，文件名为 geoip.dat。使用方式形如 "geoip:cn"，必须以 geoip:（小写）开头，后面跟双字符国家代码，支持几乎所有可以上网的国家。
        /// 特殊值："geoip:private"，包含所有私有地址，如 127.0.0.1。
        /// 从文件中加载 IP：形如 "ext:file:tag"，必须以 ext:（小写）开头，后面跟文件名和标签，文件存放在 资源目录 中，文件格式与 geoip.dat 相同标签必须在文件中存在。
        /// </summary>
        public List<string>? ip { get; set; }
        /// <summary>
        /// 目标端口范围，有三种形式：
        /// "a-b"：a 和 b 均为正整数，且小于 65536。这个范围是一个前后闭合区间，当目标端口落在此范围内时，此规则生效。
        /// a：a 为正整数，且小于 65536。当目标端口为 a 时，此规则生效。
        /// 以上两种形式的混合，以逗号 "," 分隔。形如："53,443,1000-2000"。
        /// </summary>
        public string? port { get; set; }
        /// <summary>
        /// 来源端口，有三种形式：
        /// "a-b"：a 和 b 均为正整数，且小于 65536。这个范围是一个前后闭合区间，当目标端口落在此范围内时，此规则生效。
        /// a：a 为正整数，且小于 65536。当目标端口为 a 时，此规则生效。
        /// 以上两种形式的混合，以逗号 "," 分隔。形如："53,443,1000-2000"。
        /// </summary>
        public string? sourcePort { get; set; }
        /// <summary>
        /// 可选的值有 "tcp"、"udp" 或 "tcp,udp"，当连接方式是指定的方式时，此规则生效。
        /// </summary>
        public string? network { get; set; }
        /// <summary>
        /// 数组内每一项代表一个 IP 范围，形式有 IP、CIDR、GeoIP 和从文件中加载 IP。当某一项匹配来源 IP 时，此规则生效。
        /// </summary>
        public List<string>? source { get; set; }
        /// <summary>
        /// 一个数组，数组内每一项是一个邮箱地址。当某一项匹配来源用户时，此规则生效。
        /// </summary>
        public List<string>? user { get; set; }
        /// <summary>
        /// 一个数组，数组内每一项是一个邮箱地址。当某一项匹配来源用户时，此规则生效。
        /// </summary>
        public List<string>? inboundTag { get; set; }
        /// <summary>
        /// 数组内每一项表示一种应用层协议。当某一个协议匹配当前连接的协议类型时，此规则生效。
        /// 可选值： "http" | "tls" | "bittorrent"
        /// </summary>
        public List<string>? protocol { get; set; }
        /// <summary>
        /// 一段脚本，用于检测流量的属性值。当此脚本返回真值时，此规则生效。
        /// 脚本语言为 Starlark，它的语法是 Python 的子集。脚本接受一个全局变量 attrs，其中包含了流量相关的属性。
        /// 目前只有 http 入站代理会设置这一属性。
        /// </summary>
        public string? attrs { get; set; }
        /// <summary>
        /// 对应一个 outbound 的标识
        /// </summary>
        public string? outboundTag { get; set; }
        /// <summary>
        /// 对应一个 Balancer 的标识
        /// </summary>
        public string? balancerTag { get; set; }
    }
}

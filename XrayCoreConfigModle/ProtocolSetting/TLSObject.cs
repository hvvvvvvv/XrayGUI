using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class TLSObject 
    {
        /// <summary>
        /// 指定服务器端证书的域名，在连接由 IP 建立时有用。
        /// 当目标连接由域名指定时，比如在 Socks 入站接收到了域名，或者由 Sniffing 功能探测出了域名，
        /// 这个域名会自动用于 serverName，无须手动配置。
        /// </summary>
        public string? serverName { get; set; }
        /// <summary>
        /// 当值为 true 时，服务端接收到的 SNI 与证书域名不匹配即拒绝 TLS 握手，默认为 false。
        /// </summary>
        public bool? rejectUnknownSni { get; set; }
        /// <summary>
        /// 是否允许不安全连接（仅用于客户端）。默认值为 false。
        /// 当值为 true 时，Xray 不会检查远端主机所提供的 TLS 证书的有效性。
        /// </summary>
        public bool? allowInsecure { get; set; }
        /// <summary>
        /// 指定了 TLS 握手时指定的 ALPN 数值。默认值为 ["h2", "http/1.1"]。
        /// </summary>
        public List<string>? alpn { get; set; }
        /// <summary>
        /// minVersion 为可接受的最小 SSL/TLS 版本。
        /// </summary>
        public string? minVersion { get; set; }
        /// <summary>
        /// maxVersion 为可接受的最大 SSL/TLS 版本。
        /// </summary>
        public string? maxVersion { get; set; }
        /// <summary>
        /// 此处填写你需要的加密套件名称,每个套件名称之间用:进行分隔
        /// </summary>
        public string? cipherSuites { get; set; }
        /// <summary>
        /// 证书列表，其中每一项表示一个证书（建议 fullchain）
        /// </summary>
        public List<CertificateObject>? certificates { get; set; }
        /// <summary>
        /// 是否禁用操作系统自带的 CA 证书。默认值为 false。
        /// 当值为 true 时，Xray 只会使用 certificates 中指定的证书进行 TLS 握手。
        /// 当值为 false 时，Xray 只会使用操作系统自带的 CA 证书进行 TLS 握手。
        /// </summary>
        public bool? disableSystemRoot { get; set; }
        /// <summary>
        /// 此参数的设置为 false 时, ClientHello 里没有 session_ticket 这个扩展。 
        /// 通常来讲 go 语言程序的 ClientHello 里并没有用到这个扩展, 
        /// 因此建议保持默认值。 默认值为 false。
        /// </summary>
        public bool? enableSessionResumption { get; set; }
        /// <summary>
        /// 此参数用于配置指定 TLS Client Hello 的指纹。当其值为空时，表示不启用此功能。
        /// 启用后，Xray 将通过 uTLS 库 模拟 TLS 指纹，或随机生成。支持三种配置方式：
        /// 1、常见浏览器最新版本的 TLS 指纹 包括
        ///     "chrome","firefox","safari","ios""android","edge""360","qq"
        /// 2、在 xray 启动时自动生成一个指纹
        ///     "random": 在较新版本的浏览器里随机抽取一个
        ///     "randomized": 完全随机生成一个独一无二的指纹 (100% 支持 TLS 1.3 使用 X25519)
        /// 3、使用 uTLS 原生指纹变量名 例如"HelloRandomizedNoALPN" "HelloChrome_106_Shuffle"。
        ///     完整名单见 uTLS 库：https://github.com/refraction-networking/utls/blob/master/u_common.go#L162
        /// </summary>
        public string? fingerprint { get; set; }
        /// <summary>
        /// 用于指定远程服务器的证书链 SHA256 散列值，使用标准编码格式。仅有当服务器端证书链散列值符合设置项中之一时才能成功建立 TLS 连接。
        /// 在连接因为此配置失败时，会展示远程服务器证书散列值。
        /// </summary>
        public List<string>? pinnedPeerCertificateChainSha256 { get; set; }
    }

    public class CertificateObject 
    {
        /// <summary>
        /// OCSP 装订更新，与证书热重载的时间间隔。 单位：秒。默认值为 3600，即一小时。
        /// </summary>
        public int? ocspStapling { get; set; }
        /// <summary>
        /// 仅加载一次。值为 true 时将关闭证书热重载功能与 ocspStapling 功能。当值为 true 时，将会关闭 OCSP 装订
        /// </summary>
        public bool? oneTimeLoading { get; set; }
        /// <summary>
        /// "encipherment" | "verify" | "issue"
        /// 证书用途，默认值为 "encipherment"
        /// "encipherment"：证书用于 TLS 认证和加密。
        /// "verify"：证书用于验证远端 TLS 的证书。当使用此项时，当前证书必须为 CA 证书。
        /// "issue"：证书用于签发其它证书。当使用此项时，当前证书必须为 CA 证书。
        /// </summary>
        public string? usage { get; set; }
        /// <summary>
        /// 证书文件路径，如使用 OpenSSL 生成，后缀名为 .crt。
        /// </summary>
        public string? certificateFile { get; set; }
        /// <summary>
        /// 密钥文件路径，如使用 OpenSSL 生成，后缀名为 .key。目前暂不支持需要密码的 key 文件。
        /// </summary>
        public string? keyFile { get; set; }
        /// <summary>
        /// 一个字符串数组，表示证书内容，格式如样例所示。certificate 和 certificateFile 二者选一
        /// </summary>
        public List<string>? certificate { get; set; }
        /// <summary>
        /// 一个字符串数组，表示密钥内容，格式如样例如示。key 和 keyFile 二者选一。
        /// </summary>
        public List<string>? key { get; set; }
    }
}

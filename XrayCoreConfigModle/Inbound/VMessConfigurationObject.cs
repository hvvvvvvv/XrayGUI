using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class VMessConfigurationObject: InboundConfigurationObject
    {
        /// <summary>
        /// 代表一组服务端认可的用户.
        /// </summary>
        public List<VMessClientsObject>? clients { get; set; }
        /// <summary>
        /// 指示对应的出站协议使用另一个服务器。
        /// </summary>
        [JsonPropertyName("default")]
        public DefaultClientsObject? default_ { get; set; }
        /// <summary>
        /// 可选，clients 的默认配置。仅在配合detour时有效。
        /// </summary>
        public DetourObject? detour { get; set; }
        /// <summary>
        /// 是否禁止客户端使用不安全的加密方式，如果设置为 true 当客户端指定下列加密方式时，服务器会主动断开连接。
        /// "none"
        /// "aes-128-cfb"
        /// </summary>
        public bool? disableInsecureEncryption { get; set; }
    }
    public class DetourObject 
    {
        /// <summary>
        /// 一个 inbound 的tag, 指定的 inbound 的必须是使用 VMess 协议的 inbound.
        /// </summary>
        public string? to { get; set; }
    }
    public class DefaultClientsObject 
    {
        /// <summary>
        ///  对应 policy 中 level 的值。 如不指定, 默认为 0。
        /// </summary>
        public int? level { get; set; }
        /// <summary>
        /// 动态端口的默认alterId，默认值为0。
        /// </summary>
        public int? alterId { get; set; }
    }
    public class VMessClientsObject 
    {
        /// <summary>
        /// Vmess 的用户 ID，可以是任意小于 30 字节的字符串, 也可以是一个合法的 UUID.
        /// </summary>
        public string? id { get; set; }
        /// <summary>
        /// 对应 policy 中 level 的值。 如不指定, 默认为 0。
        /// </summary>
        public int? level { get; set; }
        /// <summary>
        /// 为了进一步防止被探测，一个用户可以在主 ID 的基础上，再额外生成多个 ID。这里只需要指定额外的 ID 的数量，
        /// 推荐值为 0 代表启用 VMessAEAD。 最大值 65535。这个值不能超过服务器端所指定的值。
        /// </summary>
        public int? alterId { get; set; }
        /// <summary>
        /// 用户邮箱地址，用于区分不同用户的流量。
        /// </summary>
        public string? email { get; set; }
    }
}

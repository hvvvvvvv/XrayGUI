using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.OutBound
{
    public class VMessConfigurationObject: OutboundConfigurationObject
    {
        /// <summary>
        /// 包含一组的服务端配置.
        /// </summary>
        public List<VMessConfigurationObject>? vnext { get; set; }
    }
    public class VMessUserObject 
    {

        /// <summary>
        /// Vmess 的用户 ID，可以是任意小于 30 字节的字符串, 也可以是一个合法的 UUID.
        /// </summary>
        public string? id { get; set; }
        /// <summary>
        /// 为了进一步防止被探测，一个用户可以在主 ID 的基础上，再额外生成多个 ID。这里只需要指定额外的 ID 的数量，推荐值为 0 代表启用 VMessAEAD。 
        /// 最大值 65535。这个值不能超过服务器端所指定的值。
        /// 不指定的话，默认值是 0。
        /// </summary>
        public int? alterId { get; set; }
        /// <summary>
        /// 加密方式，客户端将使用配置的加密方式发送数据，服务器端自动识别，无需配置。
        ///     "aes-128-gcm"：推荐在 PC 上使用
        ///     "chacha20-poly1305"：推荐在手机端使用
        ///     "auto"：默认值，自动选择（运行框架为 AMD64、ARM64 或 s390x 时为 aes-128-gcm 加密方式，其他情况则为 Chacha20-Poly1305 加密方式）
        ///     "none"：不加密
        ///     "zero"：不加密，也不进行消息认证 (v1.4.0+)
        /// </summary>
        public string? security { get; set; }
        /// <summary>
        /// 用户等级，连接会使用这个用户等级对应的 本地策略。
        /// </summary>
        public int? level { get; set; }
    }
}

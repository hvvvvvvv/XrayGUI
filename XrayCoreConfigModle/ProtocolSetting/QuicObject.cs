using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class QuicObject 
    {
        /// <summary>
        /// 加密方式:"none" | "aes-128-gcm" | "chacha20-poly1305"
        /// 此加密是对 QUIC 数据包的加密，加密后数据包无法被探测。
        /// 默认值为不加密。
        /// </summary>
        public string? security { get; set; }
        /// <summary>
        /// 加密时所用的密钥。
        /// 可以是任意字符串。当 security 不为 "none" 时有效。
        /// </summary>
        public string? key { get; set; }
        /// <summary>
        /// 可以是任意字符串。当 security 不为 "none" 时有效。
        /// </summary>
        public QuicHeaderObject? header { get; set; }
    }


    public class QuicHeaderObject 
    {
        /// <summary>
        /// 伪装类型，可选的值参阅：https://github.com/XTLS/Xray-docs-next/blob/main/docs/config/transports/quic.md#headerobject
        /// </summary>
        public string? type { get; set; }
    }
}

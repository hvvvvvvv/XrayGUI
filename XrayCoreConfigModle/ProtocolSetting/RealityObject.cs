using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class RealityObject: TLSObject
    {
        /// <summary>
        /// 选填，若为 true，输出调试信息
        /// </summary>
        public bool? show { get; set; }
        /// <summary>
        ///  必填，格式同 VLESS fallbacks 的 dest
        /// </summary>
        public string? dest { get; set; }
        /// <summary>
        ///  选填，格式同 VLESS fallbacks 的 xver
        /// </summary>
        public int? xver { get; set; }
        /// <summary>
        /// 客户端可用的 serverName 列表，暂不支持 * 通配符
        /// </summary>
        public List<string>? serverNames { get; set; }
        /// <summary>
        /// 必填，执行 ./xray x25519 生成
        /// </summary>
        public string? privateKey { get; set; }
        /// <summary>
        /// 必填，执行 ./xray x25519 生成
        /// </summary>
        public string? minClientVer { get; set; }
        /// <summary>
        /// 选填，客户端 Xray 最高版本，格式为 x.y.z
        /// </summary>
        public string? maxClientVer { get; set; }
        /// <summary>
        ///  选填，允许的最大时间差，单位为毫秒
        /// </summary>
        public int? maxTimeDiff { get; set; }
        /// <summary>
        /// 必填，客户端可用的 shortId 列表，可用于区分不同的客户端，若其中有元素为""，则客户端 shortId 可为空
        /// </summary>
        public List<string>? shortIds { get; set; }
        /// <summary>
        /// 客户端 shortId 可为空
        /// </summary>
       
        ///客户端选项
        public string? publicKey { get; set; }
        /// <summary>
        /// 服务端 shortIds 之一
        /// </summary>
        public string? shortId { get; set; }
        /// <summary>
        /// 爬虫初始路径与参数，建议每个客户端不同
        /// </summary>
        public string? spiderX { get; set; }
    }
}

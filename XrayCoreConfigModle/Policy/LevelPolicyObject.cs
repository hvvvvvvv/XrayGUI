using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Policy
{
    public class LevelPolicyObject 
    {
        /// <summary>
        /// 连接建立时的握手时间限制。单位为秒。默认值为 4。
        /// 在入站代理处理一个新连接时，在握手阶段如果使用的时间超过这个时间，则中断该连接。
        /// </summary>
        public int? handshake { get; set; }
        /// <summary>
        /// 连接空闲的时间限制。单位为秒。默认值为 300。
        /// inbound/outbound 处理一个连接时，如果在 connIdle 时间内，
        /// 没有任何数据被传输（包括上行和下行数据），则中断该连接。
        /// </summary>
        public int? connIdle { get; set; }
        /// <summary>
        /// 当连接下行线路关闭后的时间限制。单位为秒。默认值为 2。
        /// 当服务器（如远端网站）关闭下行连接时，出站代理会在等待 uplinkOnly 时间后中断连接。
        /// </summary>
        public int? uplinkOnly { get; set; }
        /// <summary>
        /// 当连接上行线路关闭后的时间限制。单位为秒。默认值为 5。
        /// 当客户端（如浏览器）关闭上行连接时，入站代理会在等待 downlinkOnly 时间后中断连接。
        /// </summary>
        public int? downlinkOnly { get; set; }
        /// <summary>
        /// 当值为 true 时，开启当前等级的所有用户的上行流量统计。
        /// </summary>
        public bool? statsUserUplink { get; set; }
        /// <summary>
        /// 当值为 true 时，开启当前等级的所有用户的上行流量统计。
        /// </summary>
        public bool? statsUserDownlink { get; set; }
        /// <summary>
        /// 每个连接的内部缓存大小。单位为 kB。当值为 0 时，内部缓存被禁用。
        /// 在 ARM、MIPS、MIPSLE 平台上，默认值为 0。
        /// 在 ARM64、MIPS64、MIPS64LE 平台上，默认值为 4。
        /// 在其它平台上，默认值为 512。
        /// </summary>
        public int? bufferSize { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class KcpObject 
    {
        /// <summary>
        /// 最大传输单元（maximum transmission unit） 请选择一个介于 576 - 1460 之间的值。
        /// 默认值为 1350。
        /// </summary>
        public int? mtu { get; set; }
        /// <summary>
        /// 传输时间间隔（transmission time interval），单位毫秒（ms），mKCP 将以这个时间频率发送数据。 请选译一个介于 10 - 100 之间的值。
        /// 默认值为 50。
        /// </summary>
        public int? tti { get; set; }
        /// <summary>
        /// 上行链路容量，即主机发出数据所用的最大带宽，单位 MB/s，注意是 Byte 而非 bit。 可以设置为 0，表示一个非常小的带宽。
        /// 默认值 5。
        /// </summary>
        public int? uplinkCapacity { get; set; }
        /// <summary>
        /// 下行链路容量，即主机接收数据所用的最大带宽，单位 MB/s，注意是 Byte 而非 bit。 可以设置为 0，表示一个非常小的带宽。
        /// 默认值 20。
        /// </summary>
        public int? downlinkCapacity { get; set; }
        /// <summary>
        /// 是否启用拥塞控制。
        /// 开启拥塞控制之后，Xray 会自动监测网络质量，当丢包严重时，会自动降低吞吐量；当网络畅通时，也会适当增加吞吐量。
        /// 默认值为 false
        /// </summary>
        public bool? congestion { get; set; }
        /// <summary>
        /// 单个连接的读取缓冲区大小，单位是 MB。
        /// 默认值为 2
        /// </summary>
        public int? readBufferSize { get; set; }
        /// <summary>
        /// 单个连接的写入缓冲区大小，单位是 MB
        /// 默认值为 2
        /// </summary>
        public int? writeBufferSize { get; set; }
        /// <summary>
        /// 数据包头部伪装设置
        /// </summary>
        public KcpHeaderObject? header { get; set; }
        /// <summary>
        /// 可选的混淆密码，使用 AES-128-GCM 算法混淆流量数据，客户端和服务端需要保持一致
        /// 本混淆机制不能用于保证通信内容的安全，但可能可以对抗部分封锁。
        /// </summary>
        public string? seed { get; set; }
    }
    public class KcpHeaderObject 
    {
        /// <summary>
        /// 伪装类型
        /// 可选值参阅：https://github.com/XTLS/Xray-docs-next/blob/main/docs/config/transports/mkcp.md#headerobject
        /// </summary>
        public string? type { get; set; }
    }
}

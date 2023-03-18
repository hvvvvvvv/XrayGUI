using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class GRPCObject 
    {
        /// <summary>
        /// 指定服务名称，类似于 HTTP/2 中的 Path。 客户端会使用此名称进行通信，服务端会验证服务名称是否匹配。
        /// </summary>
        public string? serviceName { get; set; }
        /// <summary>
        /// true 启用 multiMode，默认值为： false。
        /// 这是一个 实验性 选项，可能不会被长期保留，也不保证跨版本兼容。
        /// 此模式在 测试环境中 能够带来约 20% 的性能提升，实际效果因传输速率不同而不同。
        /// </summary>
        public bool? multiMode { get; set; }
        /// <summary>
        /// 单位秒，当这段时间内没有数据传输时，将会进行健康检查。如果此值设置为 10 以下，将会使用 10，即最小值。
        /// </summary>
        public int? idle_timeout { get; set; }
        /// <summary>
        /// 单位秒，健康检查的超时时间。如果在这段时间内没有完成健康检查，且仍然没有数据传输时，即认为健康检查失败。默认值为 20。
        /// </summary>
        public int? health_check_timeout { get; set; }
        /// <summary>
        /// true 允许在没有子连接时进行健康检查。默认值为 false。
        /// </summary>
        public bool? permit_without_stream { get; set; }
        /// <summary>
        /// h2 Stream 初始窗口大小。当值小于等于 0 时，此功能不生效。
        /// 当值大于 65535 时，动态窗口机制（Dynamic Window）会被禁用。
        /// 默认值为0，即不生效。
        /// </summary>
        public int? initial_windows_size { get; set; }
    }
}

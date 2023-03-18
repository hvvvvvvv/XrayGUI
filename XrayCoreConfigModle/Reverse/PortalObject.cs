using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Reverse
{
    public class PortalObject 
    {    /// <summary>
         /// portal 的标识。在 路由配置 中使用 outboundTag 将流量转发到这个 portal。
         /// </summary>
        public string? tag { get; set; }
        /// <summary>
        /// 一个域名。当 portal 接收到流量时，如果流量的目标域名是此域名，则 portal 认为当前连接上 bridge 发来的通信连接。
        /// 而其它流量则会被当成需要转发的流量。portal 所做的工作就是把这两类连接进行识别并拼接。
        /// </summary>
        public string? domain { get; set; }
    }
}

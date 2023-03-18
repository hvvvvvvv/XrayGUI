using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Reverse
{
    public class BridgeObject 
    {
        /// <summary>
        /// 所有由 bridge 发出的连接，都会带有这个标识。可以在 路由配置 中使用 inboundTag 进行识别。
        /// </summary>
        public string? tag { get; set; }
        /// <summary>
        /// 指定一个域名，bridge 向 portal 建立的连接，都会使用这个域名进行发送。 这个域名只作为 bridge 和 portal 的通信用途，不必真实存在。
        /// </summary>
        public string? domain { get; set; }
    }
}

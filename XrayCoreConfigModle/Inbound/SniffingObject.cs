using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class SniffingObject 
    {
        /// <summary>
        /// 是否开启流量探测
        /// </summary>
        public bool? enabled { get; set; }
        /// <summary>
        /// 当流量为指定类型时，按其中包括的目标地址重置当前连接的目标。
        /// </summary>
        public List<string>? destOverride { get; set; }
        /// <summary>
        /// 当启用时，将仅使用连接的元数据嗅探目标地址。此时，除 fakedns 以外的 sniffer 将不能激活（包括 fakedns+others）。
        /// 如果关闭仅使用元数据推断目标地址，此时客户端必须先发送数据，代理服务器才会实际建立连接。
        /// 此行为与需要服务器首先发起第一个消息的协议不兼容，如 SMTP 协议。
        /// </summary>
        public bool? metadataOnly { get; set; }
        /// <summary>
        /// 一个域名列表，如果流量探测结果在这个列表中时，将 不会 重置目标地址
        /// </summary>
        public List<string>? domainsExcluded { get; set; }
        /// <summary>
        /// 将嗅探得到的域名仅用于路由，代理目标地址仍为 IP。默认值为 false
        /// </summary>
        public bool? routeOnly { get; set; }
    }
}

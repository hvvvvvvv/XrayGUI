using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class HttpObject 
    {
        /// <summary>
        /// 一个字符串数组，每一个元素是一个域名。
        /// 客户端会随机从列表中选出一个域名进行通信，服务器会验证域名是否在列表中。
        /// </summary>
        public List<string>? host { get; set; }
        /// <summary>
        /// HTTP 路径，由 / 开头, 客户端和服务器必须一致。
        /// 默认值为 "/"。
        /// </summary>
        public string? path { get; set; }
        /// <summary>
        /// 单位秒，当这段时间内没有接收到数据时，将会进行健康检查。
        /// 健康检查默认不启用。
        /// </summary>
        public int? read_idle_timeout { get; set; }
        /// <summary>
        /// 单位秒，健康检查的超时时间。如果在这段时间内没有完成健康检查，即认为健康检查失败。默认值为 15。
        /// </summary>
        public int? health_check_timeout { get; set; }
        /// <summary>
        /// HTTP 方法。默认值为 "PUT"。
        /// </summary>
        public string? method { get; set; }
        /// <summary>
        /// 自定义 HTTP 头，每个键表示一个 HTTP 头名称，对应值为一个数组。
        /// </summary>
        public Dictionary<string,List<string>>? headers { get; set; }
    }
}

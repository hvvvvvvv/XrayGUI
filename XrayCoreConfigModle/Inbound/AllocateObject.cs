using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class AllocateObject
    {
        /// <summary>
        /// "always" | "random"
        /// 端口分配策略:
        ///     "always" 表示总是分配所有已指定的端口，port 中指定了多少个端口，Xray 就会监听这些端口。
        ///     "random" 表示随机开放端口，每隔 refresh 分钟在 port 范围中随机选取 concurrency 个端口来监听。
        /// </summary>
        public string? strategy { get; set; }
        /// <summary>
        /// 随机端口刷新间隔，单位为分钟。最小值为 2，建议值为 5。这个属性仅当 strategy 设置为 "random" 时有效。
        /// </summary>
        public int? refresh { get; set; }
        /// <summary>
        /// 随机端口数量。最小值为 1，最大值为 port 范围的三分之一。建议值为 3。
        /// </summary>
        public int? concurrency { get; set; }
    }
}

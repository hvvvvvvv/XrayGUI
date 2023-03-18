using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Routing
{
    public class BalancerObject 
    {
        /// <summary>
        /// 此负载均衡器的标识，用于匹配 RuleObject 中的 balancerTag
        /// </summary>
        public string? tag { get; set; }
        /// <summary>
        /// 其中每一个字符串将用于和 outbound 标识的前缀匹配。
        /// 在以下几个 outbound 标识中：[ "a", "ab", "c", "ba" ]，"selector": ["a"] 将匹配到 [ "a", "ab" ]。
        /// 如果匹配到多个 outbound，负载均衡器目前会从中随机选出一个作为最终的 outbound。
        /// </summary>
        public List<string>? selector { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle
{
    public class PolicyObject 
    {
        /// <summary>
        /// 每个键是一个字符串形式的数字（JSON 的要求），
        /// 此数字对应用户等级。每一个值是一个 LevelPolicyObject.
        /// </summary>
        public Dictionary<string,Policy.LevelPolicyObject>? levels { get; set; }
        /// <summary>
        /// Xray 系统级别的策略
        /// </summary>
        public Policy.SystemPolicyObject? system { get; set; }
    }
}

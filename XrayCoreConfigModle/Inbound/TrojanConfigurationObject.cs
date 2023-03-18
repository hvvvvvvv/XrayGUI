using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    public class TrojanConfigurationObject: InboundConfigurationObject
    {
        /// <summary>
        /// 代表一组服务端认可的用户.
        /// </summary>
        public List<TrojanClientsObject>? clients { get; set; }
        /// <summary>
        /// 包含一系列强大的回落分流配置（可选）
        /// </summary>
        public List<FallbackObject>? fallbacks { get; set; }
    }

    public class TrojanClientsObject 
    {
        /// <summary>
        /// 必填，任意字符串。
        /// </summary>
        public string? password { get; set; }
        /// <summary>
        /// 邮件地址，可选，用于标识用户
        /// 如果存在多个 ClientObject, 请注意 email 不可以重复
        /// </summary>
        public string? email { get; set; }
        /// <summary>
        /// 对应 policy 中 level 的值。 如不指定, 默认为 0。
        /// </summary>
        public int? level { get; set; }
    }
}

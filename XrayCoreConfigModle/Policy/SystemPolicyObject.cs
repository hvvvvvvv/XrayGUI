using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Policy
{
    public class SystemPolicyObject 
    {
        /// <summary>
        /// 当值为 true 时，开启所有入站代理的上行流量统计。
        /// </summary>
        public bool? statsInboundUplink { get; set; }
        /// <summary>
        /// 当值为 true 时，开启所有入站代理的下行流量统计。
        /// </summary>
        public bool? statsInboundDownlink { get; set; }
        /// <summary>
        /// 当值为 true 时，开启所有出站代理的上行流量统计。
        /// </summary>
        public bool? statsOutboundUplink { get; set; }
        /// <summary>
        /// 当值为 true 时，开启所有出站代理的下行流量统计。
        /// </summary>
        public bool? statsOutboundDownlink { get; set; }
    }
}

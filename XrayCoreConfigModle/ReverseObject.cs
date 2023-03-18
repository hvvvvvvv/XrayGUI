using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle
{
    public class ReverseObject 
    {
        /// <summary>
        /// 每一项表示一个 bridge。每个 bridge 的配置是一个 BridgeObject。
        /// </summary>
        public List<Reverse.BridgeObject>? bridges { get; set; }
        /// <summary>
        /// 每一项表示一个 portal。每个 portal 的配置是一个 PortalObject。
        /// </summary>
        public List<Reverse.PortalObject>? portals { get; set; }
    }
}

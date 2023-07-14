using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle.OutBound;

namespace NetProxyController.Modle.Server
{
    public abstract class OutBoundConfiguration
    {
        abstract public OutboundConfigurationObject ToOutboundConfigurationObject(string addr, int port);
    }
}

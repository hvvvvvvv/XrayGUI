using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.Inbound;
using XrayCoreConfigModle.JsonConverters;

namespace XrayCoreConfigModle.OutBound
{
    [JsonConverter(typeof(OutboundConfigurationObjectConverter))]
    public abstract class OutboundConfigurationObject 
    {
    }
}

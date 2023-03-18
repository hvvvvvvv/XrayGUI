using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Inbound
{
    [JsonConverter(typeof(JsonConverters.InboundConfigurationConverter))]
    public abstract class InboundConfigurationObject 
    {
    }
}

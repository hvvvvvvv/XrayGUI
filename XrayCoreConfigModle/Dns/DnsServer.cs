using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.Dns
{
    [JsonConverter(typeof(JsonConverters.DnsServerConverter))]
    public abstract class DnsServer
    {
        public DnsServer()
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class TrojanOutboundOptions : OutboundTLSOptionsContainer
    {
        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("multiplex")]
        public OutboundMultiplexOptions? Multiplex { get; set; }

        [JsonPropertyName("transport")]
        public V2RayTransportOptions? Transport { get; set; }
    }
}

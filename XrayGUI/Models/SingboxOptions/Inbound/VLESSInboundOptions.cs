using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class VLESSInboundOptions : InboundTLSOptionsContainer
    {
        [JsonPropertyName("users")]
        public List<VLESSUser>? Users { get; set; }

        [JsonPropertyName("multiplex")]
        public InboundMultiplexOptions? Multiplex { get; set; }

        [JsonPropertyName("transport")]
        public V2RayTransportOptions? Transport { get; set; }

    }
    public class VLESSUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("uuid")]
        public string? UUID { get; set; }

        [JsonPropertyName("flow")]
        public string? Flow { get; set; }
    }
}

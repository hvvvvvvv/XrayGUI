using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class VMessInboundOptions: InboundTLSOptionsContainer
    {
        [JsonPropertyName("users")]
        public List<VMessUser>? Users { get; set; }

        [JsonPropertyName("multiplex")]
        public InboundMultiplexOptions? Multiplex { get; set; }

        [JsonPropertyName("transport")]
        public V2RayTransportOptions? Transport { get; set; }
    }
    public class VMessUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("uuid")]
        public string? UUID { get; set; }

        [JsonPropertyName("alterId")]
        public int? AlterId { get; set; }
    }
}

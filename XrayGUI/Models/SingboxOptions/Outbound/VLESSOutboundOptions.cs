using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class VLESSOutboundOptions : OutboundTLSOptionsContainer
    {
        [JsonPropertyName("uuid")]
        public string? UUID { get; set; }

        [JsonPropertyName("flow")]
        public string? Flow { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("multiplex")]
        public OutboundMultiplexOptions? Multiplex { get; set; }

        [JsonPropertyName("transport")]
        public V2RayTransportOptions? Transport { get; set; }

        [JsonPropertyName("packet_encoding")]
        public string? PacketEncoding { get; set; }
    }
}

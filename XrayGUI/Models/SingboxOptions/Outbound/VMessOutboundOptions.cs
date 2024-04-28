using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class VMessOutboundOptions : OutboundTLSOptionsContainer
    {
        [JsonPropertyName("uuid")]
        public string? UUID { get; set; }

        [JsonPropertyName("security")]
        public string? Security { get; set; }

        [JsonPropertyName("alter_id")]
        public int? AlterId { get; set; }

        [JsonPropertyName("global_padding")]
        public bool? GlobalPadding { get; set; }

        [JsonPropertyName("authenticated_length")]
        public bool? AuthenticatedLength { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("packet_encoding")]
        public string? PacketEncoding { get; set; }

        [JsonPropertyName("multiplex")]
        public OutboundMultiplexOptions? Multiplex { get; set; }

        [JsonPropertyName("transport")]
        public V2RayTransportOptions? Transport { get; set; }
    }
}

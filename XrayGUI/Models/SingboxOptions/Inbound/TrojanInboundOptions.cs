using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class TrojanInboundOptions : InboundTLSOptionsContainer
    {
        [JsonPropertyName("users")]
        public List<TrojanUser>? Users { get; set; }

        [JsonPropertyName("fallback")]
        public ServerOptions? Fallback { get; set; }

        [JsonPropertyName("fallback_for_alpn")]
        public Dictionary<string, ServerOptions>? FallbackForALPN { get; set; }

        [JsonPropertyName("multiplex")]
        public InboundMultiplexOptions? Multiplex { get; set; }

        [JsonPropertyName("transport")]
        public V2RayTransportOptions? Transport { get; set; }
    }
    public class TrojanUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}

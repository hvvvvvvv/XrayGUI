using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class ShadowsocksROutboundOptions : DialerWithServerOptions
    {
        [JsonPropertyName("method")]
        public string? Method { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("obfs")]
        public string? Obfs { get; set; }

        [JsonPropertyName("obfs_param")]
        public string? ObfsParam { get; set; }

        [JsonPropertyName("protocol")]
        public string? Protocol { get; set; }

        [JsonPropertyName("protocol_param")]
        public string? ProtocolParam { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class SocksOutboundOptions : OutboundMultiplexOptions
    {
        [JsonPropertyName("version")]
        public string? Version { get; set; }

        [JsonPropertyName("username")]
        public string? Username { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("udp_over_tcp")]
        public UDPOverTCPOptions? UDPOverTCP { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class ShadowsocksOutboundOptions : DialerWithServerOptions
    {
        [JsonPropertyName("method")]
        public string? Method { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("plugin")]
        public string? Plugin { get; set; }

        [JsonPropertyName("plugin_opts")]
        public string? PluginOptions { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("udp_over_tcp")]
        public UDPOverTCPOptions? UDPOverTCP { get; set; }

        [JsonPropertyName("multiplex")]
        public OutboundMultiplexOptions? Multiplex { get; set; }
    }
}

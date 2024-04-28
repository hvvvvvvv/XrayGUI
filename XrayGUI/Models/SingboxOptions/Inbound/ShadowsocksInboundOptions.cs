using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class ShadowsocksInboundOptions : ListenOptions
    {
        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("method")]
        public string? Method { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("users")]
        public List<ShadowsocksUser>? Users { get; set; }

        [JsonPropertyName("destinations")]
        public List<ShadowsocksDestination>? Destinations { get; set; }

        [JsonPropertyName("multiplex")]
        public InboundMultiplexOptions? Multiplex { get; set; }
    }
    public class ShadowsocksDestination : ServerOptions
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
    public class ShadowsocksUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}

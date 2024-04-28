using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class OutboundMultiplexOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("protocol")]
        public string? Protocol { get; set; }

        [JsonPropertyName("max_connections")]
        public int? MaxConnections { get; set; }

        [JsonPropertyName("min_streams")]
        public int? MinStreams { get; set; }

        [JsonPropertyName("max_streams")]
        public int? MaxStreams { get; set; }

        [JsonPropertyName("padding")]
        public bool? Padding { get; set; }

        [JsonPropertyName("brutal")]
        public BrutalOptions? Brutal { get; set; }
    }

}

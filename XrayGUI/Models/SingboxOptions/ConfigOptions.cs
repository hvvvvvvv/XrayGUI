using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.Dns;
using XrayGUI.Modle.SingboxOptions.Inbound;
using XrayGUI.Modle.SingboxOptions.Outbound;
using XrayGUI.Modle.SingboxOptions.Route;

namespace XrayGUI.Modle.SingboxOptions
{
    public class ConfigOptions
    {
        [JsonPropertyName("log")]
        public LogOptions? Log { get; set; }

        [JsonPropertyName("dns")]
        public DNSOptions? DNS { get; set; }

        [JsonPropertyName("ntp")]
        public NTPOptions? NTP { get; set; }

        [JsonPropertyName("inbounds")]
        public List<InboundOptions>? Inbounds { get; set; }

        [JsonPropertyName("outbounds")]
        public List<OutboundOptions>? Outbounds { get; set; }

        [JsonPropertyName("route")]
        public RouteOptions? Route { get; set; }

        [JsonPropertyName("experimental")]
        public ExperimentalOptions? Experimental { get; set; }
        public string? JsonSerialize()
        {
            return JsonSerializer.Serialize(this, new JsonSerializerOptions
            {
                WriteIndented = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });
        }
        public static ConfigOptions JsonDeserialize(string json)
        {
            return JsonSerializer.Deserialize<ConfigOptions>(json) ?? throw new Exception("Failed to deserialize JSON");
        }
    }
}

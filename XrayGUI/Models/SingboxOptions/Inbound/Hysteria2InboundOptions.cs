using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class Hysteria2InboundOptions : InboundTLSOptionsContainer
    {
        [JsonPropertyName("up_mbps")]
        public int? UpMbps { get; set; }

        [JsonPropertyName("down_mbps")]
        public int? DownMbps { get; set; }

        [JsonPropertyName("obfs")]
        public Hysteria2Obfs? Obfs { get; set; }

        [JsonPropertyName("users")]
        public List<Hysteria2User>? Users { get; set; }

        [JsonPropertyName("ignore_client_bandwidth")]
        public bool? IgnoreClientBandwidth { get; set; }

        [JsonPropertyName("masquerade")]
        public string? Masquerade { get; set; }

        [JsonPropertyName("brutal_debug")]
        public bool? BrutalDebug { get; set; }
    }

    public class Hysteria2User
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
    public class Hysteria2Obfs
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}

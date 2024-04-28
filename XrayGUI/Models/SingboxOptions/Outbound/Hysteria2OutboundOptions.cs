using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.Inbound;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class Hysteria2OutboundOptions : OutboundTLSOptionsContainer
    {
        [JsonPropertyName("up_mbps")]
        public int? UpMbps { get; set; }

        [JsonPropertyName("down_mbps")]
        public int? DownMbps { get; set; }

        [JsonPropertyName("obfs")]
        public Hysteria2Obfs? Obfs { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("brutal_debug")]
        public bool? BrutalDebug { get; set; }
    }
}

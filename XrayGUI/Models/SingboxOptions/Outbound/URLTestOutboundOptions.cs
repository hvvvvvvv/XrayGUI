using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class URLTestOutboundOptions
    {
        [JsonPropertyName("outbounds")]
        public List<string>? Outbounds { get; set; }

        [JsonPropertyName("url")]
        public string? URL { get; set; }

        [JsonPropertyName("interval")]
        public string? Interval { get; set; }

        [JsonPropertyName("tolerance")]
        public ushort? Tolerance { get; set; }

        [JsonPropertyName("idle_timeout")]
        public string? IdleTimeout { get; set; }

        [JsonPropertyName("interrupt_exist_connections")]
        public bool? InterruptExistConnections { get; set; }
    }
}

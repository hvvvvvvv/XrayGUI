using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class TUICInboundOptions : InboundTLSOptionsContainer
    {
        [JsonPropertyName("users")]
        public List<TUICUser>? Users { get; set; }

        [JsonPropertyName("congestion_control")]
        public string? CongestionControl { get; set; }

        [JsonPropertyName("auth_timeout")]
        public string? AuthTimeout { get; set; }

        [JsonPropertyName("zero_rtt_handshake")]
        public bool? ZeroRTTHandshake { get; set; }

        [JsonPropertyName("heartbeat")]
        public string? Heartbeat { get; set; }
    }
    public class TUICUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("uuid")]
        public string? UUID { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class TUICOutboundOptions : OutboundTLSOptionsContainer
    {
        [JsonPropertyName("uuid")]
        public string? UUID { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("congestion_control")]
        public string? CongestionControl { get; set; }

        [JsonPropertyName("udp_relay_mode")]
        public string? UDPRelayMode { get; set; }

        [JsonPropertyName("udp_over_stream")]
        public bool? UDPOverStream { get; set; }

        [JsonPropertyName("zero_rtt_handshake")]
        public bool? ZeroRTTHandshake { get; set; }

        [JsonPropertyName("heartbeat")]
        public string? Heartbeat { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }
    }
}

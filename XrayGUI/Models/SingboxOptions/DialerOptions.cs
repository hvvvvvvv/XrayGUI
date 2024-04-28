using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace XrayGUI.Modle.SingboxOptions
{
    public class DialerOptions
    {
        [JsonPropertyName("detour")]
        public string? Detour { get; set; }

        [JsonPropertyName("bind_interface")]
        public string? BindInterface { get; set; }

        [JsonPropertyName("inet4_bind_address")]
        public string? Inet4BindAddress { get; set; }

        [JsonPropertyName("inet6_bind_address")]
        public string? Inet6BindAddress { get; set; }

        [JsonPropertyName("protect_path")]
        public string? ProtectPath { get; set; }

        [JsonPropertyName("routing_mark")]
        public int? RoutingMark { get; set; }

        [JsonPropertyName("reuse_addr")]
        public bool? ReuseAddr { get; set; }

        [JsonPropertyName("connect_timeout")]
        public Duration? ConnectTimeout { get; set; }

        [JsonPropertyName("tcp_fast_open")]
        public bool? TCPFastOpen { get; set; }

        [JsonPropertyName("tcp_multi_path")]
        public bool? TCPMultiPath { get; set; }

        [JsonPropertyName("udp_fragment")]
        public bool? UDPFragment { get; set; }

        // The field "UDPFragmentDefault" is ignored in JSON serialization.
        [JsonIgnore]
        public bool? UDPFragmentDefault { get; set; }

        [JsonPropertyName("domain_strategy")]
        public string? DomainStrategy { get; set; }

        [JsonPropertyName("fallback_delay")]
        public Duration? FallbackDelay { get; set; }
    }
}

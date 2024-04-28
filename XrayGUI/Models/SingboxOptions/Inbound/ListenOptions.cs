using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class ListenOptions : InboundBasicOptions
    {
        [JsonPropertyName("listen")]
        public string? Listen { get; set; }

        [JsonPropertyName("listen_port")]
        public ushort? ListenPort { get; set; }

        [JsonPropertyName("tcp_fast_open")]
        public bool? TCPFastOpen { get; set; }

        [JsonPropertyName("tcp_multi_path")]
        public bool? TCPMultiPath { get; set; }

        [JsonPropertyName("udp_fragment")]
        public bool? UDPFragment { get; set; }
        [JsonIgnore]
        public bool? UDPFragmentDefault { get; set; }

        [JsonPropertyName("udp_timeout")]
        public string? UDPTimeout { get; set; }

        [JsonPropertyName("proxy_protocol")]
        public bool? ProxyProtocol { get; set; }

        [JsonPropertyName("proxy_protocol_accept_no_header")]
        public bool? ProxyProtocolAcceptNoHeader { get; set; }

        [JsonPropertyName("detour")]
        public string? Detour { get; set; }
    }
}

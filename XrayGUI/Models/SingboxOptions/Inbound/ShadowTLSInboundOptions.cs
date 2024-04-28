using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class ShadowTLSInboundOptions : ListenOptions
    {
        [JsonPropertyName("version")]
        public int? Version { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("users")]
        public List<ShadowTLSUser>? Users { get; set; }

        [JsonPropertyName("handshake")]
        public ShadowTLSHandshakeOptions? Handshake { get; set; }

        [JsonPropertyName("handshake_for_server_name")]
        public Dictionary<string, ShadowTLSHandshakeOptions>? HandshakeForServerName { get; set; }

        [JsonPropertyName("strict_mode")]
        public bool? StrictMode { get; set; }
    }
    public class ShadowTLSUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
    public class ShadowTLSHandshakeOptions : DialerOptions
    {
        [JsonPropertyName("server")]
        public string? Server { get; set; }

        [JsonPropertyName("server_port")]
        public ushort? ServerPort { get; set; }
    }
}

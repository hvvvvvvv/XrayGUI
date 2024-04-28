using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class SSHOutboundOptions : DialerWithServerOptions
    {
        [JsonPropertyName("user")]
        public string? User { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }

        [JsonPropertyName("private_key")]
        public List<string>? PrivateKey { get; set; }

        [JsonPropertyName("private_key_path")]
        public string? PrivateKeyPath { get; set; }

        [JsonPropertyName("private_key_passphrase")]
        public string? PrivateKeyPassphrase { get; set; }

        [JsonPropertyName("host_key")]
        public List<string>? HostKey { get; set; }

        [JsonPropertyName("host_key_algorithms")]
        public List<string>? HostKeyAlgorithms { get; set; }

        [JsonPropertyName("client_version")]
        public string? ClientVersion { get; set; }
    }
}

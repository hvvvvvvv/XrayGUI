using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayCoreConfigModle.OutBound;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class WireGuardOutboundOptions : DialerWithServerOptions
    {
        [JsonPropertyName("system_interface")]
        public bool? SystemInterface { get; set; }

        [JsonPropertyName("gso")]
        public bool? GSO { get; set; }

        [JsonPropertyName("interface_name")]
        public string? InterfaceName { get; set; }

        [JsonPropertyName("local_address")]
        public List<string>? LocalAddress { get; set; }

        [JsonPropertyName("private_key")]
        public string? PrivateKey { get; set; }

        [JsonPropertyName("peers")]
        public List<WireGuardPeer>? Peers { get; set; }

        [JsonPropertyName("peer_public_key")]
        public string? PeerPublicKey { get; set; }

        [JsonPropertyName("pre_shared_key")]
        public string? PreSharedKey { get; set; }

        [JsonPropertyName("reserved")]
        public List<byte>? Reserved { get; set; }

        [JsonPropertyName("workers")]
        public int? Workers { get; set; }

        [JsonPropertyName("mtu")]
        public uint? MTU { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }
    }
    public class WireGuardPeer : ServerOptions
    {
        [JsonPropertyName("public_key")]
        public string? PublicKey { get; set; }

        [JsonPropertyName("pre_shared_key")]
        public string? PreSharedKey { get; set; }

        [JsonPropertyName("allowed_ips")]
        public List<string>? AllowedIPs { get; set; }

        [JsonPropertyName("reserved")]
        public List<byte>? Reserved { get; set; }
    }
}

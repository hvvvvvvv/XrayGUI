using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class HysteriaInboundOptions : InboundTLSOptionsContainer
    {
        [JsonPropertyName("up")]
        public string? Up { get; set; }

        [JsonPropertyName("up_mbps")]
        public int? UpMbps { get; set; }

        [JsonPropertyName("down")]
        public string? Down { get; set; }

        [JsonPropertyName("down_mbps")]
        public int? DownMbps { get; set; }

        [JsonPropertyName("obfs")]
        public string? Obfs { get; set; }

        [JsonPropertyName("users")]
        public List<HysteriaUser>? Users { get; set; }

        [JsonPropertyName("recv_window_conn")]
        public ulong? ReceiveWindowConn { get; set; }

        [JsonPropertyName("recv_window_client")]
        public ulong? ReceiveWindowClient { get; set; }

        [JsonPropertyName("max_conn_client")]
        public int? MaxConnClient { get; set; }

        [JsonPropertyName("disable_mtu_discovery")]
        public bool? DisableMTUDiscovery { get; set; }
    }
    public class HysteriaUser
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("auth")]
        public List<byte>? Auth { get; set; }

        [JsonPropertyName("auth_str")]
        public string? AuthString { get; set; }
    }
}

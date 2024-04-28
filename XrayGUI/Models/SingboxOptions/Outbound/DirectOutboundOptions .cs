using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class DirectOutboundOptions : DialerOptions
    {
        [JsonPropertyName("override_address")]
        public string? OverrideAddress { get; set; }

        [JsonPropertyName("override_port")]
        public ushort? OverridePort { get; set; }

        [JsonPropertyName("proxy_protocol")]
        public byte? ProxyProtocol { get; set; }
    }
}

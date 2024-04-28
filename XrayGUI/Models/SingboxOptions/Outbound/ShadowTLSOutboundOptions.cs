using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class ShadowTLSOutboundOptions : OutboundTLSOptionsContainer
    {
        [JsonPropertyName("version")]
        public int? Version { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}

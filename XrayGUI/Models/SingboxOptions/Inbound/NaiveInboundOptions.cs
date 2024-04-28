using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class NaiveInboundOptions : InboundTLSOptionsContainer
    {
        [JsonPropertyName("users")]
        public List<AuthUserOptions>? Users { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }
    }
}

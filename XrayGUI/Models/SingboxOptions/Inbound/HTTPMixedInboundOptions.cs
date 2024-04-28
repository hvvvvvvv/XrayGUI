using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class HTTPMixedInboundOptions : InboundTLSOptionsContainer
    {
        [JsonPropertyName("users")]
        public List<AuthUserOptions>? Users { get; set; }
        [JsonPropertyName("set_system_proxy")]
        public bool? SetSyetemProxy { get; set; }
    }
}

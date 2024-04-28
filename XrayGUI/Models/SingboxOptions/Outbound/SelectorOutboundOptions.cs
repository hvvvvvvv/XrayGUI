using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class SelectorOutboundOptions
    {
        [JsonPropertyName("outbounds")]
        public List<string>? Outbounds { get; set; }

        [JsonPropertyName("default")]
        public string? Default { get; set; }

        [JsonPropertyName("interrupt_exist_connections")]
        public bool? InterruptExistConnections { get; set; }
    }
}

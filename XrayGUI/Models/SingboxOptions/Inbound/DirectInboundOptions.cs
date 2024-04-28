using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class DirectInboundOptions : ListenOptions
    {
        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("override_address")]
        public string? OverrideAddress { get; set; }

        [JsonPropertyName("override_port")]
        public ushort? OverridePort { get; set; }

    }
}

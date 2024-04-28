using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions
{
    [JsonConverter(typeof(JsonConverter.UDPOverTCPOConverter))]
    public class UDPOverTCPOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("version")]
        public byte? Version { get; set; }
    }
}

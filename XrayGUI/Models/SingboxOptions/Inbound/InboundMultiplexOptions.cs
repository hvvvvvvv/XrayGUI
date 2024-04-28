using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class InboundMultiplexOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("padding")]
        public bool? Padding { get; set; }

        [JsonPropertyName("brutal")]
        public BrutalOptions? Brutal { get; set; }
    }
}

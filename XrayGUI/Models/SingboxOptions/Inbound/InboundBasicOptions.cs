using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class InboundBasicOptions
    {
        [JsonPropertyName("sniff")]
        public bool? SniffEnabled { get; set; }

        [JsonPropertyName("sniff_override_destination")]
        public bool? SniffOverrideDestination { get; set; }

        [JsonPropertyName("sniff_timeout")]
        public string? SniffTimeout { get; set; }

        [JsonPropertyName("domain_strategy")]
        public string? DomainStrategy { get; set; }

        [JsonPropertyName("udp_disable_domain_unmapping")]
        public bool? UDPDisableDomainUnmapping { get; set; }
    }
}

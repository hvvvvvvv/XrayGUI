using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Dns
{
    public class DnsServerOptions
    {
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }

        [JsonPropertyName("address")]
        public string? Address { get; set; }

        [JsonPropertyName("address_resolver")]
        public string? AddressResolver { get; set; }

        [JsonPropertyName("address_strategy")]
        public string? AddressStrategy { get; set; }

        [JsonPropertyName("strategy")]
        public string? Strategy { get; set; }

        [JsonPropertyName("detour")]
        public string? Detour { get; set; }

        [JsonPropertyName("client_subnet")]
        public string? ClientSubnet { get; set; }
    }
}

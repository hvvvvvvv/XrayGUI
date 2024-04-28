using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Dns
{
    public class DNSOptions
    {
        [JsonPropertyName("servers")]
        public List<DnsServerOptions>? Servers { get; set; }

        [JsonPropertyName("rules")]
        public List<DNSRuleOptions>? Rules { get; set; }

        [JsonPropertyName("final")]
        public string? Final { get; set; }

        [JsonPropertyName("strategy")]
        public string? Strategy { get; set; }

        [JsonPropertyName("disable_cache")]
        public bool? DisableCache { get; set; }

        [JsonPropertyName("disable_expire")]
        public bool? DisableExpire { get; set; }

        [JsonPropertyName("independent_cache")]
        public bool? IndependentCache { get; set; }

        [JsonPropertyName("reverse_mapping")]
        public bool? ReverseMapping { get; set; }

        [JsonPropertyName("client_subnet")]
        public string? ClientSubnet { get; set; }

        [JsonPropertyName("fakeip")]
        public FakeIPOptions? FakeIp { get; set; }
    }
}

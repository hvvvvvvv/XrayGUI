using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Dns
{
    public class FakeIPOptions
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("inet4_range")]
        public string? Inet4Range { get; set; }

        [JsonPropertyName("inet6_range")]
        public string? Inet6Range { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class TorOutboundOptions : DialerOptions
    {
        [JsonPropertyName("executable_path")]
        public string? ExecutablePath { get; set; }

        [JsonPropertyName("extra_args")]
        public List<string>? ExtraArgs { get; set; }

        [JsonPropertyName("data_directory")]
        public string? DataDirectory { get; set; }

        [JsonPropertyName("torrc")]
        public Dictionary<string, string>? Options { get; set; }
    }
}

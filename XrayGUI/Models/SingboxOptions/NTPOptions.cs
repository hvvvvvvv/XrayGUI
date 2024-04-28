using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace XrayGUI.Modle.SingboxOptions
{
    public class NTPOptions : DialerOptions
    {
        [JsonPropertyName("enabled")]
        public bool Enabled { get; set; }

        [JsonPropertyName("interval")]
        public string? Interval { get; set; }

        [JsonPropertyName("write_to_system")]
        public bool WriteToSystem { get; set; }
        [JsonPropertyName("server")]
        public string? Server { get; set; }

        [JsonPropertyName("server_port")]
        public ushort? ServerPort { get; set; }

    }
}

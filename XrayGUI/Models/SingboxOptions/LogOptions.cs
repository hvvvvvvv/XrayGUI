using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions
{
    public class LogOptions
    {
        [JsonPropertyName("disabled")]
        public bool? Disabled { get; set; }

        [JsonPropertyName("level")]
        public string? Level { get; set; }

        [JsonPropertyName("output")]
        public string? Output { get; set; }

        [JsonPropertyName("timestamp")]
        public bool? Timestamp { get; set; }
        [JsonIgnore]
        public bool? DisableColor { get; set;}
    }
}

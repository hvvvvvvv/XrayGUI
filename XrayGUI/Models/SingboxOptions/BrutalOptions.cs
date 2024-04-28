using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions
{
    public class BrutalOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("up_mbps")]
        public int? UpMbps { get; set; }

        [JsonPropertyName("down_mbps")]
        public int? DownMbps { get; set; }
    }
}

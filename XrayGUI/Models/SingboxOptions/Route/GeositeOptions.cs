using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Route
{
    public class GeositeOptions
    {
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("download_url")]
        public string? DownloadUrl { get; set; }

        [JsonPropertyName("download_detour")]
        public string? DownloadDetour { get; set; }
    }
}

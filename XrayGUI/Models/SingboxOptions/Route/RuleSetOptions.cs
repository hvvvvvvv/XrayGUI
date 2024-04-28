using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Route
{
    [JsonConverter(typeof(JsonConverter.RuleSetConverter))]
    public class RuleSetOptions
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonPropertyName("tag")]
        public string? Tag { get; set; }
        [JsonPropertyName("format")]
        public string? Format { get; set; }
        [JsonIgnore]
        public LocalRuleSetOptions? LocalRuleSet { get; set; }
        [JsonIgnore]
        public RemoteRuleSetOptions? RemoteRuleSet { get; set; }
    }
    public class LocalRuleSetOptions
    {
        [JsonPropertyName("path")]
        public string? Path { get; set; }
    }
    public class RemoteRuleSetOptions
    {
        [JsonPropertyName("url")]
        public string? Url { get; set; }

        [JsonPropertyName("download_detour")]
        public string? DownloadDetour { get; set; }

        [JsonPropertyName("update_interval")]
        public string? UpdateInterval { get; set; }
    }
}

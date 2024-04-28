using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace XrayGUI.Modle.SingboxOptions
{
    public class ExperimentalOptions
    {
        [JsonPropertyName("cache_file")]
        public CacheFileOptions? CacheFile { get; set; }

        [JsonPropertyName("clash_api")]
        public ClashAPIOptions? ClashAPI { get; set; }

        [JsonPropertyName("v2ray_api")]
        public V2RayAPIOptions? V2RayAPI { get; set; }

        [JsonPropertyName("debug")]
        public DebugOptions? Debug { get; set; }
    }
    public class CacheFileOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("cache_id")]
        public string? CacheID { get; set; }

        [JsonPropertyName("store_fakeip")]
        public bool? StoreFakeIP { get; set; }

        [JsonPropertyName("store_rdrc")]
        public bool? StoreRDRC { get; set; }

        [JsonPropertyName("rdrc_timeout")]
        public string? RDRCTimeout { get; set; }
    }
    public class ClashAPIOptions
    {
        [JsonPropertyName("external_controller")]
        public string? ExternalController { get; set; }

        [JsonPropertyName("external_ui")]
        public string? ExternalUI { get; set; }

        [JsonPropertyName("external_ui_download_url")]
        public string? ExternalUIDownloadURL { get; set; }

        [JsonPropertyName("external_ui_download_detour")]
        public string? ExternalUIDownloadDetour { get; set; }

        [JsonPropertyName("secret")]
        public string? Secret { get; set; }

        [JsonPropertyName("default_mode")]
        public string? DefaultMode { get; set; }

        [JsonIgnore]
        public List<string>? ModeList { get; set; }

        [JsonPropertyName("cache_file")]
        public string? CacheFile { get; set; }

        [JsonPropertyName("cache_id")]
        public string? CacheID { get; set; }

        [JsonPropertyName("store_mode")]
        public bool? StoreMode { get; set; }

        [JsonPropertyName("store_selected")]
        public bool? StoreSelected { get; set; }

        [JsonPropertyName("store_fakeip")]
        public bool? StoreFakeIP { get; set; }
    }
    public class V2RayAPIOptions
    {
        [JsonPropertyName("listen")]
        public string? Listen { get; set; }

        [JsonPropertyName("stats")]
        public V2RayStatsServiceOptions? Stats { get; set; }
    }
    public class V2RayStatsServiceOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("inbounds")]
        public List<string>? Inbounds { get; set; }

        [JsonPropertyName("outbounds")]
        public List<string>? Outbounds { get; set; }

        [JsonPropertyName("users")]
        public List<string>? Users { get; set; }
    }
    public class DebugOptions
    {
        [JsonPropertyName("listen")]
        public string? Listen { get; set; }

        [JsonPropertyName("gc_percent")]
        public int? GCPercent { get; set; }

        [JsonPropertyName("max_stack")]
        public int? MaxStack { get; set; }

        [JsonPropertyName("max_threads")]
        public int? MaxThreads { get; set; }

        [JsonPropertyName("panic_on_fault")]
        public bool? PanicOnFault { get; set; }

        [JsonPropertyName("trace_back")]
        public string? TraceBack { get; set; }

        [JsonPropertyName("memory_limit")]
        public ulong? MemoryLimit { get; set; }

        [JsonPropertyName("oom_killer")]
        public bool? OOMKiller { get; set; }
    }
}

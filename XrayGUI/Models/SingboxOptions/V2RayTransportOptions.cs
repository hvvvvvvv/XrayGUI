using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using XrayGUI.Modle.SingboxOptions.JsonConverter;

namespace XrayGUI.Modle.SingboxOptions
{
    [JsonConverter(typeof(V2RayTransportConverter))]
    public class V2RayTransportOptions
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonIgnore]
        public V2RayHTTPOptions? HTTPOptions { get; set; }

        [JsonIgnore]
        public V2RayWebsocketOptions? WebsocketOptions { get; set; }

        [JsonIgnore]
        public V2RayQUICOptions? QUICOptions { get; set; }

        [JsonIgnore]
        public V2RayGRPCOptions? GRPCOptions { get; set; }

        [JsonIgnore]
        public V2RayHTTPUpgradeOptions? HTTPUpgradeOptions { get; set; }
    }
    public class V2RayHTTPOptions
    {
        [JsonPropertyName("host")]
        public List<string>? Host { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("method")]
        public string? Method { get; set; }

        [JsonPropertyName("headers")]
        public Dictionary<string, List<string>>? Headers { get; set; }

        [JsonPropertyName("idle_timeout")]
        public string? IdleTimeout { get; set; }

        [JsonPropertyName("ping_timeout")]
        public string? PingTimeout { get; set; }
    }
    public class V2RayWebsocketOptions
    {
        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("headers")]
        public Dictionary<string, List<string>>? Headers { get; set; }

        [JsonPropertyName("max_early_data")]
        public uint? MaxEarlyData { get; set; }

        [JsonPropertyName("early_data_header_name")]
        public string? EarlyDataHeaderName { get; set; }
    }
    public class V2RayQUICOptions
    {

    }
    public class V2RayGRPCOptions
    {
        [JsonPropertyName("service_name")]
        public string? ServiceName { get; set; }

        [JsonPropertyName("idle_timeout")]
        public string? IdleTimeout { get; set; }

        [JsonPropertyName("ping_timeout")]
        public string? PingTimeout { get; set; }

        [JsonPropertyName("permit_without_stream")]
        public bool? PermitWithoutStream { get; set; }

        // The field "ForceLite" is ignored in JSON serialization.
        [JsonIgnore]
        public bool? ForceLite { get; set; }
    }
    public class V2RayHTTPUpgradeOptions
    {
        [JsonPropertyName("host")]
        public string? Host { get; set; }

        [JsonPropertyName("path")]
        public string? Path { get; set; }

        [JsonPropertyName("headers")]
        public Dictionary<string, List<string>>? Headers { get; set; }
    }
}

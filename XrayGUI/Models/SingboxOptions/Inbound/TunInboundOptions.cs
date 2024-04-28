using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class TunInboundOptions : InboundBasicOptions
    {
        [JsonPropertyName("interface_name")]
        public string? InterfaceName { get; set; }
        [JsonPropertyName("mtu")]
        public uint? MTU { get; set; }

        [JsonPropertyName("gso")]
        public bool? GSO { get; set; }

        [JsonPropertyName("inet4_address")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Inet4Address { get; set; }

        [JsonPropertyName("inet6_address")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Inet6Address { get; set; }

        [JsonPropertyName("auto_route")]
        public bool? AutoRoute { get; set; }

        [JsonPropertyName("strict_route")]
        public bool? StrictRoute { get; set; }

        [JsonPropertyName("inet4_route_address")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Inet4RouteAddress { get; set; }

        [JsonPropertyName("inet6_route_address")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Inet6RouteAddress { get; set; }

        [JsonPropertyName("inet4_route_exclude_address")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Inet4RouteExcludeAddress { get; set; }

        [JsonPropertyName("inet6_route_exclude_address")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Inet6RouteExcludeAddress { get; set; }

        [JsonPropertyName("include_interface")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? IncludeInterface { get; set; }

        [JsonPropertyName("exclude_interface")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? ExcludeInterface { get; set; }

        [JsonPropertyName("include_uid")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<uint>))]
        public Listable<uint>? IncludeUID { get; set; }

        [JsonPropertyName("include_uid_range")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? IncludeUIDRange { get; set; }

        [JsonPropertyName("exclude_uid")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<uint>))]
        public Listable<uint>? ExcludeUID { get; set; }

        [JsonPropertyName("exclude_uid_range")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? ExcludeUIDRange { get; set; }

        [JsonPropertyName("include_android_user")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<int>))]
        public Listable<int>? IncludeAndroidUser { get; set; }

        [JsonPropertyName("include_package")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? IncludePackage { get; set; }

        [JsonPropertyName("exclude_package")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? ExcludePackage { get; set; }

        [JsonPropertyName("endpoint_independent_nat")]
        public bool? EndpointIndependentNat { get; set; }

        [JsonPropertyName("udp_timeout")]
        public string? UDPTimeout { get; set; }

        [JsonPropertyName("stack")]
        public string? Stack { get; set; }

        [JsonPropertyName("platform")]
        public TunPlatformOptions? Platform { get; set; }
    }
    public class TunPlatformOptions
    {
        [JsonPropertyName("http_proxy")]
        public HTTPProxyOptions? HTTPProxy { get; set; }
    }
    public class HTTPProxyOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("server")]
        public ServerOptions? ServerOptions { get; set; }

        [JsonPropertyName("bypass_domain")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? BypassDomain { get; set; }
        [JsonPropertyName("match_domain")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? MatchDomain { get; set; }
    }
}

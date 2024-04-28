using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;

namespace XrayGUI.Modle.SingboxOptions.Dns
{
    [JsonConverter(typeof(JsonConverter.DNSRuleConverter))]
    public class DNSRuleOptions
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonIgnore]
        public DefaultDNSRule? DefaultOption { get; set; }
        [JsonIgnore]
        public LogicalDNSRule? LogicalOption { get; set; }
        
    }
    public class DefaultDNSRule
    {
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        [JsonPropertyName("inbound")]        
        public Listable<string>? Inbound { get; set; }

        [JsonPropertyName("ip_version")]
        public int? IpVersion { get; set; }

        [JsonPropertyName("query_type")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? QueryType { get; set; }

        [JsonPropertyName("network")]
        public string? Network { get; set; }

        [JsonPropertyName("auth_user")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? AuthUser { get; set; }

        [JsonPropertyName("protocol")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Protocol { get; set; }

        [JsonPropertyName("domain")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Domain { get; set; }

        [JsonPropertyName("domain_suffix")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? DomainSuffix { get; set; }

        [JsonPropertyName("domain_keyword")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? DomainKeyword { get; set; }

        [JsonPropertyName("domain_regex")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? DomainRegex { get; set; }

        [JsonPropertyName("geosite")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Geosite { get; set; }

        [JsonPropertyName("source_geoip")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? SourceGeoip { get; set; }

        [JsonPropertyName("geoip")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Geoip { get; set; }

        [JsonPropertyName("source_ip_cidr")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? SourceIpCidr { get; set; }

        [JsonPropertyName("source_ip_is_private")]
        public bool? SourceIpIsPrivate { get; set; }

        [JsonPropertyName("ip_cidr")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? IpCidr { get; set; }

        [JsonPropertyName("ip_is_private")]
        public bool? IpIsPrivate { get; set; }

        [JsonPropertyName("source_port")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<ushort>))]
        public Listable<ushort>? SourcePort { get; set; }

        [JsonPropertyName("source_port_range")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? SourcePortRange { get; set; }


        [JsonPropertyName("port")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<ushort>))]
        public Listable<ushort>? Port { get; set; }

        [JsonPropertyName("port_range")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? PortRange { get; set; }

        [JsonPropertyName("process_name")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? ProcessName { get; set; }

        [JsonPropertyName("process_path")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? ProcessPath { get; set; }

        [JsonPropertyName("package_name")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? PackageName { get; set; }

        [JsonPropertyName("user")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? User { get; set; }

        [JsonPropertyName("user_id")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<int>))]
        public Listable<int>? UserId { get; set; }

        [JsonPropertyName("clash_mode")]
        public string? ClashMode { get; set; }

        [JsonPropertyName("wifi_ssid")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? WifiSsid { get; set; }

        [JsonPropertyName("wifi_bssid")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? WifiBssid { get; set; }

        [JsonPropertyName("rule_set")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? RuleSet { get; set; }

        [JsonPropertyName("rule_set_ipcidr_match_source")]
        public bool? RuleSetIpcidrMatchSource { get; set; }

        [JsonPropertyName("invert")]
        public bool? Invert { get; set; }

        [JsonPropertyName("outbound")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Outbound { get; set; }

        [JsonPropertyName("server")]
        public string? Server { get; set; }

        [JsonPropertyName("disable_cache")]
        public bool? DisableCache { get; set; }

        [JsonPropertyName("client_subnet")]
        public string? ClientSubnet { get; set; }
    }

    public class LogicalDNSRule
    {
        [JsonPropertyName("mode")]
        public string? Mode { get; set; }
        [JsonPropertyName("rules")]
        public List<DNSRuleOptions>? Rules { get; set; }
        [JsonPropertyName("invert")]
        public bool? Invert { get; set; }
        [JsonPropertyName("server")]
        public string? Server { get; set; }
        [JsonPropertyName("disable_cache")]
        public bool? DisableCache { get; set; }
        [JsonPropertyName("rewrite_ttl")]
        public uint? RewriteTTL { get; set; }
        [JsonPropertyName("client_subnet")]
        public string? ClientSubnet { get; set; }
    }
}

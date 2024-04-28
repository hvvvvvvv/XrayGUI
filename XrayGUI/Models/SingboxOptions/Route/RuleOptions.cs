using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.JsonConverter;

namespace XrayGUI.Modle.SingboxOptions.Route
{
    [JsonConverter(typeof(RouteRuleConverter))]
    public class RuleOptions
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }
        [JsonIgnore]
        public DefaultRule? DefaultRuleOtions { get; set; }
        [JsonIgnore]
        public LogicalRule? LogicalRuleOptions { get; set; }
    }

    public class DefaultRule
    {
        [JsonPropertyName("inbound")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Inbound { get; set; }

        [JsonPropertyName("ip_version")]
        public int? IpVersion { get; set; }

        [JsonPropertyName("network")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Network { get; set; }

        [JsonPropertyName("auth_user")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? AuthUser { get; set; }

        [JsonPropertyName("protocol")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Protocol { get; set; }

        [JsonPropertyName("domain")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Domain { get; set; }

        [JsonPropertyName("domain_suffix")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? DomainSuffix { get; set; }

        [JsonPropertyName("domain_keyword")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? DomainKeyword { get; set; }

        [JsonPropertyName("domain_regex")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? DomainRegex { get; set; }

        [JsonPropertyName("geosite")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Geosite { get; set; }

        [JsonPropertyName("source_geoip")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? SourceGeoip { get; set; }

        [JsonPropertyName("geoip")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Geoip { get; set; }

        [JsonPropertyName("source_ip_cidr")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? SourceIpCidr { get; set; }

        [JsonPropertyName("source_ip_is_private")]
        public bool? SourceIpIsPrivate { get; set; }

        [JsonPropertyName("ip_cidr")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? IpCidr { get; set; }

        [JsonPropertyName("ip_is_private")]
        public bool? IpIsPrivate { get; set; }

        [JsonPropertyName("source_port")]
        [JsonConverter(typeof(ListaleConverter<int>))]
        public Listable<int>? SourcePort { get; set; }

        [JsonPropertyName("source_port_range")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? SourcePortRange { get; set; }

        [JsonPropertyName("port")]
        [JsonConverter(typeof(ListaleConverter<int>))]
        public Listable<int>? Port { get; set; }

        [JsonPropertyName("port_range")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? PortRange { get; set; }

        [JsonPropertyName("process_name")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? ProcessName { get; set; }

        [JsonPropertyName("process_path")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? ProcessPath { get; set; }

        [JsonPropertyName("package_name")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? PackageName { get; set; }

        [JsonPropertyName("user")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? User { get; set; }

        [JsonPropertyName("user_id")]
        [JsonConverter(typeof(ListaleConverter<int>))]
        public Listable<int>? UserId { get; set; }

        [JsonPropertyName("clash_mode")]
        public string? ClashMode { get; set; }

        [JsonPropertyName("wifi_ssid")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? WifiSsid { get; set; }

        [JsonPropertyName("wifi_bssid")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? WifiBssid { get; set; }

        [JsonPropertyName("rule_set")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? RuleSet { get; set; }

        [JsonPropertyName("rule_set_ipcidr_match_source")]
        public bool? RuleSetIpcidrMatchSource { get; set; }

        [JsonPropertyName("invert")]
        public bool? Invert { get; set; }

        [JsonPropertyName("outbound")]
        public string? Outbound { get; set; }
    }
    public class LogicalRule
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("mode")]
        public string? Mode { get; set; }

        [JsonPropertyName("rules")]
        public List<RuleOptions>? Rules { get; set; }

        [JsonPropertyName("invert")]
        public bool? Invert { get; set; }

        [JsonPropertyName("outbound")]
        public string? Outbound { get; set; }
    }
}

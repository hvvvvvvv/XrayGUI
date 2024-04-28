using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Route
{
    public class RouteOptions
    {
        [JsonPropertyName("geoip")]
        public GeoIPOptions? Geoip { get; set; }

        [JsonPropertyName("geosite")]
        public GeositeOptions? Geosite { get; set; }

        [JsonPropertyName("rules")]
        public List<RuleOptions>? Rules { get; set; }

        [JsonPropertyName("rule_set")]
        public List<RuleSetOptions>? RuleSet { get; set; }

        [JsonPropertyName("final")]
        public string? Final { get; set; }

        [JsonPropertyName("auto_detect_interface")]
        public bool AutoDetectInterface { get; set; }

        [JsonPropertyName("override_android_vpn")]
        public bool OverrideAndroidVpn { get; set; }

        [JsonPropertyName("default_interface")]
        public string? DefaultInterface { get; set; }

        [JsonPropertyName("default_mark")]
        public int DefaultMark { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.JsonConverter;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    [JsonConverter(typeof(InboundConverter))]
    public class InboundOptions
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("tag")]
        public string? Tag { get; set; }

        [JsonIgnore]
        public TunInboundOptions? TunOptions { get; set; }

        [JsonIgnore]
        public RedirectInboundOptions? RedirectOptions { get; set; }

        [JsonIgnore]
        public TProxyInboundOptions? TProxyOptions { get; set; }

        [JsonIgnore]
        public DirectInboundOptions? DirectOptions { get; set; }

        [JsonIgnore]
        public SocksInboundOptions? SocksOptions { get; set; }

        [JsonIgnore]
        public HTTPMixedInboundOptions? HTTPOptions { get; set; }

        [JsonIgnore]
        public HTTPMixedInboundOptions? MixedOptions { get; set; }

        [JsonIgnore]
        public ShadowsocksInboundOptions? ShadowsocksOptions { get; set; }

        [JsonIgnore]
        public VMessInboundOptions? VMessOptions { get; set; }

        [JsonIgnore]
        public TrojanInboundOptions? TrojanOptions { get; set; }

        [JsonIgnore]
        public NaiveInboundOptions? NaiveOptions { get; set; }

        [JsonIgnore]
        public HysteriaInboundOptions? HysteriaOptions { get; set; }

        [JsonIgnore]
        public ShadowTLSInboundOptions? ShadowTLSOptions { get; set; }

        [JsonIgnore]
        public VLESSInboundOptions? VLESSOptions { get; set; }

        [JsonIgnore]
        public TUICInboundOptions? TUICOptions { get; set; }

        [JsonIgnore]
        public Hysteria2InboundOptions? Hysteria2Options { get; set; }

    }
}

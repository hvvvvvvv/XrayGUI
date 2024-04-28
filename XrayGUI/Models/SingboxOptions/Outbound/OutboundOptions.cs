using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    [JsonConverter(typeof(JsonConverter.OutboundConverter))]
    public class OutboundOptions
    {
        [JsonPropertyName("type")]
        public string? Type { get; set; }

        [JsonPropertyName("tag")]
        public string? Tag { get; set; }

        [JsonIgnore]
        public DirectOutboundOptions? DirectOptions { get; set; }

        [JsonIgnore]
        public SocksOutboundOptions? SocksOptions { get; set; }

        [JsonIgnore]
        public HTTPOutboundOptions? HTTPOptions { get; set; }

        [JsonIgnore]
        public ShadowsocksOutboundOptions? ShadowsocksOptions { get; set; }

        [JsonIgnore]
        public VMessOutboundOptions? VMessOptions { get; set; }

        [JsonIgnore]
        public TrojanOutboundOptions? TrojanOptions { get; set; }

        [JsonIgnore]
        public WireGuardOutboundOptions? WireGuardOptions { get; set; }

        [JsonIgnore]
        public HysteriaOutboundOptions? HysteriaOptions { get; set; }

        [JsonIgnore]
        public TorOutboundOptions? TorOptions { get; set; }

        [JsonIgnore]
        public SSHOutboundOptions? SSHOptions { get; set; }

        [JsonIgnore]
        public ShadowTLSOutboundOptions? ShadowTLSOptions { get; set; }

        [JsonIgnore]
        public ShadowsocksROutboundOptions? ShadowsocksROptions { get; set; }

        [JsonIgnore]
        public VLESSOutboundOptions? VLESSOptions { get; set; }

        [JsonIgnore]
        public TUICOutboundOptions? TUICOptions { get; set; }

        [JsonIgnore]
        public Hysteria2OutboundOptions? Hysteria2Options { get; set; }

        [JsonIgnore]
        public SelectorOutboundOptions? SelectorOptions { get; set; }

        [JsonIgnore]
        public URLTestOutboundOptions? URLTestOptions { get; set; }
    }
}

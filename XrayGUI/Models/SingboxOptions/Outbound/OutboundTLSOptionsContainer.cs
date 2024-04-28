using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using XrayGUI.Modle.SingboxOptions.Inbound;

namespace XrayGUI.Modle.SingboxOptions.Outbound
{
    public class OutboundTLSOptionsContainer : DialerWithServerOptions
    {
        [JsonPropertyName("tls")]
        public InboundTLSOptions? TLS { get; set; }
    }
    public class OutboundTLSOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("disable_sni")]
        public bool? DisableSNI { get; set; }

        [JsonPropertyName("server_name")]
        public string? ServerName { get; set; }

        [JsonPropertyName("insecure")]
        public bool? Insecure { get; set; }

        [JsonPropertyName("alpn")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? ALPN { get; set; }

        [JsonPropertyName("min_version")]
        public string? MinVersion { get; set; }

        [JsonPropertyName("max_version")]
        public string? MaxVersion { get; set; }

        [JsonPropertyName("cipher_suites")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? CipherSuites { get; set; }

        [JsonPropertyName("certificate")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Certificate { get; set; }

        [JsonPropertyName("certificate_path")]
        public string? CertificatePath { get; set; }

        [JsonPropertyName("ech")]
        public OutboundECHOptions? ECH { get; set; }

        [JsonPropertyName("utls")]
        public OutboundUTLSOptions? UTLS { get; set; }

        [JsonPropertyName("reality")]
        public OutboundRealityOptions? Reality { get; set; }
    }
    public class OutboundECHOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("pq_signature_schemes_enabled")]
        public bool? PQSignatureSchemesEnabled { get; set; }

        [JsonPropertyName("dynamic_record_sizing_disabled")]
        public bool? DynamicRecordSizingDisabled { get; set; }

        [JsonPropertyName("config")]
        [JsonConverter(typeof(JsonConverter.ListaleConverter<string>))]
        public Listable<string>? Config { get; set; }

        [JsonPropertyName("config_path")]
        public string? ConfigPath { get; set; }
    }
    public class OutboundUTLSOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("fingerprint")]
        public string? Fingerprint { get; set; }
    }
    public class OutboundRealityOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("public_key")]
        public string? PublicKey { get; set; }

        [JsonPropertyName("short_id")]
        public string? ShortID { get; set; }
    }
}

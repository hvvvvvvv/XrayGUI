using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows;
using XrayGUI.Modle.SingboxOptions.JsonConverter;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class InboundTLSOptionsContainer : ListenOptions
    {
        [JsonPropertyName("tls")]
        public InboundTLSOptions? Tls { get; set; }
    }
    public class InboundTLSOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("server_name")]
        public string? ServerName { get; set; }

        [JsonPropertyName("insecure")]
        public bool? Insecure { get; set; }

        [JsonPropertyName("alpn")]
        public string? ALPN { get; set; }

        [JsonPropertyName("min_version")]
        public string? MinVersion { get; set; }

        [JsonPropertyName("max_version")]
        public string? MaxVersion { get; set; }

        [JsonPropertyName("cipher_suites")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? CipherSuites { get; set; }

        [JsonPropertyName("certificate")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Certificate { get; set; }

        [JsonPropertyName("certificate_path")]
        public string? CertificatePath { get; set; }

        [JsonPropertyName("key")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Key { get; set; }

        [JsonPropertyName("key_path")]
        public string? KeyPath { get; set; }

        [JsonPropertyName("acme")]
        public InboundACMEOptions? ACME { get; set; }

        [JsonPropertyName("ech")]
        public InboundECHOptions? ECH { get; set; }

        [JsonPropertyName("reality")]
        public InboundRealityOptions? Reality { get; set; }
    }
    #region ACME
    public class InboundACMEOptions
    {
        [JsonPropertyName("domain")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Domain { get; set; }

        [JsonPropertyName("data_directory")]
        public string? DataDirectory { get; set; }

        [JsonPropertyName("default_server_name")]
        public string? DefaultServerName { get; set; }

        [JsonPropertyName("email")]
        public string? Email { get; set; }

        [JsonPropertyName("provider")]
        public string? Provider { get; set; }

        [JsonPropertyName("disable_http_challenge")]
        public bool? DisableHTTPChallenge { get; set; }

        [JsonPropertyName("disable_tls_alpn_challenge")]
        public bool? DisableTLSALPNChallenge { get; set; }

        [JsonPropertyName("alternative_http_port")]
        public ushort? AlternativeHTTPPort { get; set; }

        [JsonPropertyName("alternative_tls_port")]
        public ushort? AlternativeTLSPort { get; set; }

        [JsonPropertyName("external_account")]
        public ACMEExternalAccountOptions? ExternalAccount { get; set; }

        [JsonPropertyName("dns01_challenge")]
        public ACMEDNS01ChallengeOptions? DNS01Challenge { get; set; }
    }
    public class ACMEExternalAccountOptions
    {
        [JsonPropertyName("key_id")]
        public string? KeyID { get; set; }

        [JsonPropertyName("mac_key")]
        public string? MACKey { get; set; }
    }
    [JsonConverter(typeof(ACMEDNS01ChallengeConverter))]
    public class ACMEDNS01ChallengeOptions
    {
        [JsonPropertyName("provider")]
        public string? Provider { get; set; }
        [JsonIgnore]
        public ACMEDNS01AliDNSOptions? AliDNSOptions { get; set; }
        [JsonIgnore]

        public ACMEDNS01CloudflareOptions? CloudflareOptions { get; set; }
    }
    public class ACMEDNS01AliDNSOptions
    {
        [JsonPropertyName("access_key_id")]
        public string? AccessKeyID { get; set; }

        [JsonPropertyName("access_key_secret")]
        public string? AccessKeySecret { get; set; }

        [JsonPropertyName("region_id")]
        public string? RegionID { get; set; }
    }
    public class ACMEDNS01CloudflareOptions
    {
        [JsonPropertyName("api_token")]
        public string? APIToken { get; set; }
    }
    #endregion

    #region Reality
    public class InboundRealityOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("handshake")]
        public InboundRealityHandshakeOptions? Handshake { get; set; }

        [JsonPropertyName("private_key")]
        public string? PrivateKey { get; set; }

        [JsonPropertyName("short_id")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? ShortID { get; set; }

        [JsonPropertyName("max_time_difference")]
        public string? MaxTimeDifference { get; set; }
    }
    public class InboundRealityHandshakeOptions: DialerOptions
    {
        [JsonPropertyName("server")]
        public string? Server { get; set; }

        [JsonPropertyName("server_port")]
        public ushort? ServerPort { get; set; }
    }
    #endregion

    #region ECH

    public class InboundECHOptions
    {
        [JsonPropertyName("enabled")]
        public bool? Enabled { get; set; }

        [JsonPropertyName("pq_signature_schemes_enabled")]
        public bool? PQSignatureSchemesEnabled { get; set; }

        [JsonPropertyName("dynamic_record_sizing_disabled")]
        public bool? DynamicRecordSizingDisabled { get; set; }

        [JsonPropertyName("key")]
        [JsonConverter(typeof(ListaleConverter<string>))]
        public Listable<string>? Key { get; set; }

        [JsonPropertyName("key_path")]
        public string? KeyPath { get; set; }
    }

    #endregion
}

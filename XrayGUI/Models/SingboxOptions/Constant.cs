using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Gaming.Input;

namespace XrayGUI.Modle.SingboxOptions
{
    public static class RuleTypes
    {
        public const string Logical = "logical";
        public const string Default = "default";
    }
    public static class RuleSetTypes
    {
        public const string Local = "local";
        public const string Remote = "remote";
    }
    public static class DNSProviders
    {
        public const string AliDNS = "alidns";
        public const string Cloudflare = "cloudflare";
    }
    public static class DomainStrategy
    {
        public const string AsIS = "as_is";
        public const string PreferIPv4 = "prefer_ipv4";
        public const string PreferIPv6 = "prefer_ipv6";
        public const string UseIPv4 = "ipv4_only";
        public const string UseIPv6 = "ipv6_only";
    }
    public static class V2RayTransportTypes
    {
        public const string HTTP = "http";
        public const string WebSocket = "ws";
        public const string QUIC = "quic";
        public const string GRPC = "grpc";
        public const string HTTPUpgrade = "httpupgrade";
    }
    public static class InboundTypes
    {
        public const string Tun = "tun";
        public const string Redirect = "redirect";
        public const string TProxy = "tproxy";
        public const string Direct = "direct";
        public const string Socks = "socks";
        public const string HTTP = "http";
        public const string Mixed = "mixed";
        public const string Shadowsocks = "shadowsocks";
        public const string VMess = "vmess";
        public const string Trojan = "trojan";
        public const string Naive = "naive";
        public const string WireGuard = "wireguard";
        public const string Hysteria = "hysteria";
        public const string ShadowTLS = "shadowtls";
        public const string VLESS = "vless";
        public const string TUIC = "tuic";
        public const string Hysteria2 = "hysteria2";
    }
    public static class OutboundTypes
    {
        public const string Direct = "direct";
        public const string Block = "block";
        public const string DNS = "dns";
        public const string Socks = "socks";
        public const string HTTP = "http";
        public const string Shadowsocks = "shadowsocks";
        public const string VMss = "vmess";
        public const string Trojan = "trojan";
        public const string WireGuard = "wireguard";
        public const string Hysteria = "hysteria";
        public const string Tor = "tor";
        public const string SSH = "ssh";
        public const string ShadowTLS = "shadowtls";
        public const string ShadowsocksR = "shadowsocksr";
        public const string VLESS = "vless";
        public const string TUIC = "tuic";
        public const string Hysteria2 = "hysteria2";
        public const string Selector = "selector";
        public const string URLTest = "urltest";
    }
    public static class VlessFlows
    {
        public const string None = "none";
        public const string XTLS_RPRX_VISION = "xtls-rprx-vision";
    }
}

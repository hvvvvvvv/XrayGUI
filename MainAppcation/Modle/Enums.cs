using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NetProxyController.Modle
{
    public enum SystemProtocol
    {
        Http = 0,
        Socks = 1
    }
    public enum ProxyModes
    {
        System = 0,
        Tun = 1
    }
    public enum OutboundProtocol
    {
        vless = 0,
        vmess = 1,
        trojan = 2,
        socks = 3,
        shadowsocks = 4
    }
    public enum XtlsFlow
    {
        None = 0,
        Xtls_rprx_vision = 1,
        Xtls_rprx_vision_udp443 = 2,
        Xtls_rprx_direct = 3,
        Xtls_rprx_direct_udp443= 4,
        Xtls_rprx_origin = 5,
        Xtls_rprx_origin_udp443 = 6
    }
    public enum VmessSecurity
    {
        None = 0,
        Zero = 1,
        Auto = 2,
        Aes_128_gcm = 3,
        Chacha20_poly1305 = 4
    }
    public enum SS_Ecrept
    {
        None = 0,
        Blake3_aes_128_gcm_2022 = 1,
        Blake3_aes_256_gcm_2022 = 2,
        Blake3_chacha20_poly1305_2022 = 3,
        Aes_128_gcm = 4,
        Aes_256_gcm = 5,
        Chacha20_poly1305 = 6
    }

    public enum TransportType
    {
        tcp = 0,
        kcp = 1,
        ws = 2,
        http = 3,
        domainsocket = 4,
        quic = 5,
        grpc = 6,
    }
    public enum TransportSecurity
    {
        none = 0,
        tls = 1,
        xtls = 2,
        reality = 3,
    }

    internal static class EnumExtensions
    {
        public static string GetStringValue(this XtlsFlow value)
        {
            return value switch
            {
                XtlsFlow.Xtls_rprx_vision => "xtls-rprx-vision",
                XtlsFlow.Xtls_rprx_vision_udp443 => "xtls-rprx-vision-udp443",
                XtlsFlow.Xtls_rprx_direct => "xtls-rprx-direct",
                XtlsFlow.Xtls_rprx_direct_udp443 => "xtls-rprx-direct-udp443",
                XtlsFlow.Xtls_rprx_origin => "Xtls-rprx-origin",
                XtlsFlow.Xtls_rprx_origin_udp443 => "Xtls-rprx-origin-udp443",
                _ => "none"
            };
        }
        public static string GetStringValue(this VmessSecurity value)
        {
            return value switch
            {
                VmessSecurity.None => "none",
                VmessSecurity.Zero => "zero",
                VmessSecurity.Aes_128_gcm => "aes-128-gcm",
                VmessSecurity.Chacha20_poly1305 => "chacha20-poly1305",
                _ => "auto"
            };
        }
        public static string GetStringValue(this SS_Ecrept value)
        {
            return value switch
            {
                SS_Ecrept.Blake3_aes_128_gcm_2022 => "2022-blake3-aes-128-gcm",
                SS_Ecrept.Blake3_aes_256_gcm_2022 => "2022-blake3-aes-256-gcm",
                SS_Ecrept.Blake3_chacha20_poly1305_2022 => "2022-blake3-chacha20-poly1305",
                SS_Ecrept.Aes_128_gcm => "aes-128-gcm",
                SS_Ecrept.Aes_256_gcm => "aes-256-gcm",
                SS_Ecrept.Chacha20_poly1305 => "chacha20-poly1305",
                _ => "none"
            };
        }
    }
}

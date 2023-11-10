using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XrayGUI.Modle
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
        shadowsocks = 4,
        freedom = 5,
    }
    public enum XtlsFlow
    {
        Xtls_rprx_vision = 0,
        Xtls_rprx_vision_udp443 = 1,
        Xtls_rprx_direct = 2,
        Xtls_rprx_direct_udp443= 3,
        Xtls_rprx_origin = 4,
        Xtls_rprx_origin_udp443 = 5
    }
    public enum SecurityMode
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
        Chacha20_poly1305 = 6,
        Xchacha20_poly1305 = 7,
        Chacha20_ietf_poly1305 = 8,
        Xchacha20_ietf_poly1305 = 9
    }

    public enum TransportType
    {
        tcp = 0,
        kcp = 1,
        ws = 2,
        http = 3,
        quic = 4,
        grpc = 5,
        none = 6
    }
    public enum TransportSecurity
    {
        none = 0,
        tls = 1,
        xtls = 2,
        reality = 3,
    }
    public enum FeignType
    {
        none = 0,
        http = 1,
        srtp = 2,
        utp = 3,
        wechat_video = 4,
        dtls = 5,
        wireguard = 6,
    }
    public enum HttpRequestType
    {
        GET = 0,
        HEAD  = 1,
        POST = 2,
        PUT = 3,
        DELETE = 4,
        CONNECT = 5,
        OPTIONS = 6,
        TRACE = 7,
        PATCH = 8,
    }
    public enum TlsFingerPrint
    {
        none = 0,
        chrome = 1,
        firefox = 2,
        safari = 3,
        ios = 4,
        android = 5,
        edge = 6,
        qq = 7,
        random = 8,
        randomized = 9
    }

    [Flags]
    public enum KeyModifier
    {
        None = 0x0000,
        Alt = 0x0001,
        Ctrl = 0x0002,
        Shift = 0x0004,
        Win = 0x0008,
        NoRepeat = 0x4000
    }

    public static class EnumExtensions
    {
        public static string GetStringValue(this FeignType value)
        {
            return value switch
            {
                FeignType.wechat_video => "wechat-video",
                _ => value.ToString()
            };
        }
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
        public static string GetStringValue(this SecurityMode value)
        {
            return value switch
            {
                SecurityMode.None => "none",
                SecurityMode.Zero => "zero",
                SecurityMode.Aes_128_gcm => "aes-128-gcm",
                SecurityMode.Chacha20_poly1305 => "chacha20-poly1305",
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
                SS_Ecrept.Xchacha20_poly1305 => "xchacha20-poly1305",
                SS_Ecrept.Chacha20_ietf_poly1305 => "chacha20-ietf-poly1305",
                SS_Ecrept.Xchacha20_ietf_poly1305 => "xchacha20-ietf-poly1305",
                _ => "none"
            };
        }
        public static T ParseEunmEx<T>(string? text) where T : struct
        {
            if (text is null) return default;
            foreach(var item in Enum.GetValues(typeof(T)).Cast<T>())
            {               
                string? compareText = item switch
                {
                    SS_Ecrept i => i.GetStringValue(),
                    SecurityMode i => i.GetStringValue(),
                    XtlsFlow i => i.GetStringValue(),
                    FeignType i => i.GetStringValue(),
                    _ => item.ToString()
                };
                if (string.Equals(compareText, text, StringComparison.OrdinalIgnoreCase))
                {
                    return item;
                }
            }
            throw new Exception();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Vless = 0,
        Vmess = 1,
        Trojan = 2,
        Scoks = 3,
        ShadowSocks = 4
    }
    public enum XtlsFlow
    {
        Xtls_rprx_vision = 0,
        Xtls_rprx_vision_udp443 = 1,
        Xtls_rprx_direct = 2,
        Xtls_rprx_direct_udp443= 3,
        Xtls_rprx_direct_origin = 4,
        Xtls_rprx_direct_origin_udp443 = 5
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
        Blake3_aes_128_256_2022 = 2,
        Blake3_chacha20_poly1305_2022 = 3,
        Aes_128_gcm = 4,
        Aes_256_gcm = 5,
        Chacha20_poly1305 = 6
    }
}

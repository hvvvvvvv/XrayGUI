﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Modle
{
    public enum SystemProtocol
    {
        Http = 1,
        Socks = 2
    }
    public enum ProxyModes
    {
        System = 1,
        Tun = 2
    }
    public enum OutboundProtocol
    {
        Vless = 1,
        Vmess = 2,
        Trojan = 3,
        Scoks = 4,
        ShadowSocks = 5,
    }
}
namespace XrayCoreConfigModle
{
    public enum InboundServerSettingType
    {
        Unknown = 0,
        Http = 1,
        Socks = 2,
        Trojan = 3,
        Shadowsocks = 4,
        Vless = 5,
        Vmess = 6,
        DokodemoDoor = 7
    }
    public enum OutboundServerSettingType
    {
        Unknown = 0,
        Http = 1,
        Socks = 2,
        Trojan = 3,
        Shadowsocks = 4,
        Vless = 5,
        Vmess = 6,
        Dns = 7,
        Freedom = 8,
        Blackhole = 9,
        WireGuard = 10
    }
    public enum DnsServerInputMode
    {
        Object = 0,
        String = 1
    }
}
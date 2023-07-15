using NetProxyController.Modle.Server;
using NetProxyController.Modle.Subscription;
using NetProxyController.Modle;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using XrayCoreConfigModle;
using System.Net.NetworkInformation;
using static Vanara.PInvoke.User32;

namespace NetProxyController.Handler
{
    public class SubscribeHandle
    {
        private const string SSPrefixMatch = "ss://";
        private const string TrojanPrefixMatch = "trojan://";
        private const string VmessPrefixMatch = "vmess://";
        private static readonly Regex SSUrlRegex = new(SSPrefixMatch + @"(?<base64>[A-Za-z0-9+-/=_]+)(?:#(?<tag>\S+))?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex SSDetailsRegex = new(@"^((?<method>.+?):(?<password>.*)@(?<hostname>.+?):(?<port>\d+?))$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex StdVmessUserInfo = new(
            @"^(?<network>[a-z]+)(\+(?<streamSecurity>[a-z]+))?:(?<id>[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12})$", RegexOptions.Compiled);
        private static ServerItem? ResolveSSUrlType01(string subContent)
        {
            var urlMatch = SSUrlRegex.Match(subContent);
            if (!urlMatch.Success) return null;
            var detailMatch = SSDetailsRegex.Match(urlMatch.Groups["base64"].Value);
            if (!detailMatch.Success) return null;
            try
            {
                var tag = urlMatch.Groups["tag"].Value;
                var port = Convert.ToInt32(detailMatch.Groups["port"].Value);
                var addr = detailMatch.Groups["hostname"].Value;
                var ret = new ServerItem()
                {

                    Address = !string.IsNullOrEmpty(addr) ? addr : throw new Exception(),
                    Port = port,
                    Remarks = !string.IsNullOrEmpty(tag) ? tag : $"UnknownName_{addr}"
                };
                ShadowSocksInfo shadowSocksInfo = new()
                {
                    Password = detailMatch.Groups["password"].Value,
                    Method = EnumExtensions.ParseEunmEx<SS_Ecrept>(detailMatch.Groups["method"].Value)
                };
                ret.SetProtocolInfoObj(shadowSocksInfo);
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static ServerItem? ResolveSSUrlType02(string subContent)
        {
            if (!subContent.StartsWith(SSPrefixMatch)) return null;
            try
            {
                var u = new Uri(subContent);
                var name = u.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
                var rawUserInfo = u.GetComponents(UriComponents.UserInfo, UriFormat.UriEscaped);
                var addr = u.IdnHost;
                var ret = new ServerItem()
                {
                    Address = u.IdnHost,
                    Port = u.Port == -1 ? throw new Exception() : u.Port,
                    Remarks = string.IsNullOrEmpty(name) ? $"UnknownName_{addr}" : name
                };
                if (Tools.EncodeHelper.TryConvertFromBase64(rawUserInfo, out string decodedText))
                {
                    rawUserInfo = decodedText;
                }
                string[] userInfoParts = rawUserInfo.Split(new[] { ':' }, 2);
                if (userInfoParts.Length != 2) throw new Exception();
                ShadowSocksInfo shadowSocksInfo = new();
                shadowSocksInfo.Method = EnumExtensions.ParseEunmEx<SS_Ecrept>(userInfoParts[0]);
                string pwd = Tools.EncodeHelper.TryDecodeFromUrlCode(userInfoParts[1], out string decoded) ? decoded : userInfoParts[1];
                shadowSocksInfo.Password = !string.IsNullOrEmpty(pwd) ? pwd : throw new Exception();
                ret.SetProtocolInfoObj(shadowSocksInfo);
                NameValueCollection queryParameters = HttpUtility.ParseQueryString(u.Query);
                if (queryParameters["plugin"] != null)
                {
                    var obfsHost = queryParameters["plugin"]!.Split(';').FirstOrDefault(t => t.Contains("obfs-host="));
                    if (queryParameters["plugin"]!.Contains("obfs=http") && !string.IsNullOrEmpty(obfsHost))
                    {
                        obfsHost = obfsHost.Replace("obfs-host=", string.Empty);
                        StreamInfo streamInfo = new();
                        streamInfo.Transport = TransportType.tcp;
                        streamInfo.TcpTransport.Feign = FeignType.http;
                        streamInfo.TcpTransport.Headers = new Dictionary<string, List<string>>()
                        {
                            {"host",obfsHost.Split(",").ToList() }
                        };
                        ret.SetStreamInfo(streamInfo);
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
                return ret;
            }
            catch (Exception) { return null; }
        }
        private static List<ServerItem>? ResolveSSJson(string subcontent)
        {
            List<ShadowSocksItem>? SSserverItems;
            if (JsonHandler.TryJsonDeserializeFromText<List<ShadowSocksItem>>(subcontent, out var outPut))
            {
                SSserverItems = outPut;
            }
            else if (JsonHandler.TryJsonDeserializeFromText<ShadowSokcsJson>(subcontent, out var json))
            {
                SSserverItems = json.servers;
            }
            else
            {
                return null;
            }
            List<ServerItem> ret = new();
            foreach (var item in SSserverItems!)
            {
                try
                {
                    ServerItem server = new()
                    {
                        Address = !string.IsNullOrEmpty(item.server) ? item.server : throw new Exception(),
                        Remarks = !string.IsNullOrEmpty(item.remarks) ? item.remarks : throw new Exception(),
                        Port = Convert.ToInt32(item.server_port)
                    };
                    ShadowSocksInfo shadowSocksInfo = new()
                    {
                        Method = EnumExtensions.ParseEunmEx<SS_Ecrept>(item.method),
                        Password = !string.IsNullOrEmpty(item.password) ? item.password : throw new Exception(),
                    };
                    server.SetProtocolInfoObj(shadowSocksInfo);
                    ret.Add(server);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return ret;
        }
        private static ServerItem? ResolveTrojanStdUrl(string subContent)
        {
            if (!subContent.StartsWith(TrojanPrefixMatch)) return null;
            try
            {
                var u = new Uri(subContent);
                var addr = u.IdnHost;
                var port = u.Port;
                var remarks = u.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
                var id = u.UserInfo;
                var q = HttpUtility.ParseQueryString(u.Query);
                ServerItem ret = new()
                {
                    Protocol = OutboundProtocol.trojan,
                    Remarks = !string.IsNullOrEmpty(remarks) ? remarks : $"UnknownName_{addr}",
                    Address = !string.IsNullOrEmpty(addr) ? addr : throw new Exception(),
                    Port = port != -1 ? port : throw new Exception()
                };
                TrojanInfo trojanInfo = new()
                {
                    Password = !string.IsNullOrEmpty(id) ? id : throw new Exception()
                };
                ret.SetProtocolInfoObj(trojanInfo);
                ret.SetStreamInfo(ResolveStreamInfo(q) ?? throw new Exception());
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static ServerItem? ResolveVlessStdUrl(string subContent)
        {
            try
            {
                var u = new Uri(subContent);
                var q = HttpUtility.ParseQueryString(u.Query);
                var addr = !string.IsNullOrEmpty(u.IdnHost) ? u.IdnHost : throw new Exception();
                var port = u.Port != -1 ? u.Port : throw new Exception();
                var remarks = u.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
                var id = !string.IsNullOrEmpty(u.UserInfo) ? u.UserInfo : throw new Exception();
                var flow = q["flow"] is not null ? EnumExtensions.ParseEunmEx<XtlsFlow>(q["flow"]) : throw new Exception();
                ServerItem ret = new()
                {
                    Protocol = OutboundProtocol.vless,
                    Address = addr,
                    Port = port,
                    Remarks = !string.IsNullOrEmpty(remarks) ? remarks : $"UnknownName_{addr}"
                };
                VlessInfo vlessInfo = new()
                {
                    Id = id,
                    Flow = flow,
                };
                ret.SetProtocolInfoObj(vlessInfo);
                ret.SetStreamInfo(ResolveStreamInfo(q) ?? throw new Exception());
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static StreamInfo? ResolveStreamInfo(NameValueCollection query)
        {
            StreamInfo streamInfo = new()
            {
                Security = EnumExtensions.ParseEunmEx<TransportSecurity>(query["security"]),
            };
            try
            {
                switch (streamInfo.Security)
                {
                    case TransportSecurity.reality:
                        {
                            streamInfo.RealityPolicy.SpiderX = HttpUtility.UrlDecode(query["spx"]) ?? string.Empty;
                            streamInfo.RealityPolicy.FingerPrint = EnumExtensions.ParseEunmEx<TlsFingerPrint>(HttpUtility.UrlDecode(query["fp"]));
                            streamInfo.RealityPolicy.ShortId = HttpUtility.UrlDecode(query["sid"]) ?? string.Empty;
                            streamInfo.RealityPolicy.ServerName = query["sni"] ?? throw new Exception();
                            streamInfo.RealityPolicy.PublicKey = HttpUtility.UrlDecode(query["pbk"]) ?? throw new Exception();
                        }
                        break;
                    case TransportSecurity.tls:
                        {
                            streamInfo.TlsPolicy.ServerName = query["sni"] ?? throw new Exception();
                            streamInfo.TlsPolicy.FingerPrint = EnumExtensions.ParseEunmEx<TlsFingerPrint>(HttpUtility.UrlDecode(query["fp"]));
                            streamInfo.TlsPolicy.Alpn = HttpUtility.UrlDecode(query["alpn"])?.Split(",").ToList();
                        }
                        break;
                    case TransportSecurity.xtls:
                        {
                            streamInfo.XTlsPolicy.ServerName = query["sni"] ?? throw new Exception();
                            streamInfo.XTlsPolicy.Alpn = HttpUtility.UrlDecode(query["alpn"])?.Split(",").ToList();
                        }
                        break;
                }
                switch (query["type"])
                {
                    case "tcp":
                        {
                            streamInfo.Transport = TransportType.tcp;
                            if (query["headerType"] == "http")
                            {
                                streamInfo.TcpTransport.Feign = FeignType.http;
                                if (query["host"] is not null)
                                {
                                    streamInfo.TcpTransport.Headers = new()
                                    {
                                        {"Host", HttpUtility.UrlDecode(query["host"]!).Split(",").ToList()}
                                    };
                                }
                            }
                        }
                        break;
                    case "kcp":
                        {
                            streamInfo.Transport = TransportType.kcp;
                            streamInfo.KcpTransport.Feign = EnumExtensions.ParseEunmEx<FeignType>(query["headerType"]);
                            streamInfo.KcpTransport.Seed = HttpUtility.UrlDecode(query["seed"] ?? string.Empty);
                            if (!Global.KcpOrQuicFeignItems.Contains(streamInfo.KcpTransport.Feign)) throw new Exception();
                        }
                        break;
                    case "ws":
                        {
                            streamInfo.Transport = TransportType.ws;
                            if (query["host"] is not null)
                            {
                                streamInfo.WsTransport.Headers = new()
                                {
                                    {
                                        "Host", HttpUtility.UrlDecode(query["host"]!)
                                    }
                                };
                            }
                            streamInfo.WsTransport.Path = HttpUtility.UrlDecode(query["path"] ?? streamInfo.WsTransport.Path);
                        }
                        break;
                    case "http":
                    case "h2":
                        {
                            streamInfo.Transport = TransportType.http;
                            streamInfo.H2Transport.Hosts = HttpUtility.UrlDecode(query["host"] ?? streamInfo.H2Transport.Hosts);
                            streamInfo.H2Transport.Path = HttpUtility.UrlDecode(query["path"] ?? streamInfo.H2Transport.Path);
                        }
                        break;
                    case "quic":
                        {
                            streamInfo.Transport = TransportType.quic;
                            streamInfo.QuicTransport.Feign = EnumExtensions.ParseEunmEx<FeignType>(query["headerType"]);
                            streamInfo.QuicTransport.Security = EnumExtensions.ParseEunmEx<SecurityMode>(query["quicSecurity"]);
                            streamInfo.QuicTransport.Key = HttpUtility.UrlDecode(query["key"] ?? string.Empty);
                            if (!Global.KcpOrQuicFeignItems.Contains(streamInfo.QuicTransport.Feign) || !Global.QuicSecurityModeItems.Contains(streamInfo.QuicTransport.Security))
                            {
                                throw new Exception();
                            }

                        }
                        break;
                    case "grpc":
                        {
                            streamInfo.Transport = TransportType.grpc;
                            streamInfo.GrpcTranport.ServiceName = HttpUtility.UrlDecode(query["serviceName"] ?? string.Empty);
                        }
                        break;
                }
                return streamInfo;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static ServerItem? ResolveVmessJson(string subContent)
        {
            if (!subContent.StartsWith(VmessPrefixMatch))
            {
                return null;
            }
            subContent = subContent[VmessPrefixMatch.Length..];
            if (Tools.EncodeHelper.TryConvertFromBase64(subContent, out string outPut))
            {
                subContent = outPut;
            }
            if (!JsonHandler.TryJsonDeserializeFromText(subContent, out Modle.Subscription.VmessbyJson jsonObj))
            {
                return null;
            }
            try
            {
                var ret = new ServerItem()
                {
                    Protocol = OutboundProtocol.vmess,
                    Address = jsonObj.add,
                    Port = jsonObj.port != 0 ? jsonObj.port : throw new Exception(),
                    Remarks = jsonObj.ps
                };
                var userVerific = new VmessInfo()
                {
                    Security = string.IsNullOrEmpty(jsonObj.scy) ? SecurityMode.Auto : EnumExtensions.ParseEunmEx<SecurityMode>(jsonObj.scy),
                    Id = jsonObj.id,
                    AlterId = jsonObj.aid
                };
                var StreamInfo = new StreamInfo()
                {
                    Transport = string.IsNullOrEmpty(jsonObj.net) ? TransportType.tcp : EnumExtensions.ParseEunmEx<TransportType>(jsonObj.net),
                    Security = string.IsNullOrEmpty(jsonObj.tls) ? TransportSecurity.tls : EnumExtensions.ParseEunmEx<TransportSecurity>(jsonObj.tls)
                };
                var feign = string.IsNullOrEmpty(jsonObj.type) ? FeignType.none : EnumExtensions.ParseEunmEx<FeignType>(jsonObj.type);
                switch (StreamInfo.Transport)
                {
                    case TransportType.tcp:
                        {
                            if (!Global.TcpFeignItems.Contains(feign))
                            {
                                throw new Exception();
                            }
                            StreamInfo.TcpTransport.Feign = feign;
                            if (!string.IsNullOrEmpty(jsonObj.host))
                            {
                                StreamInfo.TcpTransport.Headers = new Dictionary<string, List<string>>
                                {
                                    { "Host",jsonObj.host.Split(",").ToList() }
                                };
                            }
                        }
                        break;
                    case TransportType.quic:
                        {
                            if (!Global.KcpOrQuicFeignItems.Contains(feign))
                            {
                                throw new Exception();
                            }
                            var security = string.IsNullOrEmpty(jsonObj.host) ? SecurityMode.None : EnumExtensions.ParseEunmEx<SecurityMode>(jsonObj.host);
                            if (!Global.QuicSecurityModeItems.Contains(security))
                            {
                                throw new Exception();
                            }
                            StreamInfo.QuicTransport.Feign = feign;
                            StreamInfo.QuicTransport.Security = security;
                            StreamInfo.QuicTransport.Key = jsonObj.path;
                        }
                        break;
                    case TransportType.ws:
                        {
                            if (!string.IsNullOrEmpty(jsonObj.host))
                            {
                                StreamInfo.WsTransport.Headers = new Dictionary<string, string>
                                {
                                    {"Host",jsonObj.host }
                                };
                            }
                            StreamInfo.WsTransport.Path = jsonObj.path;
                        }
                        break;
                    case TransportType.kcp:
                        {
                            if (!Global.KcpOrQuicFeignItems.Contains(feign))
                            {
                                throw new Exception();
                            }
                            StreamInfo.KcpTransport.Feign = feign;
                            StreamInfo.KcpTransport.Seed = jsonObj.path;
                        }
                        break;
                    case TransportType.http:
                        {
                            StreamInfo.H2Transport.Hosts = jsonObj.host;
                            StreamInfo.H2Transport.Path = jsonObj.path;
                        }
                        break;
                    case TransportType.grpc:
                        {
                            StreamInfo.GrpcTranport.ServiceName = jsonObj.path;
                        }
                        break;
                }
                switch (StreamInfo.Security)
                {
                    case TransportSecurity.tls:
                        {
                            StreamInfo.TlsPolicy.FingerPrint = string.IsNullOrEmpty(jsonObj.fp) ? TlsFingerPrint.none : EnumExtensions.ParseEunmEx<TlsFingerPrint>(jsonObj.fp);
                            StreamInfo.TlsPolicy.ServerName = jsonObj.sni;
                            if (!string.IsNullOrEmpty(jsonObj.alpn))
                            {
                                StreamInfo.TlsPolicy.Alpn = jsonObj.alpn.Split(',').ToList();
                            }
                        }
                        break;
                    case TransportSecurity.xtls:
                        {
                            StreamInfo.XTlsPolicy.FingerPrint = string.IsNullOrEmpty(jsonObj.fp) ? TlsFingerPrint.none : EnumExtensions.ParseEunmEx<TlsFingerPrint>(jsonObj.fp);
                            if (!string.IsNullOrEmpty(jsonObj.alpn))
                            {
                                StreamInfo.XTlsPolicy.Alpn = jsonObj.alpn.Split(',').ToList();
                            }
                        }
                        break;
                }
                ret.SetProtocolInfoObj(userVerific);
                ret.SetStreamInfo(StreamInfo);
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static ServerItem? ResolveVmessStdUrl(string SubContent)
        {
            if (!SubContent.StartsWith(VmessPrefixMatch)) return null;
            try
            {
                Uri u = new(SubContent);
                var q = HttpUtility.ParseQueryString(u.Query);
                var m = StdVmessUserInfo.Match(u.UserInfo);
                if (!m.Success) throw new Exception();
                var ret = new ServerItem()
                {
                    Protocol = OutboundProtocol.vmess,
                    Address = u.IdnHost,
                    Port = u.Port,
                    Remarks = u.GetComponents(UriComponents.Fragment, UriFormat.Unescaped)
                };
                VmessInfo vmessInfo = new()
                {
                    Security = SecurityMode.Auto,
                    Id = m.Groups["id"].Value
                };
                StreamInfo streamInfo = new()
                {
                    Security = m.Groups["streamSecurity"].Success ? EnumExtensions.ParseEunmEx<TransportSecurity>(m.Groups["streamSecurity"].Value) : TransportSecurity.tls
                };
                switch (m.Groups["network"].Value)
                {
                    case "tcp":
                        {
                            streamInfo.Transport = TransportType.tcp;
                            streamInfo.TcpTransport.Feign = EnumExtensions.ParseEunmEx<FeignType>(q["type"] ?? "none");
                            if (!Global.TcpFeignItems.Contains(streamInfo.TcpTransport.Feign))
                            {
                                throw new Exception();
                            }
                        }
                        break;
                    case "kcp":
                        {
                            streamInfo.Transport = TransportType.kcp;
                            streamInfo.KcpTransport.Feign = EnumExtensions.ParseEunmEx<FeignType>(q["type"] ?? "none");
                            if (!Global.KcpOrQuicFeignItems.Contains(streamInfo.TcpTransport.Feign))
                            {
                                throw new Exception();
                            }
                        }
                        break;
                    case "quic":
                        {
                            streamInfo.Transport = TransportType.quic;
                            streamInfo.QuicTransport.Feign = EnumExtensions.ParseEunmEx<FeignType>(q["type"] ?? "none");
                            streamInfo.QuicTransport.Security = EnumExtensions.ParseEunmEx<SecurityMode>(HttpUtility.UrlDecode(q["security"] ?? "none"));
                            streamInfo.QuicTransport.Key = q["key"] ?? string.Empty;
                            if (!Global.QuicSecurityModeItems.Contains(streamInfo.QuicTransport.Security) || !Global.KcpOrQuicFeignItems.Contains(streamInfo.QuicTransport.Feign))
                            {
                                throw new Exception();
                            }
                        }
                        break;
                    case "h2":
                    case "http":
                        {
                            streamInfo.Transport = TransportType.http;
                            streamInfo.H2Transport.Path = q["path"] ?? streamInfo.H2Transport.Path;
                            streamInfo.H2Transport.Hosts = q["host"] ?? string.Empty;
                        }
                        break;
                    case "ws":
                        {
                            streamInfo.WsTransport.Path = q["path"] ?? streamInfo.WsTransport.Path;
                            if (q["host"] is not null)
                            {
                                streamInfo.WsTransport.Headers = new Dictionary<string, string>()
                                {
                                    {"host",q["host"]! }
                                };
                            }
                        }
                        break;
                    default: throw new Exception();
                }
                ret.SetProtocolInfoObj(vmessInfo);
                ret.SetStreamInfo(streamInfo);
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private static List<ServerItem>? ResolveSubFromSubctent(string subContent)
        {
            List<ServerItem> ret = new();
            if(Tools.EncodeHelper.TryConvertFromBase64(subContent,out string base64DecodeText))
            {
                subContent = base64DecodeText;
            }
            foreach(var item in subContent.Split(Environment.NewLine))
            {
                ServerItem? serverItem = GetPrefixText(item) switch
                {
                    SSPrefixMatch => ResolveSSUrlType01(item) ?? ResolveSSUrlType02(item),
                    VmessPrefixMatch => ResolveVlessStdUrl(item),
                    _ => null
                };
            }
            return null;
        }
        private static string GetPrefixText(string subItemText)
        {
            var ret = string.Empty;
            var delimiter = "://";
            var delimiterIndex = subItemText.IndexOf(delimiter);
            if(delimiterIndex > 0)
            {
                ret = subItemText[..delimiterIndex];
                ret += delimiter;
            }
            return ret;
        }
    }
}

using ABI.Windows.ApplicationModel.Activation;
using CommunityToolkit.Mvvm.Messaging;
using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Documents.DocumentStructures;
using Windows.Storage.Search;

namespace NetProxyController.Handler.SubResolve
{
    internal static class TrojanResolve
    {
        public const string PrefixMatch = "trojan://";
        public static ServerItem? ResolveVmessStdUrl(string subContent)
        {
            if (!subContent.StartsWith(PrefixMatch)) return null;
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
        public static ServerItem? ResolveVlessStdUrl(string subContent)
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
        public static StreamInfo? ResolveStreamInfo(NameValueCollection query)
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
            catch(Exception)
            {
                return null;
            }
        }
    }
}

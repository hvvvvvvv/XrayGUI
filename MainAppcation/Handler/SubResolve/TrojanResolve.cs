using ABI.Windows.ApplicationModel.Activation;
using CommunityToolkit.Mvvm.Messaging;
using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
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
        public static ServerItem? ResolveByStdUrl(string subContent)
        {
            if (!subContent.StartsWith(PrefixMatch)) return null;
            try
            {
                var u = new Uri(subContent);
                var addr = u.IdnHost;
                var port = u.Port;
                var remarks = u.GetComponents(UriComponents.Fragment, UriFormat.Unescaped);
                var id = u.UserInfo;
                var query = HttpUtility.ParseQueryString(u.Query);
                ServerItem ret = new()
                {
                    Remarks = !string.IsNullOrEmpty(remarks) ? remarks : $"UnknownName_{addr}",
                    Address = !string.IsNullOrEmpty(addr) ? addr : throw new Exception(),
                    Port = port != -1 ? port : throw new Exception()
                };
                TrojanInfo trojanInfo = new()
                {
                    Password = !string.IsNullOrEmpty(id) ? id : throw new Exception()
                };
                StreamInfo streamInfo = new()
                {
                    Security = EnumExtensions.ParseEunmEx<TransportSecurity>(query["security"] ?? TransportSecurity.none.ToString()),
                };
                switch(streamInfo.Security)
                {
                    case TransportSecurity.reality:
                        {
                            streamInfo.RealityPolicy.SpiderX = query["spx"] ?? string.Empty;
                            streamInfo.RealityPolicy.FingerPrint = EnumExtensions.ParseEunmEx<TlsFingerPrint>(query["fp"] ?? TlsFingerPrint.none.ToString());
                            streamInfo.RealityPolicy.ShortId = query["sid"] ?? string.Empty;
                            streamInfo.RealityPolicy.ServerName = query["sni"] ?? throw new Exception();
                            streamInfo.RealityPolicy.PublicKey = query["pbk"] ?? throw new Exception();
                        }break;
                    case TransportSecurity.tls:
                        {
                            streamInfo.TlsPolicy.ServerName = query["sni"] ?? throw new Exception();
                            streamInfo.TlsPolicy.FingerPrint = EnumExtensions.ParseEunmEx<TlsFingerPrint>(query["fp"] ?? TlsFingerPrint.none.ToString());
                            streamInfo.TlsPolicy.Alpn = query["alpn"]?.Split(",").ToList();
                        }
                        break;
                    case TransportSecurity.xtls:
                        {
                            streamInfo.XTlsPolicy.ServerName = query["sni"] ?? throw new Exception();
                            streamInfo.XTlsPolicy.Alpn = query["alpn"]?.Split(",").ToList();
                        }
                        break;
                }
                switch(query["type"])
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
                        }break;
                    case "kcp":
                        {
                            streamInfo.Transport = TransportType.kcp;
                            streamInfo.KcpTransport.Feign = EnumExtensions.ParseEunmEx<FeignType>(query["headerType"] ?? "none");
                            streamInfo.KcpTransport.Seed = HttpUtility.UrlDecode(query["seed"] ?? string.Empty);
                            if (!Global.KcpOrQuicFeignItems.Contains(streamInfo.KcpTransport.Feign)) throw new Exception();
                        }break;
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
                            streamInfo.QuicTransport.Feign = EnumExtensions.ParseEunmEx<FeignType>(query["headerType"] ?? "none");
                            streamInfo.QuicTransport.Security = EnumExtensions.ParseEunmEx<SecurityMode>(query["quicSecurity"] ?? "none");
                            streamInfo.QuicTransport.Key = HttpUtility.UrlDecode(query["key"] ?? string.Empty);
                            if (!Global.KcpOrQuicFeignItems.Contains(streamInfo.QuicTransport.Feign) || !Global.QuicSecurityModeItems.Contains(streamInfo.QuicTransport.Security))
                            {
                                throw new Exception();
                            }

                        }break;
                    case "grpc":
                        {
                            streamInfo.Transport = TransportType.grpc;
                            streamInfo.GrpcTranport.ServiceName = HttpUtility.UrlDecode(query["serviceName"] ?? string.Empty);
                        }
                        break;
                }
                ret.SetProtocolInfoObj(trojanInfo);
                ret.SetStreamInfo(streamInfo);
                return ret;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

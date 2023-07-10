using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProxyController.Modle.Server;
using NetProxyController.Modle;
using NetProxyController.View;
using XrayCoreConfigModle;
using Vanara.Extensions.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Drawing;

namespace NetProxyController.Handler.SubResolve
{
    internal static class VmessResolver
    {
        public const string PrefixMatch = "vmess://";
        public static ServerItem? ResolveByJson(string subContent)
        {
            if(!subContent.StartsWith(PrefixMatch))
            {
                return null;
            }
            subContent = subContent[PrefixMatch.Length..];
            if(Tools.EncodeHelper.TryConvertFromBase64(subContent, out string outPut))
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
                    Port = Convert.ToInt32(jsonObj.port),
                    Remarks = jsonObj.ps
                };
                var userVerific = new VmessInfo()
                {
                    Security = string.IsNullOrEmpty(jsonObj.scy) ? SecurityMode.Auto : EnumExtensions.ParseEunmEx<SecurityMode>(jsonObj.scy),
                    Id = jsonObj.id,
                    AlterId = Convert.ToInt32(jsonObj.aid)
                };
                var StreamInfo = new StreamInfo()
                {
                    Transport = string.IsNullOrEmpty(jsonObj.net) ? TransportType.tcp : EnumExtensions.ParseEunmEx<TransportType>(jsonObj.net),
                    Security = string.IsNullOrEmpty(jsonObj.tls) ? TransportSecurity.tls : EnumExtensions.ParseEunmEx<TransportSecurity>(jsonObj.tls)
                };
                var feign = string.IsNullOrEmpty(jsonObj.type) ? FeignType.none : EnumExtensions.ParseEunmEx<FeignType>(jsonObj.type);
                switch(StreamInfo.Transport)
                {
                    case TransportType.tcp:
                        {
                            if(!Global.TcpFeignItems.Contains(feign))
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
                        }break;
                    case TransportType.quic:
                        {
                            if(!Global.KcpOrQuicFeignItems.Contains(feign))
                            {
                                throw new Exception();
                            }
                            var security = string.IsNullOrEmpty(jsonObj.host) ? SecurityMode.None : EnumExtensions.ParseEunmEx<SecurityMode>(jsonObj.host);
                            if(!Global.QuicSecurityModeItems.Contains(security))
                            {
                                throw new Exception();
                            }
                            StreamInfo.QuicTransport.Feign = feign;
                            StreamInfo.QuicTransport.Security = security;
                            StreamInfo.QuicTransport.Key = jsonObj.path;
                        }break;
                    case TransportType.ws:
                        {
                            if(!string.IsNullOrEmpty(jsonObj.host))
                            {
                                StreamInfo.WsTransport.Headers = new Dictionary<string, string>
                                {
                                    {"Host",jsonObj.host }
                                };
                            }
                            StreamInfo.WsTransport.Path = jsonObj.path;
                        }break;
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
                        }break;
                    case TransportType.grpc:
                        {
                            StreamInfo.GrpcTranport.ServiceName = jsonObj.path;
                        }break;
                }
                switch(StreamInfo.Security)
                {
                    case TransportSecurity.tls:
                        {
                            StreamInfo.TlsPolicy.FingerPrint = string.IsNullOrEmpty(jsonObj.fp) ? TlsFingerPrint.none : EnumExtensions.ParseEunmEx<TlsFingerPrint>(jsonObj.fp);
                            StreamInfo.TlsPolicy.ServerName = jsonObj.sni;
                            if(!string.IsNullOrEmpty(jsonObj.alpn))
                            {
                                StreamInfo.TlsPolicy.Alpn = jsonObj.alpn.Split(',').ToList();
                            }
                        }break;
                    case TransportSecurity.xtls:
                        {
                            StreamInfo.XTlsPolicy.FingerPrint = string.IsNullOrEmpty(jsonObj.fp) ? TlsFingerPrint.none : EnumExtensions.ParseEunmEx<TlsFingerPrint>(jsonObj.fp);
                            if (!string.IsNullOrEmpty(jsonObj.alpn))
                            {
                                StreamInfo.XTlsPolicy.Alpn = jsonObj.alpn.Split(',').ToList();
                            }
                        }break;
                }
                ret.SetProtocolInfoObj(userVerific);
                ret.SetStreamInfo(StreamInfo);
                return ret;
            }
            catch(Exception)
            {
                return null;
            }
        }
        private static readonly Regex StdVmessUserInfo = new(
            @"^(?<network>[a-z]+)(\+(?<streamSecurity>[a-z]+))?:(?<id>[0-9a-f]{8}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{4}-[0-9a-f]{12})$", RegexOptions.Compiled);

        public static ServerItem? ResolveByStdUrl(string SubContent)
        {
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
                switch(m.Groups["network"].Value)
                {
                    case "tcp":
                        {
                            streamInfo.Transport = TransportType.tcp;
                            streamInfo.TcpTransport.Feign = EnumExtensions.ParseEunmEx<FeignType>(q["type"] ?? "none");
                            if(!Global.TcpFeignItems.Contains(streamInfo.TcpTransport.Feign))
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
                            if(!Global.QuicSecurityModeItems.Contains(streamInfo.QuicTransport.Security) || !Global.KcpOrQuicFeignItems.Contains(streamInfo.QuicTransport.Feign))
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
            catch(Exception)
            {
                return null;
            }
        }
    }
}

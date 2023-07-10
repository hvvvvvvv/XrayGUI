﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProxyController.Modle.Server;
using NetProxyController.Modle;
using NetProxyController.View;
using XrayCoreConfigModle;
using Vanara.Extensions.Reflection;

namespace NetProxyController.Handler.SubResolve
{
    internal static class VmessResolvecs
    {
        public const string PrefixMatch = "vmess://";
        public static ServerItem? ResolveByJson(string subContent)
        {
            if(!subContent.StartsWith(PrefixMatch))
            {
                return null;
            }
            if (!JsonHandler.TryJsonDeserializeFromText(subContent[PrefixMatch.Length..], out Modle.Subscription.VmessbyJson? jsonObj) || jsonObj is null)
            {
                return null;
            }
            try
            {
                var ret = new ServerItem()
                {
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
        public static ServerItem? ResoveByJsonBase64(string subContent)
        {

        }
    }
}
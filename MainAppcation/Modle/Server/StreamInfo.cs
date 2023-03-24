using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NetProxyController.Modle;
using SQLite;
using XrayCoreConfigModle.ProtocolSetting;

namespace NetProxyController.Modle.Server
{
    internal class StreamInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public TransportType Transport { get; set; }
        public int TransportIndex { get; set; }
        public TransportSecurity Security { get; set; }
        public int SecurityIndex { get; set; }

        public StreamSettingsObject ToStreamSettingsObject()
        {
            var ret = new StreamSettingsObject()
            {
                network = Transport.ToString(),
                security = Security.ToString(),
            };
            switch(Transport)
            {
                case TransportType.tcp:
                    ret.tcpSettings = Global.DBService.Find<TcpInfo>(TransportIndex).ToTcpObject();
                    break;
                case TransportType.kcp:
                    ret.kcpSettings = Global.DBService.Find<KcpInfo>(TransportIndex).ToKcpObject();
                    break;
                case TransportType.http:
                    ret.httpSettings = Global.DBService.Find<H2Info>(TransportIndex).ToHttpObject();
                    break;
                case TransportType.ws:
                    ret.wsSettings = Global.DBService.Find<WebSocketInfo>(TransportIndex).ToWebSocketObject();
                    break;
                case TransportType.quic:
                    ret.quicSettings = Global.DBService.Find<QuicInfo>(TransportIndex).ToQuicObject();
                    break;
                case TransportType.grpc:
                    ret.grpcSettings = Global.DBService.Find<GrpcInfo>(TransportIndex).ToGRPCObject();
                    break;
            }
            switch(Security)
            {
                case TransportSecurity.tls:
                    ret.tlsSettings = Global.DBService.Find<TlsInfo>(SecurityIndex).ToTLSObject();
                    break;
                case TransportSecurity.xtls:
                    ret.xtlsSettings = Global.DBService.Find<TlsInfo>(SecurityIndex).ToTLSObject();
                    break;
                case TransportSecurity.reality:
                    ret.realitySettings = Global.DBService.Find<RealityInfo>(SecurityIndex).ToRealityObject();
                    break;
            }
            return ret;                
        }
    }
}

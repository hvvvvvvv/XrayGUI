using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XrayGUI.Modle;
using SQLite;
using XrayCoreConfigModle.ProtocolSetting;

namespace XrayGUI.Modle.Server
{
    public class StreamInfo
    {
        public TransportType Transport { get; set; }
        private TcpInfo? _TcpTransport;
        public TcpInfo TcpTransport 
        {
            get
            {
                _TcpTransport ??= new();
                return _TcpTransport;
            }
            set =>_TcpTransport = value;
        }
        private KcpInfo? _KcpTransport;
        public KcpInfo KcpTransport
        {
            get
            {
                _KcpTransport ??= new();
                return _KcpTransport;
            }
            set =>_KcpTransport = value;
        }
        private H2Info? _H2Transport;
        public H2Info H2Transport
        {
            get
            {
                _H2Transport ??= new();
                return _H2Transport;
            }
            set =>_H2Transport = value;
        }
        private WebSocketInfo? _WebSocketTransport;
        public WebSocketInfo WsTransport
        {
            get
            {
                _WebSocketTransport ??= new();
                return _WebSocketTransport;
            }
            set =>_WebSocketTransport = value;
        }
        private QuicInfo? _QuicTransport;
        public QuicInfo QuicTransport
        {
            get
            {
                _QuicTransport ??= new();
                return _QuicTransport;
            }
            set =>_QuicTransport = value;
        }
        private GrpcInfo? _GrpcTransport;
        public GrpcInfo GrpcTranport
        {
            get
            {
                _GrpcTransport ??= new();
                return _GrpcTransport;
            }
            set =>_GrpcTransport = value;
        }
        public TransportSecurity Security { get; set; }
        private TlsInfo? _TlsTransport;
        public TlsInfo TlsPolicy
        {
            get
            {
                _TlsTransport ??= new();
                return _TlsTransport;
            }
            set =>_TlsTransport = value;
        }
        private TlsInfo? _XTlsPolicy;
        public TlsInfo XTlsPolicy
        {
            get
            {
                _XTlsPolicy ??= new();
                return _XTlsPolicy;
            }
            set =>_XTlsPolicy = value;
        }
        private RealityInfo? _RealityPolicy;
        public RealityInfo RealityPolicy
        {
            get
            {
                _RealityPolicy ??= new();
                return _RealityPolicy;
            }
            set =>_RealityPolicy = value;
        }
        
        public StreamSettingsObject ToStreamSettingsObject()
        {
            var ret = new StreamSettingsObject()
            {
                network = Transport != TransportType.none ? Transport.ToString() : null,
                security = Security.ToString()
            };
            switch(Transport)
            {
                case TransportType.tcp:
                    ret.tcpSettings = TcpTransport.ToTcpObject();
                    break;
                case TransportType.kcp:
                    ret.kcpSettings = KcpTransport.ToKcpObject();
                    break;
                case TransportType.http:
                    ret.httpSettings = H2Transport.ToHttpObject();
                    break;
                case TransportType.ws:
                    ret.wsSettings = WsTransport.ToWebSocketObject();
                    break;
                case TransportType.quic:
                    ret.quicSettings = QuicTransport.ToQuicObject();
                    break;
                case TransportType.grpc:
                    ret.grpcSettings = GrpcTranport.ToGRPCObject();
                    break;
            }
            switch(Security)
            {
                case TransportSecurity.tls:
                    ret.tlsSettings = TlsPolicy.ToTLSObject();
                    break;
                case TransportSecurity.xtls:
                    ret.xtlsSettings = XTlsPolicy.ToTLSObject();
                    break;
                case TransportSecurity.reality:
                    ret.realitySettings = RealityPolicy.ToRealityObject();
                    break;
            }
            return ret;                
        }
    }
}

using System;
using System.Collections.Generic;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using NetProxyController.Modle;
using SQLite;
using XrayCoreConfigModle.ProtocolSetting;

namespace NetProxyController.Modle.Server
{
    internal class TcpInfo
    {
        public bool AcceptProxyProtocol { get; set; } = false;
        public FeignType Feign { get; set; }
        public string Version { get; set; } = "1.1";
        public HttpRequestType Method { get; set; } = HttpRequestType.GET;
        public string Path { get; set; } = "/";
        public string? HeaderId { get; set; }
        public Dictionary<string,List<string>>? Headers { get; set; }
        public TcpObject ToTcpObject()
        {
            HeaderObject? _Header = Feign switch
            {
                FeignType.http => new HttpHeaderObject()
                {
                    request = new()
                    {
                        version = Version,
                        path = Path.Split(',').ToList(),
                        method = Method.ToString(),
                        headers = Headers
                    }
                },
                FeignType.none => new NoneHeaderObject(),
                _ => null
            };
            return new()
            {
                acceptProxyProtocol = AcceptProxyProtocol,
                header = _Header,
            };
        }
    }
    internal class KcpInfo
    {
        public int Mtu { get; set; } = 1350;
        public int TTI { get; set; } = 20;
        public int UplinkCapacity { get; set; } = 5;
        public int DownlinkCapacity { get; set; } = 20;
        public int ReadBufferSize { get; set; } = 2;
        public int WriteBufferSize { get; set; } = 2;
        public bool Congestion { get; set; } = false;
        public string Seed { get; set; } = string.Empty;
        public FeignType Feign { get; set; }

        public KcpObject ToKcpObject()
        {
            return new()
            {
                mtu = Mtu,
                tti = TTI,
                uplinkCapacity = UplinkCapacity,
                downlinkCapacity = DownlinkCapacity,
                readBufferSize = ReadBufferSize,
                writeBufferSize = WriteBufferSize,
                congestion = Congestion,
                seed = string.IsNullOrEmpty(Seed) ? null : Seed,
                header = new()
                {
                    type = Feign.GetStringValue()
                }
            };
        }
    }
    internal class WebSocketInfo
    {
        public bool AcceptProxyProtocol { get; set; } = false;
        public string Path { get; set; } = "/";
        public Dictionary<string,string>? Headers { get; set; }
        public WebSocketObject ToWebSocketObject()
        {
            return new()
            {
                acceptProxyProtocol = AcceptProxyProtocol,
                path = Path,
                headers = Headers
            };
        }
    }

    internal class H2Info
    {
        public string Hosts { get; set; } = string.Empty;
        public string Path { get; set; } = "/";
        public int ReadIdleTimeout { get; set; } = 0;
        public int HealthCheckTimeout { get; set; } = 15;
        public HttpRequestType Method { get; set; } = HttpRequestType.PUT;
        Dictionary<string,List<string>>? Headers { get; set; }
        public HttpObject ToHttpObject()
        {
            return new()
            {
                host = string.IsNullOrEmpty(Hosts) ? null : Hosts.Split(',').ToList(),
                path = Path,
                read_idle_timeout = ReadIdleTimeout <= 0 ? null : ReadIdleTimeout,
                health_check_timeout = HealthCheckTimeout,
                method = Method.ToString(),
                headers = Headers
            };
        }
    }
    internal class QuicInfo
    {
        public SecurityMode Security { get; set; } = SecurityMode.None;
        public string Key { get; set; } = string.Empty;
        public FeignType Feign { get; set; }
        public QuicObject ToQuicObject()
        {
            return new()
            {
                security = Security.ToString(),
                key = string.IsNullOrEmpty(Key) ? null : Key,
                header = new()
                {
                    type = Feign.GetStringValue()
                }
            };
        }
    }
    internal class GrpcInfo
    {
        public string ServiceName { get; set; } = string.Empty;
        public int IdleTimeout { get; set; } = 10;
        public int HealthCheckTimeout { get; set; } = 20;
        public bool PermitWithoutStream { get; set; } = false;
        public int InitialWindowsSize { get; set; } = 0;
        public GRPCObject ToGRPCObject()
        {
            return new()
            {
                serviceName = ServiceName,
                idle_timeout = IdleTimeout <= 0 ? null : IdleTimeout,
                health_check_timeout = HealthCheckTimeout,
                permit_without_stream = PermitWithoutStream,
                initial_windows_size = InitialWindowsSize
            };
        }
    }
}

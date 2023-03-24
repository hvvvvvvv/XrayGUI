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
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public bool AcceptProxyProtocol { get; set; } = false;
        public FeignType Feign { get; set; }
        public string Version { get; set; } = "1.1";
        public HttpRequestType Method { get; set; } = HttpRequestType.GET;
        public string Path { get; set; } = "/";
        public string? HeaderId { get; set; }
        public string Accept_Encoding { get; set; } = "gzip, deflate";
        public string Connection { get; set; } = "keep-alive";
        public string Pragma { get; set; } = "no-cache";
        public TcpObject ToTcpObject()
        {
            List<FeignHeader> headers = Global.DBService.Table<FeignHeader>().Where(i => i.Id == HeaderId).ToList();
            var requestHeaders = new Dictionary<string, List<string>>();
            foreach(var head in headers)
            {
                requestHeaders.Add(head.HeaderKey, head.HeaderValue.Split(',').ToList());
            }
            HeaderObject? _Header = Feign switch
            {
                FeignType.http => new HttpHeaderObject()
                {
                    request = new()
                    {
                        version = Version,
                        path = Path.Split(',').ToList(),
                        method = Method.ToString(),
                        headers = requestHeaders
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
        [PrimaryKey]
        [AutoIncrement]
        public  int Index { get; set; }
        public int Mtu { get; set; } = 1350;
        public int TTI { get; set; } = 20;
        public int UplinkCapacity { get; set; } = 5;
        public int DownlinkCapacity { get; set; } = 20;
        public int ReadBufferSize { get; set; } = 2;
        public int WriteBufferSize { get; set; } = 2;
        public bool Congestion { get; set; } = false;
        public string? Seed { get; set; }
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
                seed = Seed,
                header = new()
                {
                    type = Feign.GetStringValue()
                }
            };
        }
    }
    internal class WebSocketInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public bool AcceptProxyProtocol { get; set; } = false;
        public string Path { get; set; } = "/";
        public string? HeaderId { get; set; }
        public WebSocketObject ToWebSocketObject()
        {
            var headers = Global.DBService.Table<FeignHeader>().Where(i => i.Id == HeaderId).ToList();
            var httpHeaders = new Dictionary<string, string>();
            headers.ForEach(i => httpHeaders.Add(i.HeaderKey, i.HeaderValue));
            return new()
            {
                acceptProxyProtocol = AcceptProxyProtocol,
                path = Path,
                headers = httpHeaders
            };
        }
    }

    internal class H2Info
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string Hosts { get; set; } = string.Empty;
        public string Path { get; set; } = "/";
        public int ReadIdleTimeout { get; set; } = 0;
        public int HealthCheckTimeout { get; set; } = 15;
        public HttpRequestType Method { get; set; } = HttpRequestType.PUT;
        string? HeaderId { get; set; }
        public HttpObject ToHttpObject()
        {
            var headers = Global.DBService.Table<FeignHeader>().Where(i => i.Id == HeaderId).ToList();
            var httpHeaders = new Dictionary<string, List<string>>();
            foreach(var head in headers)
            {
                httpHeaders.Add(head.HeaderKey, head.HeaderValue.Split(',').ToList());
            }
            return new()
            {
                host = Hosts.Split(',').ToList(),
                path = Path,
                read_idle_timeout = ReadIdleTimeout <= 0 ? null : ReadIdleTimeout,
                health_check_timeout = HealthCheckTimeout,
                method = Method.ToString(),
                headers = httpHeaders
            };
        }
    }
    internal class QuicInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public SecurityMode Security { get; set; } = SecurityMode.None;
        public string Key { get; set; } = string.Empty;
        public FeignType Feign { get; set; }
        public QuicObject ToQuicObject()
        {
            return new()
            {
                security = Security.ToString(),
                key = Key,
                header = new()
                {
                    type = Feign.GetStringValue()
                }
            };
        }
    }
    internal class GrpcInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string ServiceName { get; set; } = string.Empty;
        public int IdleTimeout { get; set; } = 0;
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
    internal class FeignHeader
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string HeaderKey { get; set; } = string.Empty;
        public string HeaderValue { get; set; } = string.Empty;
        public string? Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Windows;
using Vanara.Extensions.Reflection;

namespace NetProxyController
{
    public class ProxyServerInfo
    {
        private string _ServerPort = string.Empty;
        public string ServerPort
        {
            get
            {
                return _ServerPort;
            }
            set
            {
                if (int.TryParse(value, out int number) && number >= 1 && number <= 65535)
                {
                    _ServerPort = value;
                }
                else
                {
                    throw new ExceptionByPortNumberValidationFailed();
                }
            }
        }
        public string ServerAddr { get; set; } = default!;
        public ProxyType Protocol { get; set; } = default!;

        public override string ToString()
        {
            switch(Protocol)
            {
                case ProxyType.Http:
                    return $"{ServerAddr}:{ServerPort}";
                case ProxyType.Socks:
                    return $"socks={ServerAddr}:{ServerPort}";
                default:
                    return base.ToString()!;
            }

            
        }
        public ProxyServerInfo(string server)
        {
            string dnsNamePattern = @"[a-zA-Z][a-zA-Z0-9]+(\.[a-zA-Z0-9]+)*";
            string ipAddrPattern = @"(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)";
            string pattern = $"({ipAddrPattern})|({dnsNamePattern})";
            Match match;
            var matchHelper = (string _pattern, out Match _match) =>
            {
                _match = Regex.Match(server, _pattern);
                return _match.Success;
            };

            if (matchHelper($"(?i)^http://({pattern}):([0-9]+$)",out match))           {
                ServerAddr = match.Groups[1].Value;
                Protocol = ProxyType.Http;
                ServerPort = match.Groups[match.Groups.Count - 1].Value;
            }
            else if(matchHelper($"(?i)^http://({pattern})$", out match))
            {
                ServerAddr = match.Groups[1].Value;
                Protocol = ProxyType.Http;
                ServerPort = "80";
            }
            else if(matchHelper($"(?i)^({pattern})$", out match))
            {
                ServerAddr = match.Groups[0].Value;
                Protocol = ProxyType.Http;
                ServerPort = "80";
            }
            else if(matchHelper($"(?i)^({pattern}):([0-9]+$)$", out match))
            {
                ServerAddr = match.Groups[1].Value;
                Protocol = ProxyType.Http;
                ServerPort = match.Groups[match.Groups.Count - 1].Value;
            }
            else if (matchHelper($"(?i)^socks=({pattern})$", out match))
            {
                ServerAddr = match.Groups[1].Value;
                Protocol = ProxyType.Socks;
                ServerPort = "1080";
            }
            else if (matchHelper($"(?i)^socks=({pattern}):([0-9]+)$", out match))
            {
                ServerAddr = match.Groups[1].Value;
                Protocol = ProxyType.Socks;
                ServerPort = match.Groups[match.Groups.Count - 1].Value;
            }
            else
            {
                ServerAddr = "127.0.0.1";
                ServerPort = "80";
                Protocol = ProxyType.Http;
            }
        }


    }

    public class ExceptionByPortNumberValidationFailed : Exception
    {
        public ExceptionByPortNumberValidationFailed(): base("端口号格式不正确")
        {
            
        }
    }
    public class ExceptionByServerInfoValidationFailed: Exception
    {
        public ExceptionByServerInfoValidationFailed() : base("代理服务器字符串格式错误")
        {
        }
    }
    public enum ProxyType { Http,Socks }
}

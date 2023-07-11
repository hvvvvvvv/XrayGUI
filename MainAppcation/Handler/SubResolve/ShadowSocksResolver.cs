using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetProxyController.Modle;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using System.DirectoryServices;
using System.Collections.Specialized;
using System.Web;
using NetProxyController.Modle.Subscription;
using XrayCoreConfigModle;

namespace NetProxyController.Handler.SubResolve
{
    internal static class ShadowSocksResolver
    {
        public const string PrefixMatch = "ss://";
        private static readonly Regex SSUrlRegex = new(PrefixMatch + @"(?<base64>[A-Za-z0-9+-/=_]+)(?:#(?<tag>\S+))?", RegexOptions.IgnoreCase | RegexOptions.Compiled);
        private static readonly Regex SSDetailsRegex = new(@"^((?<method>.+?):(?<password>.*)@(?<hostname>.+?):(?<port>\d+?))$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        public static ServerItem? ResolveByUrlType01(string subContent)
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
            catch(Exception)
            {
                return null;
            }
        }
        public static ServerItem? ResolveByUrlType02(string subContent)
        {
            if(!subContent.StartsWith(PrefixMatch)) return null;
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
                if(Tools.EncodeHelper.TryConvertFromBase64(rawUserInfo, out string decodedText))
                {
                    rawUserInfo = decodedText;
                }
                string[] userInfoParts = rawUserInfo.Split(new[] { ':' }, 2);
                if(userInfoParts.Length != 2) throw new Exception();
                ShadowSocksInfo shadowSocksInfo = new();
                shadowSocksInfo.Method = EnumExtensions.ParseEunmEx<SS_Ecrept>(userInfoParts[0]);
                string pwd = Tools.EncodeHelper.TryDecodeFromUrlCode(userInfoParts[1], out string decoded) ? decoded : userInfoParts[1];
                shadowSocksInfo.Password = pwd;
                ret.SetProtocolInfoObj(shadowSocksInfo);
                NameValueCollection queryParameters = HttpUtility.ParseQueryString(u.Query);
                if(queryParameters["plugin"] != null)
                {                   
                    var obfsHost = queryParameters["plugin"]!.Split(';').FirstOrDefault(t => t.Contains("obfs-host"));
                    if(queryParameters["plugin"]!.Contains("obfs=http") && !string.IsNullOrEmpty(obfsHost))
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
            catch(Exception) { return null; }
        }

        public static List<ServerItem>? ResolveByJson(string subcontent)
        {
            List<ShadowSocksItem>? SSserverItems;
            if(JsonHandler.TryJsonDeserializeFromText<List<ShadowSocksItem>>(subcontent, out var outPut))
            {
                SSserverItems = outPut;
            }
            else if(JsonHandler.TryJsonDeserializeFromText<ShadowSokcsJson>(subcontent, out var json))
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
    }
}

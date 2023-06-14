using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XrayCoreConfigModle;
using NetProxyController.Modle;

namespace NetProxyController.Modle.Server
{
    internal class ServerItem
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; } = -1;
        public OutboundProtocol Protocol { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Port { get; set; }
        public string Remarks { get; set; } = string.Empty;
        private string protocolInfoContent = string.Empty;
        public string ProtocolInfoContent
        {
            get => protocolInfoContent;
            set
            {
                if(string.IsNullOrEmpty(protocolInfoContent))
                {
                    protocolInfoContent = value;
                    try
                    {
                        protocolInfoObj = Protocol switch
                        {
                            OutboundProtocol.socks => JsonSerializer.Deserialize<SocksInfo>(protocolInfoContent),
                            OutboundProtocol.vless => JsonSerializer.Deserialize<VlessInfo>(protocolInfoContent),
                            OutboundProtocol.vmess => JsonSerializer.Deserialize<VmessInfo>(protocolInfoContent),
                            OutboundProtocol.trojan => JsonSerializer.Deserialize<TrojanInfo>(protocolInfoContent),
                            OutboundProtocol.shadowsocks => JsonSerializer.Deserialize<ShadowSocksInfo>(protocolInfoContent),
                            _ => null
                        };
                    }
                    catch { }
                }
            }
        }
        public OutBoundConfiguration? GetProtocolInfoObj() => protocolInfoObj;
        public void SetProtocolInfoObj(OutBoundConfiguration obj) => protocolInfoObj = obj;
        private OutBoundConfiguration? protocolInfoObj;

        private StreamInfo? streamInfoObj;
        private string streamInfoContent = string.Empty;
        public string StreamInfoContent
        {
            get => streamInfoContent;
            set
            { 
                if(string.IsNullOrEmpty(streamInfoContent))
                {
                    streamInfoContent = value;
                    try
                    {
                        streamInfoObj = JsonSerializer.Deserialize<StreamInfo>(streamInfoContent);
                    }
                    catch { }
                }
            }
        }
        public StreamInfo GetStreamInfo() => streamInfoObj ?? new();
        public void SetStreamInfo(StreamInfo obj) => streamInfoObj = obj;
        public OutboundServerItemObject ToOutboundServerItemObject()
        {
            return new()
            {
                protocol = Protocol.ToString(),
                tag = Index.ToString(),
                settings = protocolInfoObj?.ToOutboundConfigurationObject(Address, Port),
                streamSettings = streamInfoObj?.ToStreamSettingsObject(),
            };
        }
        public void SaveToDataBase()
        {
            protocolInfoContent = protocolInfoObj is null ? string.Empty : JsonSerializer.Serialize(protocolInfoObj,protocolInfoObj.GetType());
            streamInfoContent = streamInfoObj is null ? string.Empty : JsonSerializer.Serialize(streamInfoObj);
            if(Index == -1)
            {
                Global.DBService.Insert(this);
                return;
            }
            Global.DBService.InsertOrReplace(this);
        }

        public static List<ServerItem> GetItemsFromDataBase()
        {
            return Global.DBService.Table<ServerItem>().ToList();
        }
    }
}

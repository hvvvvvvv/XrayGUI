using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XrayCoreConfigModle;
using XrayGUI.Modle;
using System.Collections.ObjectModel;

namespace XrayGUI.Modle.Server
{
    public class ServerItem
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; } = -1;
        public OutboundProtocol Protocol { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Port { get; set; }
        public bool IsActivated { get; set; }
        public Guid? SubGroupId { get; set; }
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
            lock (serverItemsDataList)
            {
                if (serverItemsDataList.Any(i => i.Index == Index))
                {
                    Global.DBService.Update(this);
                }
                else
                {
                    Global.DBService.Insert(this);
                    serverItemsDataList.Add(this);
                }
            }
                
        }
        public void DeleteFromDataBase()
        {
            if (Index == -1) return;
            lock (serverItemsDataList)
            {
                Global.DBService.Delete<ServerItem>(Index);
                serverItemsDataList.Remove(this);
            }
        }
        private static List<ServerItem> serverItemsDataList = GetItemsFromDataBase();
        public static ReadOnlyCollection<ServerItem> ServerItemsDataList { get; } = new ReadOnlyCollection<ServerItem>(serverItemsDataList);
        private static List<ServerItem> GetItemsFromDataBase()
        {
            return Global.DBService.Table<ServerItem>().ToList();
            
        }
    }
}

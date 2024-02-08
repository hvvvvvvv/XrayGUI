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
        public string ProtocolInfoContent { get; set; } = string.Empty;
        public OutBoundConfiguration? GetProtocolInfoObj()
        {
            if (!string.IsNullOrEmpty(ProtocolInfoContent))
            {
                try
                {
                    return Protocol switch
                    {
                        OutboundProtocol.socks => JsonSerializer.Deserialize<SocksInfo>(ProtocolInfoContent),
                        OutboundProtocol.vless => JsonSerializer.Deserialize<VlessInfo>(ProtocolInfoContent),
                        OutboundProtocol.vmess => JsonSerializer.Deserialize<VmessInfo>(ProtocolInfoContent),
                        OutboundProtocol.trojan => JsonSerializer.Deserialize<TrojanInfo>(ProtocolInfoContent),
                        OutboundProtocol.shadowsocks => JsonSerializer.Deserialize<ShadowSocksInfo>(ProtocolInfoContent),
                        _ => null
                    };
                }
                catch { }
            }
            return null;
        }
        public void SetProtocolInfoObj(OutBoundConfiguration? obj) => ProtocolInfoContent =  obj is null ? string.Empty : JsonSerializer.Serialize(obj, obj.GetType());
        public string StreamInfoContent { get; set; } = string.Empty;
        public StreamInfo GetStreamInfo()
        {
            if (!string.IsNullOrEmpty(StreamInfoContent))
            {
                try
                {
                    return JsonSerializer.Deserialize<StreamInfo>(StreamInfoContent) ?? new();
                }
                catch { }
            }
            return new();
        }
        public void SetStreamInfo(StreamInfo obj) => StreamInfoContent = JsonSerializer.Serialize(obj);
        public OutboundServerItemObject ToOutboundServerItemObject()
        {
            return new()
            {
                protocol = Protocol.ToString(),
                tag = Index.ToString(),
                settings = GetProtocolInfoObj()?.ToOutboundConfigurationObject(Address, Port),
                streamSettings = GetStreamInfo().ToStreamSettingsObject()
            };
        }
        public void SaveToDataBase()
        {
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

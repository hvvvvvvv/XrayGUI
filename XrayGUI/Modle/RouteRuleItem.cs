using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using XrayGUI.Modle;
using XrayCoreConfigModle.Routing;

namespace XrayGUI.Modle
{
    internal class RouteRuleItem
    {
        [PrimaryKey]
        public Guid Index { get; set; }
        public int Priority { get; set; }
        public DomainMacher? DomainMatcher { get; set; }
        public string? MatchDoamin { get; set; }
        public string? MatchIP { get; set; }
        public string? MatchPort { get; set; }
        public TransportProtocol TransportProtocol { get; set; }
        public ApplicationProtocol ApplicationProtocol { get; set; }
        public int OutboundServerIndex { get; set; } = -1;
        public bool IsActivated { get; set; }
        public string Remarks { get; set; } = string.Empty;
        public void SaveToDataBase()
        {
            lock (_RouteRuleItemDataList)
            {
                if (Index == default)
                {
                    Global.DBService.Insert(this);
                    _RouteRuleItemDataList.Add(this);
                }
                else
                {
                    Global.DBService.Update(this);
                }
            }
        }
        public void DelateFormDataBase()
        {
            lock (_RouteRuleItemDataList)
            {
                Global.DBService.Delete(this);
                _RouteRuleItemDataList.Remove(this);
            }
        }
        public RuleObject? ToRuleObject()
        {
            var outBoundServer = RouteRuleItemDataList.Where(x => x.OutboundServerIndex == OutboundServerIndex).FirstOrDefault();
            List<string> network = new();
            List<string> protocol = new();
            if (outBoundServer is null)
            {
                return null;
            }
            foreach (TransportProtocol p in Enum.GetValues(typeof(TransportProtocol)))
            {
                if ((TransportProtocol & p) != 0)
                {
                    network.Add(p.ToString());
                }
            }
            foreach (ApplicationProtocol p in Enum.GetValues(typeof(ApplicationProtocol)))
            {
                if ((ApplicationProtocol & p) != 0)
                {
                    protocol.Add(p.ToString());
                }
            }            
            return new()
            {
                domainMatcher = DomainMatcher?.ToString(),
                domain = string.IsNullOrEmpty(MatchDoamin) ? null : MatchDoamin.Split(',').ToList(),
                ip = string.IsNullOrEmpty(MatchIP) ? null : MatchIP.Split(',').ToList(),
                port = MatchPort,
                network = network.Count > 0 ? string.Join(',', network.ToArray()) : null,
                protocol = protocol.Count > 0 ? protocol : null,
                outboundTag = outBoundServer.Index.ToString(),
            };
            
        }
        private static List<RouteRuleItem> _RouteRuleItemDataList = Global.DBService.Table<RouteRuleItem>().ToList();
        public static readonly ReadOnlyCollection<RouteRuleItem> RouteRuleItemDataList = new(_RouteRuleItemDataList);
    }
}

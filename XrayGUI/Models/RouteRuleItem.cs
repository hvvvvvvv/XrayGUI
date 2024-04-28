using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using XrayGUI.Modle;
using XrayCoreConfigModle.Routing;
using XrayGUI.Modle.Server;

namespace XrayGUI.Modle
{
    internal class RouteRuleItem
    {
        [PrimaryKey]
        public Guid Identity { get; set; }
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
        public string ProcessName { get; set; } = string.Empty;
        public string ProcessPath { get; set; } = string.Empty;
        public Guid OwnerPolicyId { get; set; }
        public void Save()
        {
            if(Identity == default || Global.DBService.Find<RouteRuleItem>(Identity) == default)
            {
                Global.DBService.Insert(this);
            }
            else
            {
                Global.DBService.Update(this);
            }
        }
        public bool Delate() => Global.DBService.Delete(this) > 0;
        public RouteRuleItem Copy()
        {
            return new RouteRuleItem
            {
                Priority = Priority,
                DomainMatcher = DomainMatcher,
                MatchDoamin = MatchDoamin,
                MatchIP = MatchIP,
                MatchPort = MatchPort,
                TransportProtocol = TransportProtocol,
                ApplicationProtocol = ApplicationProtocol,
                OutboundServerIndex = OutboundServerIndex,
                IsActivated = IsActivated,
                Remarks = Remarks,
                ProcessName = ProcessName,
                ProcessPath = ProcessPath,
                OwnerPolicyId = OwnerPolicyId
            };
        }
    }
}

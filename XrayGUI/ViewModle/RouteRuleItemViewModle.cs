using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using XrayGUI.Modle;
using XrayGUI.Modle.Server;

namespace XrayGUI.ViewModle
{
    internal class RouteRuleItemViewModle : ViewModleBase
    {
        public RouteRuleItem DataItem;
        public RouteRuleItemViewModle(RouteRuleItem dataItem)
        {
            DataItem = dataItem;
        }
        public bool IsSelected { get; set; }
        public string Remarks => DataItem.Remarks;
        public string Dommmain => DataItem.MatchDoamin ?? Global.MatchItemUndefined;
        public string IP => DataItem.MatchIP ?? Global.MatchItemUndefined;
        public string Port => DataItem.MatchPort ?? Global.MatchItemUndefined;
        public string Protocol
        {
            get
            {
                List<string> ps = new();
                foreach (ApplicationProtocol item in Enum.GetValues(typeof(ApplicationProtocol)))
                {
                    if((DataItem.ApplicationProtocol & item) != 0)
                    {
                        ps.Add(item.ToString());
                    }
                }
                return ps.Count == 0 ? Global.MatchItemUndefined : string.Join(",", ps);
            }
        }
        public string OwnerServer
        {
            get
            {
                var server = ServerItem.ServerItemsDataList.Where(x => x.Index == DataItem.OutboundServerIndex).FirstOrDefault();
                if(server is null)
                {
                    return Global.MatchItemUndefined;
                }
                var groupName = Global.DBService.Find<SubscriptionItem>(server.SubGroupId)?.SubcriptionName ?? string.Empty;
                return $"{groupName}/{server.Remarks}";
            }
        }
    }
}

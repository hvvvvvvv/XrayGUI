using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayGUI.Modle;
using XrayGUI.Modle.Server;

namespace XrayGUI.ViewModle
{
    internal class RouteRuleEditViewModle : ViewModleBase
    {
        private RouteRuleItem routeRule;
        public RouteRuleEditViewModle(RouteRuleItem routeRule)
        {
            this.routeRule = routeRule;
            ruleName = this.routeRule.Remarks;
            selectedApplicationProtocol = this.routeRule.ApplicationProtocol;
            selectedDomainMacher = this.routeRule.DomainMatcher;
            isEnable = this.routeRule.IsActivated;
            matchDomain = this.routeRule.MatchDoamin?.Replace(',','\n') ?? string.Empty;
            matchIP = this.routeRule.MatchIP?.Replace(',', '\n') ?? string.Empty;
            proxyServerIndex = this.routeRule.OutboundServerIndex;
        }
        private string ruleName;
        public string RuleName
        {
            get => ruleName;
            set => ruleName = value;
        }
        private ApplicationProtocol selectedApplicationProtocol;
        public bool IsHttp
        {
            get => (selectedApplicationProtocol & ApplicationProtocol.http) == ApplicationProtocol.http;
            set
            {
                if (value)
                {
                    selectedApplicationProtocol |= ApplicationProtocol.http;
                }
                else
                {
                    selectedApplicationProtocol &= ~ApplicationProtocol.http;
                }
                OnPropertyChanged();
            }
        }
        public bool IsTls
        {
            get => (selectedApplicationProtocol & ApplicationProtocol.tls) == ApplicationProtocol.tls;
            set
            {
                if (value)
                {
                    selectedApplicationProtocol |= ApplicationProtocol.tls;                   
                }
                else
                {
                    selectedApplicationProtocol &= ~ApplicationProtocol.tls;
                }
                OnPropertyChanged();
            }
        }
        public bool IsBittorrent
        {
            get => (selectedApplicationProtocol & ApplicationProtocol.bittorrent) == ApplicationProtocol.bittorrent;
            set
            {
                if (value)
                {
                    selectedApplicationProtocol |= ApplicationProtocol.bittorrent;                   
                }
                else
                {
                    selectedApplicationProtocol &= ~ApplicationProtocol.bittorrent;
                }
                OnPropertyChanged();
            }
        }
        private DomainMacher? selectedDomainMacher;
        public bool IsGlobal
        {
            get => selectedDomainMacher is null;
            set
            {
                if (value)
                {
                    selectedDomainMacher = null;
                }
                OnPropertyChanged();
            }
        }
        public bool IsHybrid
        {
            get => selectedDomainMacher == DomainMacher.hybrid;
            set
            {
                if (value)
                {
                    selectedDomainMacher = DomainMacher.hybrid;
                }
                OnPropertyChanged();
            }
        }
        public bool IsLinear
        {
            get => selectedDomainMacher == DomainMacher.linear;
            set
            {
                if (value)
                {
                    selectedDomainMacher = DomainMacher.linear;
                }
                OnPropertyChanged();
            }
        }
        private bool isEnable;
        public bool IsEnable
        {
            get => isEnable;
            set
            {
                isEnable = value;
                OnPropertyChanged();
            }
        }
        private string matchDomain;
        public string MatchDomain
        {
            get => matchDomain;
            set => matchDomain = value;
        }
        private string matchIP;
        public string MatchIP
        {
            get => matchIP;
            set => matchIP = value;
        }
        private int proxyServerIndex;
        public string ProxyServerDescribe
        {
            get
            {
                var server = ServerItem.ServerItemsDataList.FirstOrDefault(i => i.Index == proxyServerIndex);
                if(server is not null)
                {
                    return $"{server.Remarks} || {server.Index}";
                }
                return string.Empty;
            }
        }

    }
}

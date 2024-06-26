﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using XrayGUI.Modle;
using Windows.Globalization.NumberFormatting;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Immutable;

namespace XrayGUI.ViewModle
{
    internal class RouteRulesManagerViewModle : ViewModleBase
    {                         
        public RouteRulesManagerViewModle()
        {
            routeListRuleItemsList = new(Global.DBService.Table<RouteRuleItem>().Select(i => new RouteRuleItemViewModle(i)));
            EditRouteRuleCmd = new(EditRouteRuleExcute);
        }
        private ObservableCollection<RouteRuleItemViewModle> routeListRuleItemsList;
        public ObservableCollection<RouteRuleItemViewModle> RouteLIstRuleItemsList
        {
            get => routeListRuleItemsList;
            private set
            {
                routeListRuleItemsList = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand EditRouteRuleCmd { get; private set; }
        private DomainMacher selectedDomainMacher;
        public DomainMacher SelectedDomainMacher
        {
            get => selectedDomainMacher;
            set
            {
                selectedDomainMacher = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<DomainMacher> DomainMacherItems => Enum.GetValues(typeof(DomainMacher)).Cast<DomainMacher>();
        private DomainStrategy selectedDomainStrategy;
        public DomainStrategy SelectedDomainStrategy
        {
            get => selectedDomainStrategy;
            set
            {
                selectedDomainStrategy = value;
                OnPropertyChanged();
            }
        }
        public IEnumerable<DomainStrategy> DomainStrategyItems => Enum.GetValues(typeof(DomainStrategy)).Cast<DomainStrategy>();
        public void EditRouteRuleExcute()
        {           
            View.RouteRuleEditView.Instance.ShowDialog();
        }

    }
}

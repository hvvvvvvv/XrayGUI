using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara.Collections;
using XrayGUI.Modle;
using XrayGUI.View;

namespace XrayGUI.ViewModle
{
    public class ProxyServerMenuViewModle : ViewModleBase
    {
        public ObservableCollection<ProxyServerListItemViewModle> ProxyServerList { get; private set; } 
        public ProxyServerListItemViewModle? SelectedProxyServer { get; set; }
        public ProxyServerMenuViewModle()
        {
            ProxyServerList = new(OutboundServerItem.GetAll().Select( i => new ProxyServerListItemViewModle(i)));
            AddProxyServerCmd = new(AddProxyServer);
            EditeProxyServerCmd = new(EditeProxyServer);
            RefreshProxyServerListCmd = new(RefreshProxyServerList);
            SaveProxyServersCmd = new(SaveProxyServers);
        }
       
        public RelayCommand AddProxyServerCmd { get; private set; }
        public RelayCommand EditeProxyServerCmd { get; private set; }
        public RelayCommand RefreshProxyServerListCmd { get; private set; }
        public RelayCommand SaveProxyServersCmd { get; private set; }
        private void AddProxyServer()
        {
            var newServer = new ProxyServerListItemViewModle();
            if(new ProxyServerEdit(newServer).ShowDialog() == true)
            {
                ProxyServerList.Add(newServer);
            }
        }
        private void EditeProxyServer()
        {
            if(SelectedProxyServer != null && new ProxyServerEdit(SelectedProxyServer).ShowDialog() != true)
            {
                SelectedProxyServer.ReloadData();
            }
            
        }
        private void RefreshProxyServerList()
        {
            ProxyServerList.Clear();
            OutboundServerItem.GetAll().ForEach(i => ProxyServerList.Add(new ProxyServerListItemViewModle(i)));
        }
        private void SaveProxyServers()
        {
            foreach (var item in ProxyServerList)
            {
                item.SaveData();
            }
        }

    }
}

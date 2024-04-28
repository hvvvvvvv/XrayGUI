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
        public List<ProxyServerListItemViewModle> SelectedProxyServers { get; set; }
        public ProxyServerMenuViewModle()
        {
            ProxyServerList = new(OutboundServerItem.GetAll().Select( i => new ProxyServerListItemViewModle(i)));
            SelectedProxyServers = new();
            AddProxyServerCmd = new(AddProxyServer);
            EditeProxyServerCmd = new(EditeProxyServer);
            RefreshProxyServerListCmd = new(RefreshProxyServerList);
            SaveProxyServersCmd = new(SaveProxyServers);            
            DelateProxyServerCmd = new(DelateProxyServer);
            deletedItems = new();
        }
       
        public RelayCommand AddProxyServerCmd { get; private set; }
        public RelayCommand EditeProxyServerCmd { get; private set; }
        public RelayCommand RefreshProxyServerListCmd { get; private set; }
        public RelayCommand SaveProxyServersCmd { get; private set; }
        public RelayCommand DelateProxyServerCmd { get; private set; }

        private List<ProxyServerListItemViewModle> deletedItems;
        private void DelateProxyServer()
        {
            foreach (var item in SelectedProxyServers)
            {
                ProxyServerList.Remove(item);
                deletedItems.Add(item);
            }
        }
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
            if(SelectedProxyServer is not null && ProxyServerList.Contains(SelectedProxyServer))
            {
                var editServer = ProxyServerListItemViewModle.CreateCopy(SelectedProxyServer);
                if(new ProxyServerEdit(editServer).ShowDialog() == true)
                {
                    ProxyServerList[ProxyServerList.IndexOf(SelectedProxyServer)] = editServer;
                }
            }           
           
        }
        private void RefreshProxyServerList()
        {
            ProxyServerList.Clear();
            OutboundServerItem.GetAll().ForEach(i => ProxyServerList.Add(new ProxyServerListItemViewModle(i)));
            deletedItems.Clear();
        }
        private void SaveProxyServers()
        {
            foreach (var item in ProxyServerList)
            {
                item.SaveData();
            }
            foreach (var item in deletedItems)
            {
                item.DelateData();
            }
            deletedItems.Clear();
        }

    }
}

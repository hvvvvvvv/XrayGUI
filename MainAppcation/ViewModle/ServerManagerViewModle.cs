using CommunityToolkit.Mvvm.Input;
using XrayGUI.Modle;
using XrayGUI.Modle.Server;
using XrayGUI.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Input;
using System.Diagnostics;
using HandyControl.Controls;
using XrayGUI.Handler;
using System.Windows;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Threading;

namespace XrayGUI.ViewModle
{
    internal class ServerManagerViewModle: ViewModleBase
    {
        public ServerManagerViewModle()
        {
            serverItemList = new(GetDateItemFromDataBase());
            CreateProxyServerCmd = new(CreateProxyServerExcute);
            SelectionChangedCmd = new(SelectionChangedCmdExcute);
            EditServerCmd = new(EditProxyServerExcute, () => IsSingleSelectedItem);
            DeleteServerCmd = new(DeleteProxyServerItemExcute, () => IsContainSelectedItems);
            SetDefalutRoutingCmd = new(SetDefalutRoutingExcute, () => IsSingleSelectedItem);
            ImportServerFromClipboardCmd = new(ImportServerFromClipboardCmdExcute);
            SetActivatedServersCmd = new(SetActivatedServersExcute,(_) => IsContainSelectedItems);
            TestNetRelayCmd = new(TestNetRelayExcute, () => !TestDelayExcuting);
            SubManagerCmd = new(SubManagerExcute);
            RefreshListViewCmd = new(RefreshListView, () => IsContainSelectedItems);
            SubcriptionUpdateHandle.Instance.UpdateEvent += e => { if (e.IsCompeleteUpdate) RefreshListView(); };
        }
        private static List<ServerItemViewModle> GetDateItemFromDataBase()
        {
            var ret = new List<ServerItemViewModle>();
            ServerItem.ServerItemsDataList.ToList().ForEach(item => ret.Add(new(item)));
            return ret;
        }
        private ObservableCollection<ServerItemViewModle> serverItemList;
        private int SelectedItemsConut;
        private bool TestDelayExcuting;
        public ObservableCollection<ServerItemViewModle> ServerItemList
        {
            get => serverItemList;
            private set
            {
                serverItemList = value;
                OnPropertyChanged();                
            }
        }
        public RelayCommand CreateProxyServerCmd { get; private set; }
        public RelayCommand SelectionChangedCmd { get; private set; }
        public RelayCommand EditServerCmd { get; private set; }
        public RelayCommand DeleteServerCmd { get; private set; }
        public RelayCommand SetDefalutRoutingCmd { get; private set; }
        public RelayCommand ImportServerFromClipboardCmd { get; private set; }
        public RelayCommand<bool> SetActivatedServersCmd { get; private set; }
        public RelayCommand TestNetRelayCmd { get; private set; }
        public RelayCommand SubManagerCmd { get; private set; }
        public RelayCommand RefreshListViewCmd { get; private set; }
        public bool IsContainSelectedItems => SelectedItemsConut > 0;
        public bool IsSingleSelectedItem => SelectedItemsConut == 1;
        public int SelectedIndex { get; set; } = -1;
        public bool DefaultServerMenuItemChecked => IsSingleSelectedItem && ServerItemList[SelectedIndex].Server.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex;
        private void RefreshListView()
        {
            lock(serverItemList)
            {
                ServerItemList = new(GetDateItemFromDataBase());
            }
        }
        private void SelectionChangedCmdExcute()
        {
            SelectedItemsConut = ServerItemList.Where(item => item.IsSelected).Count();
            OnPropertyChanged(nameof(IsContainSelectedItems));
            OnPropertyChanged(nameof(DefaultServerMenuItemChecked));
            EditServerCmd.NotifyCanExecuteChanged();
            SetDefalutRoutingCmd.NotifyCanExecuteChanged();
            SetActivatedServersCmd.NotifyCanExecuteChanged();
            DeleteServerCmd.NotifyCanExecuteChanged();
            RefreshListViewCmd.NotifyCanExecuteChanged();
            TestNetRelayCmd.NotifyCanExecuteChanged();
        }
        private void CreateProxyServerExcute()
        {
            var NewServer = new ServerItemViewModle();
            if(new ServerSettingWindow(NewServer.Server).ShowDialog() == true)
            {
                NewServer.UpdateData();
                serverItemList.Add(NewServer);
            }
        }
        private void EditProxyServerExcute()
        {
            foreach(var item in ServerItemList)
            {
                if(item.IsSelected)
                {
                    item.EditServerItem();
                    return;
                }
            }
        }
        private void DeleteProxyServerItemExcute()
        {
            if(HandyControl.Controls.MessageBox.Show(messageBoxText:$"是否删除选中项(共{SelectedItemsConut}项)？",button: System.Windows.MessageBoxButton.YesNo,
                icon: MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foreach (var item in ServerItemList.Where(i => i.IsSelected).ToArray())
                {
                    item.Server.DeleteFromDataBase();
                    ServerItemList.Remove(item);
                }
                XrayHanler.Instance.ReloadConfig();
            }
            
        }
        private void SetDefalutRoutingExcute()
        {
            var selectedItem = ServerItemList.Where(i => i.IsSelected).FirstOrDefault();
            if (selectedItem != null)
            {
                if(ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex == selectedItem.Server.Index)
                {
                    ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex = -1;
                }
                else
                {
                    var oldDefServer = serverItemList.FirstOrDefault(i => i.Server.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex);
                    ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex = selectedItem.Server.Index;
                    oldDefServer?.UpdateData();
                }
                ConfigObject.Instance.Save();
                selectedItem.UpdateData();
                serverItemList.FirstOrDefault(i => i.Server.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex)?.UpdateData();
                XrayHanler.Instance.ReloadConfig();
                OnPropertyChanged(nameof(DefaultServerMenuItemChecked));
            }
        }
        private void ImportServerFromClipboardCmdExcute()
        {
            var inputText = Tools.EncodeHelper.GetClipboardText();
            if (string.IsNullOrEmpty(inputText)) return;
            var inputServers = SubscriptionResolveHandle.ResolveSubFromSubContent(inputText);
            if (inputServers.Count > 0)
            {
                foreach (var item in inputServers)
                {
                    item.SaveToDataBase();
                    ServerItemList.Add(new(item));
                }
            }
        }
        private void SetActivatedServersExcute(bool isActive)
        {
            foreach (var item in ServerItemList.Where(i => i.IsSelected))
            {
                item.Server.IsActivated = isActive;
                item.Server.SaveToDataBase();
                item.UpdateData();
            }
            XrayHanler.Instance.ReloadConfig();
        }
        private async void TestNetRelayExcute()
        {                 
            if (TestDelayExcuting) return;           
            TestDelayExcuting = true;            
            TestNetRelayCmd.NotifyCanExecuteChanged();
            var selectedItems = ServerItemList.Where(i => i.IsSelected).ToList();
            var tasks = new List<Task>();
            selectedItems.ForEach(i => i.ProxyTestPort = default);
            XrayTestServerHandle.Instance.SetTestServerItems(selectedItems);
            XrayTestServerHandle.Instance.ReloadConfig();
            XrayTestServerHandle.Instance.CoreStart();
            ThreadPool.GetMinThreads(out int minThreads, out int minCompelationPortThreads);
            ThreadPool.GetMaxThreads(out int maxThreads, out int maxCompelationPOrtThreads);
            ThreadPool.SetMinThreads(selectedItems.Count + minThreads, selectedItems.Count + minCompelationPortThreads);
            ThreadPool.SetMaxThreads(selectedItems.Count + maxThreads, selectedItems.Count + maxCompelationPOrtThreads);
            selectedItems.ForEach(i => tasks.Add(Task.Run(i.StartTestNetDelay)));
            await Task.WhenAll(tasks.ToArray());
            ThreadPool.SetMinThreads(minThreads, minCompelationPortThreads);
            ThreadPool.SetMaxThreads(maxThreads, maxCompelationPOrtThreads);
            XrayTestServerHandle.Instance.CoreStop();
            TestDelayExcuting = false;
            TestNetRelayCmd.NotifyCanExecuteChanged();                
        }
        private void SubManagerExcute()
        {
            new SubcriptionManagerView().ShowDialog();
            RefreshListView();
        }

    }
}

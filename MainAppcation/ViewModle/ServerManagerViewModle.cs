using CommunityToolkit.Mvvm.Input;
using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using NetProxyController.View;
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
using NetProxyController.Handler;
using System.Windows;

namespace NetProxyController.ViewModle
{
    internal class ServerManagerViewModle: ViewModleBase
    {
        public ServerManagerViewModle()
        {
            serverItemList = new();
            serverItems = GetDateItemFromDataBase();
            serverItemList = new CollectionViewSource()
            {
                Source = serverItems
            };
            createProxyServerCmd = new(CreateProxyServerExcute);
            selectionChangedCmd = new(SelectionChangedCmdExcute);
            editServerCmd = new(EditProxyServerExcute);
            deleteServerCmd = new(DeleteProxyServerItemExcute);
            setDefalutRoutingCmd = new(SetDefalutRoutingExcute);
            importServerFromClipboardCmd = new(ImportServerFromClipboardCmdExcute);
            setActivatedServersCmd = new(SetActivatedServersExcute);
            testNetRelayCmd = new(TestNetRelayExcute);
        }
        private static List<ServerItemViewModle> GetDateItemFromDataBase()
        {
            var ret = new List<ServerItemViewModle>();
            ServerItem.ServerItemsDataList.ToList().ForEach(item => ret.Add(new(item)));
            return ret;
        }
        private List<ServerItemViewModle> serverItems;
        private CollectionViewSource serverItemList;
        private int SelectedItemsConut;
        public CollectionViewSource ServerItemList
        {
            get => serverItemList;
            set
            {
                serverItemList = value;
                OnPropertyChanged();                
            }
        }
        private RelayCommand createProxyServerCmd;
        public RelayCommand CreateProxyServerCmd
        {
            get => createProxyServerCmd;
            set => _ = value;
        }
        private RelayCommand selectionChangedCmd;
        public RelayCommand SelectionChangedCmd
        {
            get => selectionChangedCmd;
            set => _ = value;
        }
        private RelayCommand editServerCmd;
        public RelayCommand EditServerCmd
        {
            get => editServerCmd;
            set => _ = value;
        }
        private RelayCommand deleteServerCmd;
        public RelayCommand DeleteServerCmd
        {
            get => deleteServerCmd;
            set => _ = value;
        }
        private RelayCommand setDefalutRoutingCmd;
        public RelayCommand SetDefalutRoutingCmd
        {
            get => setDefalutRoutingCmd;
            set => _ = value;
        }
        private RelayCommand importServerFromClipboardCmd;
        public RelayCommand ImportServerFromClipboardCmd
        {
            get => importServerFromClipboardCmd;
            set => _ = value;
        }
        private RelayCommand<bool> setActivatedServersCmd;
        public RelayCommand<bool> SetActivatedServersCmd
        {
            get => setActivatedServersCmd;
            set => _ = value;
        }
        private RelayCommand testNetRelayCmd;
        public RelayCommand TestNetRelayCmd
        {
            get => testNetRelayCmd;
            set => _ = value;
        }
        public bool SelectedItemsIsSingle
        {
            get => serverItems.Where(item => item.IsSelected).Count() == 1;
            set => _ = value;
        }
        public bool SelectedItemsIsMultiple
        {
            get => serverItems.Where(item => item.IsSelected).Count() >= 1;
            set => _ = value;
        }
        public bool IsContainSelectedItems
        {
            get => SelectedItemsConut > 0;
            set => _ = value;
        }
        private int selectedIndex = -1;
        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                selectedIndex = value;
                OnPropertyChanged();
            }
        }
        public bool DefaultServerMenuItemChecked
        {
            get => DefaultServerMenuItemEnabled && serverItems[selectedIndex].Server.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex;
            set => _ = value;
        }
        public bool DefaultServerMenuItemEnabled
        {
            get => SelectedItemsConut == 1;
            set => _ = value;
        }
        private void SelectionChangedCmdExcute()
        {
            SelectedItemsConut = serverItems.Where(item => item.IsSelected).Count();
            OnPropertyChanged(nameof(SelectedItemsIsSingle));
            OnPropertyChanged(nameof(SelectedItemsIsMultiple));
            OnPropertyChanged(nameof(IsContainSelectedItems));
            OnPropertyChanged(nameof(DefaultServerMenuItemChecked));
            OnPropertyChanged(nameof(DefaultServerMenuItemEnabled));
        }
        private void CreateProxyServerExcute()
        {
            var NewServer = new ServerItemViewModle();
            if(new ServerSettingWindow(NewServer.Server).ShowDialog() == true)
            {
                NewServer.UpdateData();
                serverItems.Add(NewServer);
                ServerItemList.View.Refresh();
            }
        }
        private void EditProxyServerExcute()
        {
            foreach(var item in serverItems)
            {
                if(item.IsSelected)
                {
                    item.EditServerItem();
                    serverItems.ForEach(i => i.IsSelected = false);
                    SelectedIndex = -1;
                    SelectionChangedCmdExcute();
                    return;
                }
            }
        }
        private void DeleteProxyServerItemExcute()
        {
            if(HandyControl.Controls.MessageBox.Show(messageBoxText:$"是否删除选中项(共{serverItems.Where(item => item.IsSelected).Count()}项)？",button: System.Windows.MessageBoxButton.YesNo,
                icon: System.Windows.MessageBoxImage.Question) == System.Windows.MessageBoxResult.Yes)
            {
                for (int i = 0; i < serverItems.Count; i++)
                {
                    if (serverItems[i].IsSelected)
                    {
                        serverItems[i].Server.DeleteFromDataBase();
                        serverItems.RemoveAt(i);
                        i--;
                    }
                }
            }
            else
            {
                serverItems.ForEach(i => i.IsSelected = false);
                SelectedIndex = -1;
                return;
            }
            serverItemList.View.Refresh();
            SelectionChangedCmdExcute();
            XrayHanler.Instance.ReloadConfig();
        }
        private void SetDefalutRoutingExcute()
        {
            var selectedItem = serverItems.Where(i => i.IsSelected).FirstOrDefault();
            if (selectedItem != null)
            {
                ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex = DefaultServerMenuItemChecked ? -1 : selectedItem.Server.Index;
                ConfigObject.Instance.Save();
                serverItemList.View.Refresh();
                XrayHanler.Instance.ReloadConfig();
            }
        }
        private void ImportServerFromClipboardCmdExcute()
        {
            var inputText = Tools.EncodeHelper.GetClipboardText();
            if (string.IsNullOrEmpty(inputText)) return;
            var inputServers = SubscribeHandle.ResolveSubFromSubContent(inputText);
            if (inputServers.Count > 0)
            {
                foreach (var item in inputServers)
                {
                    item.SaveToDataBase();
                    serverItems.Add(new(item));
                }
                ServerItemList.View.Refresh();
            }
        }
        private void SetActivatedServersExcute(bool isActive)
        {
            foreach (var item in serverItems.Where(i => i.IsSelected))
            {
                item.Server.IsActivated = isActive;
                item.Server.SaveToDataBase();
                item.UpdateData();
            }
            XrayHanler.Instance.ReloadConfig();
        }
        private void TestNetRelayExcute()
        {
            var selectedItems = serverItems.Where(i => i.IsSelected).ToList();
            var tasks = new List<Task>();
            if (selectedItems.Count <= 0) return;
            selectedItems.ForEach(i => { i.NetDelay = -2; i.ProxyTestPort = default; });
            XrayTestServerHandle.Instance.SetTestServerItems(selectedItems);
            XrayTestServerHandle.Instance.ReloadConfig();
            XrayTestServerHandle.Instance.CoreStart();
            Task.Run(() =>
            {
                foreach(var item in selectedItems)
                {
                    tasks.Add(Task.Run(item.StartTestNetDelay));
                }
                Task.WaitAll(tasks.ToArray());
                XrayTestServerHandle.Instance.CoreStop();
            });
        }

    }
}

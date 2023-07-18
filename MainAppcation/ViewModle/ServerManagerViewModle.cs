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
            if (serverItems.Count > 0 && serverItems.FirstOrDefault(i => i.Server.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex) is null)
            {
                ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex = serverItems[0].Server.Index;
            }
        }
        private static List<ServerItemViewModle> GetDateItemFromDataBase()
        {
            var ret = new List<ServerItemViewModle>();
            Global.DBService.Table<ServerItem>().ToList().ForEach(item => ret.Add(new(item)));
            return ret;
        }
        private List<ServerItemViewModle> serverItems;
        private CollectionViewSource serverItemList;
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
        private void SelectionChangedCmdExcute()
        {
            OnPropertyChanged(nameof(SelectedItemsIsSingle));
            OnPropertyChanged(nameof(SelectedItemsIsMultiple));
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
            if(MessageBox.Show(messageBoxText:$"是否删除选中项(共{serverItems.Where(item => item.IsSelected).Count()}项)？",button: System.Windows.MessageBoxButton.YesNo,
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
            XrayHanler.Instance.ReLoad();
        }
        private void SetDefalutRoutingExcute()
        {
            var selectedItem = serverItems.Where(i => i.IsSelected).FirstOrDefault();
            if (selectedItem != null)
            {
                ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex = selectedItem.Server.Index;
                ConfigObject.Instance.Save();
                serverItems.ForEach(i => i.IsSelected = false);
                SelectedIndex = -1;
                serverItemList.View.Refresh();
                SelectionChangedCmdExcute();
                XrayHanler.Instance.ReLoad();
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

    }
}

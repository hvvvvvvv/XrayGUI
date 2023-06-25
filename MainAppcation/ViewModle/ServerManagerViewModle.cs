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
            selectionChangedCmd = new(SelectionChangedCmdExcute!);
        }
        private static List<ServerItemViewModle> GetDateItemFromDataBase()
        {
            var ret = new List<ServerItemViewModle>();
            Global.DBService.Table<ServerItem>().ToList().ForEach(item => ret.Add(new(item)));
            return ret;
        }
        private List<ServerItemViewModle> serverItems;
        private CollectionViewSource serverItemList;
        private IList? selectionServerItems;
        public CollectionViewSource ServerItemList
        {
            get => serverItemList;
            set
            {
                serverItemList = value;
                OnPropertyChanged();                
            }
        }
        private ServerItemViewModle? selectServerItems;
        public ServerItemViewModle? SelectServerItems
        {
            get => selectServerItems;
            set
            {
                selectServerItems = value;
                OnPropertyChanged();
            }
        }
        private RelayCommand createProxyServerCmd;
        public RelayCommand CreateProxyServerCmd
        {
            get => createProxyServerCmd;
            set => _ = value;
        }
        private RelayCommand<object> selectionChangedCmd;
        public RelayCommand<object> SelectionChangedCmd
        {
            get => selectionChangedCmd;
            set => _ = value;
        }
        private void SelectionChangedCmdExcute(object view)
        {
            if (view is ListView listView)
            {
                selectionServerItems ??= listView.SelectedItems;
                Debug.WriteLine(selectionServerItems.GetType());
            }
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

    }
}

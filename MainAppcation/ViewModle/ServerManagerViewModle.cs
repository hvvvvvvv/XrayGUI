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
                    SelectionChangedCmdExcute();
                    return;
                }
            }
        }
        private void DeleteProxyServerItemExcute()
        {
            if(MessageBox.Show(messageBoxText:"是否删除选中项？",button: System.Windows.MessageBoxButton.YesNo,
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
            }
            serverItemList.View.Refresh();
            SelectionChangedCmdExcute();
        }

    }
}

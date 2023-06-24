using CommunityToolkit.Mvvm.Input;
using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using NetProxyController.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NetProxyController.ViewModle
{
    internal class ServerManagerViewModle: ViewModleBase
    {
        public ServerManagerViewModle()
        {
            serverItemList = new();
            Global.DBService.Table<ServerItem>().ToList().ForEach(item => serverItemList.Add(new(item)));
            editServerItemCmd = new RelayCommand(EditServerItemExcute);
            itemDoubleClick = ItemDoubleClickExcute;
        }
        private List<ServerItemViewModle> serverItemList;
        public List<ServerItemViewModle> ServerItemList
        {
            get => serverItemList;
            set
            {
                serverItemList = value;
                OnPropertyChanged();
            }
        }
        private ServerItemViewModle? selectServerItem;
        public ServerItemViewModle? SelectServerItem
        {
            get => selectServerItem;
            set
            {
                selectServerItem = value;
                OnPropertyChanged();
            }
        }
        private RelayCommand editServerItemCmd;
        public RelayCommand EditServerItemCmd
        { 
            get => editServerItemCmd;
            set => _ = value;
        }
        private MouseButtonEventHandler itemDoubleClick;
        public MouseButtonEventHandler ItemDoubleClick
        {
            get => itemDoubleClick;
            set => _ = value;
        }
        private void EditServerItemExcute()
        {
            new ServerSettingWindow(SelectServerItem?.Server ?? new()).ShowDialog();
        }
        private void ItemDoubleClickExcute(object sender, MouseButtonEventArgs e)
        {
            new ServerSettingWindow(SelectServerItem?.Server ?? new()).ShowDialog();
        }
    }
}

using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class ServerManagerViewModle: ViewModleBase
    {
        public ServerManagerViewModle()
        {
            serverItemList = new();
            Global.DBService.Table<ServerItem>().ToList().ForEach(item => serverItemList.Add(new(item)));
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
    }
}

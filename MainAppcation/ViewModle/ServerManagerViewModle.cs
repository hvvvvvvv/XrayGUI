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
        private List<ServerItem> _serverItem;
        public List<ServerItem> ServerItem
        {
            get => _serverItem;
            set
            {
                _serverItem = value;
                OnPropertyChanged();
            }
        }
    }
}

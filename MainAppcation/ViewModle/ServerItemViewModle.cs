using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class ServerItemViewModle : ViewModleBase
    {
        public ServerItem Server;
        public ServerItemViewModle(ServerItem server)
        {
            Server = server;
            ServerName = server.Remarks;
        }
        private void SetProperty<T>(ref T property, T value, [CallerMemberName]string? propertyName = null)
        {
            property = value;
            OnPropertyChanged(propertyName);
        }
        private string serverName;
        public string ServerName
        {
            get => serverName;
            set => SetProperty(ref serverName, value);
        }

    }
}

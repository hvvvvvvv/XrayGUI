using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Tools
{
    public static class NetworkHelper
    {
        public static bool IsListennerAvailable(int port, string addr, ProtocolType protocolType)
        {
            using var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, protocolType);
            try
            {
                var ipaddr = IPAddress.Parse(addr);
                socket.Bind(new IPEndPoint(ipaddr, port));
                return true;
            }
            catch (SocketException)
            {
                return false;
            }
        }
    }
}

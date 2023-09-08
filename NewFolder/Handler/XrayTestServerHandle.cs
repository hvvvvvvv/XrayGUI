using XrayGUI.Modle;
using XrayGUI.ViewModle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle.Routing;
using XrayCoreConfigModle;

namespace XrayGUI.Handler
{
    internal class XrayTestServerHandle : XrayHanler
    {
        private static XrayTestServerHandle? _instance;
        public static new XrayTestServerHandle Instance => _instance ??= new();
        private List<ServerItemViewModle>? testServerItems;
        public XrayTestServerHandle() : base()
        {
            coreConfigPath = Global.XrayCoreTestConfigPath;
        }
        public void SetTestServerItems(IEnumerable<ServerItemViewModle> items) => testServerItems = new(items);
        protected override void LoadConfig()
        {
            var _outbounds = (from ServerItemViewModle item in testServerItems ??= new() select item.Server.ToOutboundServerItemObject()).ToList();
            _outbounds.Insert(0, new OutboundServerItemObject()
            {
                protocol = "blackhole"
            });
            var _inbounds = new List<InboundServerItemObject>();
            var routeRlues = new List<RuleObject>();
            var sockets = new List<Socket>();
            foreach (var item in testServerItems)
            {
                Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
                int port = ((IPEndPoint)socket.LocalEndPoint!).Port;
                item.ProxyTestPort = port;
                sockets.Add(socket);
                _inbounds.Add(new InboundServerItemObject()
                {
                    protocol = "http",
                    listen = Global.LoopBcakAddress,
                    port = port,
                    settings = new XrayCoreConfigModle.Inbound.HttpConfigurationObject()
                    {
                        allowTransparent = true
                    },
                    tag = item.Server.Index.ToString()
                });
                routeRlues.Add(new RuleObject()
                {
                    inboundTag = new()
                    {
                        item.Server.Index.ToString()
                    },
                    outboundTag = item.Server.Index.ToString()
                });
            }
            if (Isrunning) CoreStop();
            JsonHandler.JsonSerializeToFile(new MainConfiguration()
            {
                inbounds = _inbounds,
                outbounds = _outbounds,
                routing = new RoutingObject()
                {
                    rules = routeRlues
                }
            }, coreConfigPath
            );
            sockets.ForEach(i => i.Dispose());
        }
    }
}

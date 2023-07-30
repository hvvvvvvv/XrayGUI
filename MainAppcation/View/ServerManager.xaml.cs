using NetProxyController.ViewModle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace NetProxyController.View
{
    /// <summary>
    /// ServerManager.xaml 的交互逻辑
    /// </summary>
    internal partial class ServerManager : Window
    {
        public bool IsClosed = false;
        public ServerManager()
        {
            InitializeComponent();
            Closing += (_, _) => IsClosed = true;
            foreach(var i in listView.Items)
            {
                
            }
        }
        private static ServerManager _instance = new ServerManager();
        public static ServerManager Instance
        {
            get
            {
                if(_instance.IsClosed)
                {
                    _instance = new ServerManager();
                }
                return _instance;
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach(var i in e.AddedItems)
            {
                if(i is ServerItemViewModle vm)
                {
                   vm.IsSelected = true;
                }
            }
            foreach(var i in e.RemovedItems)
            {
                if (i is ServerItemViewModle vm)
                {
                    vm.IsSelected = false;
                }
            }
        }
    }
}

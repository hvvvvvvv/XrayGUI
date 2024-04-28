using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using XrayGUI.ViewModle;

namespace XrayGUI.View
{
    /// <summary>
    /// ProxyServerView.xaml 的交互逻辑
    /// </summary>
    public partial class ProxyServerView : UserControl
    {

        public ProxyServerView()
        {
            InitializeComponent();
        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(DataContext is ProxyServerMenuViewModle viewModle)
            {
                foreach (ProxyServerListItemViewModle item in e.RemovedItems)
                {
                    viewModle.SelectedProxyServers.Remove(item);
                }
                foreach (ProxyServerListItemViewModle item in e.AddedItems)
                {
                    if(viewModle.ProxyServerList.Contains(item) && !viewModle.SelectedProxyServers.Contains(item))
                    {
                        viewModle.SelectedProxyServers.Add(item);
                    }
                }
            }
        }
    }
}

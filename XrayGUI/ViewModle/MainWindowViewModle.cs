using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MaterialDesignThemes.Wpf;
using System.Threading.Tasks;

namespace XrayGUI.ViewModle
{
    internal class MainWindowViewModle : ViewModleBase   
    {
        public MainWindowViewModle()
        {
            MenuItems = new()
            {
                new MenuViewModle("代理节点", PackIconKind.ResistorNodes){ Content = new View.ProxyServerView()},
                new MenuViewModle("订阅分组", PackIconKind.Group),
                new MenuViewModle("路由规则", PackIconKind.Router),
                new MenuViewModle("入站设置", PackIconKind.Tune),
                new MenuViewModle("DNS服务", PackIconKind.Dns),
                new MenuViewModle("参数设置", PackIconKind.Settings),
                new MenuViewModle("日志信息", PackIconKind.TextBoxOutline)                               
            };
            selectedMenuItem = MenuItems[0];
        }
        public ObservableCollection<MenuViewModle> MenuItems { get; private set; }
        private MenuViewModle selectedMenuItem;
        public MenuViewModle SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProerty(ref selectedMenuItem, value);
        }
    }
}

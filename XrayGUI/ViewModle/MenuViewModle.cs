using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialDesignThemes.Wpf;

namespace XrayGUI.ViewModle
{
    public class MenuViewModle : ViewModleBase
    {
        public MenuViewModle(string name, PackIconKind selectedIcon, PackIconKind unSelectedIcon)
        {
            MenuName = name;
            SelectedIcon = selectedIcon;
            UnSelectedIcon = unSelectedIcon;
        }
        public MenuViewModle(string name, PackIconKind icon)
        {
            MenuName = name;
            SelectedIcon = icon;
            UnSelectedIcon = icon;
        }
        public PackIconKind SelectedIcon { get; private set; }
        public PackIconKind UnSelectedIcon { get; private set; }
        public string MenuName { get; private set; }
        public object? Content { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls;
using WindowsProxy;
using System.Windows.Controls.Ribbon;
using ProxyNotifyWindow;

namespace NetProxyController
{
    public partial class  TaskBarIconByPorxyControl: TaskbarIcon
    {
        private AppConfigration Configration;
        private NotifyWindow NotifyWindow_;
        private GlobalHotkey.GlobalHotkeyRegister hotkeyRegister = new();
        public TaskBarIconByPorxyControl()
        {
            Configration = new(AppContext.BaseDirectory + "AppSetting.json");
            DataContext = Configration;                     
            NotifyWindow_ = new(Configration.IsProxyEnable ? NotifyWindow.StatusEnableImage : NotifyWindow.StatusDisableImage);
            hotkeyRegister.Add(Configration.ProxyHotkey,OnHotkeyHandle);
            UpdateIcon();
            InitializeComponent();            
        }

        private void OnQuitClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
        }
        private void OnIsEnableProxyClick(object sender, RoutedEventArgs e)
        {
            UpdateIcon();
        }
        private void OnHotkeyHandle()
        {
            if(!Configration.IsHotkeyPause)
            {
                Configration.IsProxyEnable = !Configration.IsProxyEnable;
                NotifyWindow_.ShowNotify(Configration.IsProxyEnable ? NotifyWindow.StatusEnableImage : NotifyWindow.StatusDisableImage);
                UpdateIcon();
            }
        }
        public void Show() => Visibility = Visibility.Visible;
        public void UpdateIcon()
        {
            ToolTipText = Configration.IsProxyEnable ? "代理已开启" : "代理已关闭";
            Icon = Configration.IsProxyEnable ? AppConfigration.IconByProxyEnabled : AppConfigration.IconByProxyDisabled;
        }

        private void OnAppSetingClick(object sender, RoutedEventArgs e)
        {
            new SettingWindow(Configration).Show();
        }
    }
}

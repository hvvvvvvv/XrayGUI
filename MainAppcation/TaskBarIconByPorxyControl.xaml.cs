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
using System.Security.Cryptography;

namespace NetProxyController
{
    public partial class  TaskBarIconByPorxyControl: TaskbarIcon
    {
        private AppConfigration Configration;
        private NotifyWindow NotifyWindow_;
        private SettingWindow _SettingWindow = default!;
        public TaskBarIconByPorxyControl()
        {
            Configration = new(AppContext.BaseDirectory + "AppSetting.json");
            Configration.HotkeyHappendEvent += OnHotkeyHandle;            
            DataContext = Configration;                     
            NotifyWindow_ = new(Configration.IsProxyEnable ? NotifyWindow.StatusEnableImage : NotifyWindow.StatusDisableImage);
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
            Configration.IsProxyEnable = !Configration.IsProxyEnable;
            NotifyWindow_.ShowNotify(Configration.IsProxyEnable ? NotifyWindow.StatusEnableImage : NotifyWindow.StatusDisableImage);
            UpdateIcon();
        }
        public void Show() => Visibility = Visibility.Visible;
        public void UpdateIcon()
        {
            ToolTipText = Configration.IsProxyEnable ? "代理已开启" : "代理已关闭";
            Icon = Configration.IsProxyEnable ? AppConfigration.IconByProxyEnabled : AppConfigration.IconByProxyDisabled;
        }

        private void OnAppSetingClick(object sender, RoutedEventArgs e)
        {
            ShowSettingWindow();
        }
        private void ShowSettingWindow()
        {
            if (_SettingWindow != null && !_SettingWindow._SettingComleted)
            {
                _SettingWindow.WindowState = WindowState.Normal;
                _SettingWindow.Activate();
            }
            else
            {
                _SettingWindow = new(Configration);
                _SettingWindow.Show();
            }
        }

        private void TaskbarIcon_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ShowSettingWindow();
        }
    }
}

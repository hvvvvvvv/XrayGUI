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
using System.Drawing;
using System.Reflection.PortableExecutable;

namespace NetProxyController
{
    public partial class  TaskBarIconByPorxyControl: TaskbarIcon
    {
        public static Icon IconByProxyEnabled = Icon.FromHandle(Resource.ProxyEnable.GetHicon());
        public static Icon IconByProxyDisabled = Icon.FromHandle(Resource.ProxyDisable.GetHicon());
        private AppConfigration Configration;
        private NotifyWindow NotifyWindow_;
        private SettingWindow _SettingWindow;
        public TaskBarIconByPorxyControl()
        {
            Configration = new();                      
            _SettingWindow = new(Configration);
            NotifyWindow_ = new(Configration.ProxyEnable ? NotifyWindow.StatusEnableImage : NotifyWindow.StatusDisableImage);
            InitializeComponent();
            init();
        }
        public void Show() => Visibility = Visibility.Visible;
        private void init()
        {
            Configration.hotkeyHandler.HotkeyHappenedCallback += OnHotkeyHandle;
            Quit.Click += (s,e) => Application.Current.Shutdown(0);
            AppSetting.Click += (s, e) => ShowSettingWindow();
            TrayLeftMouseUp += (s, e) => ShowSettingWindow();
            IsProxyEnable.Click += OnIsEnableProxyClick;            
            AutoStart.Click += AutoStart_Click;           
            UpdateView();
        }
        private void UpdateView()
        {
            AutoStart.IsChecked = Configration.EnableAutostart;
            IsProxyEnable.IsChecked = Configration.ProxyEnable;
            ToolTipText = Configration.ProxyEnable ? "代理已开启" : "代理已关闭";
            Icon = Configration.ProxyEnable ? IconByProxyEnabled : IconByProxyDisabled;            
        }
        private void OnIsEnableProxyClick(object sender, RoutedEventArgs e)
        {
            Configration.ProxyEnable = IsProxyEnable.IsChecked;
            Configration.UpdateSetting();
            UpdateView();
        }
        private void OnHotkeyHandle()
        {
            Configration.ProxyEnable = !Configration.ProxyEnable;
            Configration.UpdateSetting();
            NotifyWindow_.ShowNotify(Configration.ProxyEnable ? NotifyWindow.StatusEnableImage : NotifyWindow.StatusDisableImage);            
            UpdateView();
        }
        private void ShowSettingWindow()
        {
            _SettingWindow.Show();
        }
        private void AutoStart_Click(object sender, RoutedEventArgs e)
        {
            Configration.EnableAutostart = AutoStart.IsChecked;
            Configration.UpdateSetting();
            UpdateView();
        }
    }
}

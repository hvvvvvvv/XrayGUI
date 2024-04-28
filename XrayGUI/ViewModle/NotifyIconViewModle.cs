using System;
using System.Drawing;
using XrayGUI.Modle;
using System.Linq;
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using System.Windows.Media;
using XrayGUI.Handler;
using XrayGUI.View;
using HandyControl.Controls;
using XrayGUI.Modle.Server;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace XrayGUI.ViewModle
{
    internal class NotifyIconViewModle : ViewModleBase
    {
        private NotifyWindow _notifyWindowView;
        public bool ProxyEnableChecked
        {
            get => ConfigObject.Instance.ProxyEnable;
            set
            {
                ConfigObject.Instance.ProxyEnable = value;
                SystemProxyHanler.Instance.LoadConfig();                
                ConfigObject.Instance.Save();
                OnPropertyChanged(nameof(ProxyEnableChecked));
            }
        }
        public bool AutoStartChecked
        {
            get => ConfigObject.Instance.EnableAutostart;
            set
            {              
                ConfigObject.Instance.EnableAutostart = value;
                AutoStartHandler.Instance.LoadConfig();
                ConfigObject.Instance.Save();
                OnPropertyChanged(nameof(AutoStartChecked));
            }                
        }
        public RelayCommand QuitCmd { get; set; }
        public RelayCommand ShowSettingWndCmd { get; set; }
        public RelayCommand ShowServerManagerCmd { get; set; }
        public RelayCommand ShowRouteRulesManagerCmd { get; set; }
        public RelayCommand SetAutoStartCmd { get; set; }
        public RelayCommand ProxySwitchCmd { get; set; }
        public RelayCommand MainWindowShowCmd { get; set; }

        public NotifyIconViewModle()
        {
            _notifyWindowView = new();
            HotkeyHandler.Instance.HotkeyHappenedCallback += OnHotkeyEvenRaise;
            QuitCmd = new (() => Application.Current.Shutdown(0));
            ShowSettingWndCmd = new(() => SettingWindow.Instance.Show());
            ShowServerManagerCmd = new(() => ServerManager.Instance.Show());
            ShowRouteRulesManagerCmd = new(() => RouteRulesManager.Instance.Show());
            ProxySwitchCmd = new(() => ProxyEnableChecked = !ProxyEnableChecked);
            SetAutoStartCmd = new(() => AutoStartChecked = !AutoStartChecked);
            MainWindowShowCmd = new(() => new MainWindow().Show());
            XrayHanler.Instance.CoreStart();
            SystemProxyHanler.Instance.LoadConfig();
            SubcriptionUpdateHandle.Instance.UpdateEvent += (e) =>
            {
                if(e.IsCompeleteUpdate)
                {
                    XrayHanler.Instance.ReloadConfig();
                    Growl.InfoGlobal("订阅更新完成");
                }
                else
                {
                    Growl.WarningGlobal($"订阅更新失败\n{e.ErrMsg}");
                }
                
            };
        }
        private void OnHotkeyEvenRaise()
        {
            ProxyEnableChecked = !ProxyEnableChecked;
            _notifyWindowView.Show();
        }

    }
}

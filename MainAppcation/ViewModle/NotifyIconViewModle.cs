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
using XrayGUI.Modle.Server;
using System.Windows.Threading;

namespace XrayGUI.ViewModle
{
    internal class NotifyIconViewModle : ViewModleBase
    {
        private View.NotifyWindow _notifyWindowView;
        public bool ProxyEnableChecked
        {
            get => ConfigObject.Instance.ProxyEnable;
            set
            {
                ConfigObject.Instance.ProxyEnable = value;
                SystemProxyHanler.Instance.LoadConfig();
                OnPropertyChanged(nameof(ProxyEnableChecked));
                OnPropertyChanged(nameof(BarIconPath));
                OnPropertyChanged(nameof(ToolTipText));
                ConfigObject.Instance.Save();
            }
        }     
        public string BarIconPath => ConfigObject.Instance.ProxyEnable ? "/Icon/ProxyEnable.ico" : "/Icon/ProxyDisable.ico";
        public string ToolTipText => ConfigObject.Instance.ProxyEnable ? "代理已开启" : "代理已关闭";
        public bool AutoStartChecked
        {
            get => ConfigObject.Instance.EnableAutostart;
            set
            {
                ConfigObject.Instance.EnableAutostart = value;
                AutoStartHandler.Instance.LoadConfig();
                ConfigObject.Instance.Save();
            }                
        }
        public RelayCommand QuitCmd { get; set; }
        public RelayCommand ShowSettingWndCmd { get; set; }
        public RelayCommand ShowServerManagerCmd { get; set; }

        public NotifyIconViewModle()
        {
            _notifyWindowView = new();
            HotkeyHandler.Instance.HotkeyHappenedCallback += OnHotkeyEvenRaise;
            QuitCmd = new (() => Application.Current.Shutdown(0));
            ShowSettingWndCmd = new(() => SettingWindow.Instance.Show());
            ShowServerManagerCmd = new(() => ServerManager.Instance.Show());
            XrayHanler.Instance.CoreStart();
            SystemProxyHanler.Instance.LoadConfig();
            SubcriptionUpdateHandle.Instance.UpdateEvent += (e) =>
            {
                if(e.IsCompeleteUpdate)
                {
                    if(ServerItem.ServerItemsDataList.Any(i => i.SubGroupId == e.Subscription.SubcriptionId && (i.IsActivated || i.Index == ConfigObject.Instance.XrayCoreSetting.DefaultOutboundServerIndex)))
                    {
                        XrayHanler.Instance.ReloadConfig();
                    }
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

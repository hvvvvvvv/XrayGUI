using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using NetProxyController.Modle;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data.Common;
using CommunityToolkit;
using CommunityToolkit.Mvvm.Input;
using System.Windows;
using ProxyNotifyWindow;
using System.Configuration;
using System.Windows.Media;
using static NetProxyController.Tools.ImageHelper;
using System.Windows.Media.Imaging;

namespace NetProxyController.ViewModle
{
    internal class NotifyIconViewModle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private MainConfigration _configration;
        private SettingWindow _settingWindow;
        private NotifyWindow _notifyWindow;
        private ImageSource _notifyWndImage => _configration.ProxyEnable ? NotifyWindow.StatusEnableImage : NotifyWindow.StatusDisableImage;      
   
        public bool ProxyEnableChecked
        {
            get => _configration.ProxyEnable;
            set
            {
                _configration.ProxyEnable = value;
                OnProxyEnableChanged(value);
            }
        }
        
        public string BarIconPath
        {
            get => _configration.ProxyEnable ? "/Icon/ProxyEnable.ico" : "/Icon/ProxyDisable.ico";
        }
        public string ToolTipText
        {
            get => _configration.ProxyEnable ? "代理已开启" : "代理已关闭";
        }
        public bool AutoStartChecked
        {
            get => _configration.autoStartHandler.Enable;
            set => _configration.autoStartHandler.Enable = value;
        }
        public RelayCommand QuitCmd { get; set; }
        public RelayCommand ShowSettingWndCmd { get; set; }

        public NotifyIconViewModle(MainConfigration configration)
        {
            _configration = configration;
            _configration.hotkeyHandler.HotkeyHappenedCallback += OnHotkeyEvenRaise;
            _settingWindow = new(_configration);
            _notifyWindow = new(_notifyWndImage);
            QuitCmd = new RelayCommand(() => Application.Current.Shutdown(0));
            ShowSettingWndCmd = new(ShowSettingWindow);
        }
        private void RiaseChangedEvent(string proertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(proertyName));
        }
        private void OnProxyEnableChanged(bool IsEnable)
        {
            if (IsEnable)
            {
                _configration.systemProyHanler.OnProxy();
            }
            else
            {
                _configration.systemProyHanler.OffProxy();
            }
            RiaseChangedEvent(nameof(BarIconPath));
            RiaseChangedEvent(nameof(ProxyEnableChecked));
            RiaseChangedEvent(nameof(ToolTipText));
            _configration.Save();
        }
        private void ShowSettingWindow()
        {
            _settingWindow.Show();
        }
        private void OnHotkeyEvenRaise()
        {
            ProxyEnableChecked = !ProxyEnableChecked;
            _notifyWindow.ShowNotify(_notifyWndImage);
        }

    }
}

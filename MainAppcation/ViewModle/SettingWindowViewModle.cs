using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using CommunityToolkit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GlobalHotkey;
using CommunityToolkit.Mvvm.Input;
using NetProxyController.Modle;
using HandyControl.Controls;
using NetProxyController.Handler;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;

namespace NetProxyController.ViewModle
{
    internal class SettingWindowViewModle : ViewModleBase
    {
        private bool saveBtnEnable = true;
        public bool SaveBtnEnable 
        {
            get => saveBtnEnable;
            set
            {
                saveBtnEnable = value;
                OnPropertyChanged();
            }
        }
        private int httpPort;
        private string _httpHost;
        [Required(ErrorMessage = "Http端口号不能为空")]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$", ErrorMessage = "请输入正确的端口号(1-65535)")]
        public string HttpPort
        {
            get => _httpHost;
            set
            {
                _httpHost = value;
                if(ValidationProperty())
                {
                    httpPort = Convert.ToInt32(value);
                }
            }
        }
        private int socksPort;
        private string _socksHost;
        [Required(ErrorMessage = "Socks端口号不能为空")]
        [RegularExpression(@"^(?:[1-9]\d{0,3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$", ErrorMessage = "请输入正确的端口号(1-65535)")]
        public string SocksPort
        {
            get => _socksHost;
            set
            {
                _socksHost = value;

                if(ValidationProperty())
                {
                    socksPort = Convert.ToInt32(value);
                }
            }
        }

        private SystemProtocol proxyProtocol;
        public bool HttpProxyChecked
        {
            get => proxyProtocol == SystemProtocol.Http;
            set
            {
                if(value == true) 
                    proxyProtocol = SystemProtocol.Http;
            }
        }
        public bool SocksProxyChecked
        {
            get => proxyProtocol == SystemProtocol.Socks;
            set
            {
                if (value == true)
                    proxyProtocol = SystemProtocol.Socks;
            }
        }
        private bool hotKeyEnabled;
        public bool HotKeyEnableChecked
        {
            get => hotKeyEnabled;
            set
            {
                hotKeyEnabled = value;
                OnPropertyChanged();
            }
        }
        public bool HotKeyDisableChecked
        {
            get => !hotKeyEnabled;
            set => hotKeyEnabled = !value;
        }
        private string sysProxyByPass;
        public string SysProxyByPass
        {
            get => sysProxyByPass;
            set => sysProxyByPass = value;
        }

        private Hotkey hotKey;
        public Hotkey Hotkey
        {
            get => hotKey;
            set
            {
                hotKey = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand<System.Windows.Window> SaveBtnCmd { get; set; }
        public RelayCommand<KeyEventArgs> NumberInputPreviewKeyDownCmd { get; set; }
        public RelayCommand<KeyEventArgs> HotkeyInputPreviewKeyDownCmd { get; set; }
        public SettingWindowViewModle()
        {
            proxyProtocol = ConfigObject.Instance.SystemProxySetting.UseProtocol;
            hotKeyEnabled = ConfigObject.Instance.HotkeySetting.Enable;
            sysProxyByPass = ConfigObject.Instance.SystemProxySetting.ByPassUrl;
            _httpHost = ConfigObject.Instance.localPort.Http.ToString();
            httpPort = ConfigObject.Instance.localPort.Http;
            _socksHost = ConfigObject.Instance.localPort.Scoks.ToString();
            socksPort = ConfigObject.Instance.localPort.Scoks;
            hotKey = ConfigObject.Instance.HotkeySetting.Hotkey;
            ErrorsChanged += (s, e) => SaveBtnEnable = !HasErrors;
            SaveBtnCmd = new(SaveBtnExcute!, (_) => !HasErrors);
            NumberInputPreviewKeyDownCmd = new(NumberInputPreviewKeyDownExcute!);
            HotkeyInputPreviewKeyDownCmd = new(HotkeyInputPreviewKeyDownExcute!);
        }

        private void SaveBtnExcute(System.Windows.Window win)
        {
            if (!ValidationAllProperty()) return;
            ConfigObject.Instance.localPort.Scoks = socksPort;
            ConfigObject.Instance.localPort.Http = httpPort;
            ConfigObject.Instance.SystemProxySetting.UseProtocol = proxyProtocol;
            ConfigObject.Instance.SystemProxySetting.ByPassUrl = sysProxyByPass;
            ConfigObject.Instance.HotkeySetting.Hotkey = Hotkey;
            HotkeyHandler.Instance.LoadConfig();
            XrayHanler.Instance.ReloadConfig();
            SystemProxyHanler.Instance.LoadConfig();
            ConfigObject.Instance.Save();
            win.Close();
        }
        private void NumberInputPreviewKeyDownExcute(KeyEventArgs e)
        {
            if (Keyboard.Modifiers == ModifierKeys.None && bumberKeys.Contains(e.Key))
            {
                return;
            }
            e.Handled = true;
        }
        private void HotkeyInputPreviewKeyDownExcute(KeyEventArgs e)
        {
            e.Handled = true;
            if (!assistKeys.Contains(e.Key) && !assistKeys.Contains(e.SystemKey))
            {
                Key _key;
                if (e.Key == Key.System)
                {
                    _key = e.SystemKey;
                }
                else
                {
                    _key = e.Key;
                }
                Hotkey = new Hotkey((KeyModifier)Keyboard.Modifiers, _key);
            }
        }
        private static readonly List<Key> bumberKeys = new()
        {
            Key.D0, Key.D1, Key.D2, Key.D3, Key.D4,
            Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
            Key.NumPad0, Key.NumPad1, Key.NumPad2,
            Key.NumPad3, Key.NumPad4, Key.NumPad5,
            Key.NumPad6, Key.NumPad7, Key.NumPad8,
            Key.NumPad9,Key.Back,Key.Tab
        };
        private static readonly List<Key> assistKeys = new List<Key>()
         {
              Key.LeftCtrl, Key.RightCtrl, Key.LeftAlt, Key.RightAlt,Key.LeftShift,Key.RightShift
         };
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GlobalHotkey;
using NetProxyController.Tools;
using HandyControl.Themes;
using NetProxyController.Modle;
using Windows.Services.Store;
using XrayCoreConfigModle;

namespace NetProxyController
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        private MainConfigration _appConfig;
        private bool _IsShowing = false;
        internal SettingWindow(MainConfigration appConfig)
        {
            InitializeComponent();           
            Icon = ImageHelper.IconToImageSource(Resource.Setting);
            _appConfig = appConfig;            
        }

        public new void Show()
        {
            if(!_IsShowing)
            {
                viewData.HttpPort = _appConfig.LocalPort.Http;
                viewData.SocksPort = _appConfig.LocalPort.Scoks;
                viewData.SysProxyBypass = _appConfig.SystemProxySetting.ByPassUrl;
                viewData.RadioHotkeyOn = _appConfig.HotkeySetting.Enable;
                viewData.RadioHotkeyOff = !_appConfig.HotkeySetting.Enable;
                viewData.RadioHttpChecked = _appConfig.SystemProxySetting.UseProtocol == SystemProtocol.Http;
                viewData.RadioSocksChecked = _appConfig.SystemProxySetting.UseProtocol == SystemProtocol.Socks;
                viewData.GlobalHotkey = _appConfig.HotkeySetting.Hotkey;
                _appConfig.hotkeyHandler.IsPause = true;
                _IsShowing = true;
                base.Show();
            }

            WindowState = WindowState.Normal;
            Activate();

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(HttpPortByBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
            {
                TextBoxHttpPort.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            if (SocksPortBybinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
            {
                TextBoxSokcsPort.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            if (Validation.GetErrors(TextBoxHttpPort).Count > 0)
            {
                string valueText = TextBoxHttpPort.Text;
                var binding = new Binding()
                {
                    Path = HttpPortByBinding.Path,
                    Mode = HttpPortByBinding.Mode,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                foreach(var rules in HttpPortByBinding.ValidationRules) binding.ValidationRules.Add(rules);
                BindingOperations.SetBinding(TextBoxHttpPort, TextBox.TextProperty, binding);
                TextBoxHttpPort.Text = valueText;
                return;
            }
            if(Validation.GetErrors(TextBoxSokcsPort).Count > 0)
            {
                string textValue = TextBoxSokcsPort.Text;
                var binding = new Binding()
                {
                    Path = SocksPortBybinding.Path,
                    Mode = SocksPortBybinding.Mode,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                foreach(var rules in SocksPortBybinding.ValidationRules) binding.ValidationRules.Add(rules);
                BindingOperations.SetBinding(TextBoxSokcsPort, TextBox.TextProperty, binding);
                TextBoxSokcsPort.Text = textValue;
                return;
            }
            _appConfig.HotkeySetting.Hotkey = viewData.GlobalHotkey;
            _appConfig.LocalPort.Http = viewData.HttpPort;
            _appConfig.LocalPort.Scoks = viewData.SocksPort;
            _appConfig.SystemProxySetting.UseProtocol = viewData.SystemProtocol;
            _appConfig.SystemProxySetting.ByPassUrl = viewData.SysProxyBypass;
            _appConfig.UpdateSetting();
            _appConfig.xrayHanler.ReLoad();
            _appConfig.hotkeyHandler.Load();
            Close();
        }
       
        private readonly List<Key> assistKeys = new List<Key>()
         {
              Key.LeftCtrl, Key.RightCtrl, Key.LeftAlt, Key.RightAlt,Key.LeftShift,Key.RightShift
         };
        private void TextboxByHotkeySetting_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;           
            if(!assistKeys.Contains(e.Key) && !assistKeys.Contains(e.SystemKey))
            {
                Key _key;
                if(e.Key == Key.System)
                {
                    _key = e.SystemKey;
                }
                else
                {
                    _key = e.Key;
                }
                viewData.GlobalHotkey = new Hotkey((KeyModifier)Keyboard.Modifiers, _key);
            }
        }

        private void TextboxPortnumber_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var numberKey = new[]
            {
                Key.D0, Key.D1, Key.D2, Key.D3, Key.D4,
                Key.D5, Key.D6, Key.D7, Key.D8, Key.D9,
                Key.NumPad0, Key.NumPad1, Key.NumPad2,
                Key.NumPad3, Key.NumPad4, Key.NumPad5, 
                Key.NumPad6, Key.NumPad7, Key.NumPad8,
                Key.NumPad9,Key.Back
            };
            if(Keyboard.Modifiers == ModifierKeys.None && numberKey.Contains(e.Key))
            {
                return;
            }
            e.Handled = true;
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
            _IsShowing = false;
            _appConfig.hotkeyHandler.IsPause = false;
           
        }
    }

    public class PortNumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int number;
            if (int.TryParse(value.ToString(), out number))
            {
                if(number >= 1 && number <= 65535)
                {
                    return ValidationResult.ValidResult;
                }                
            }
            return new ValidationResult(false, "请输入正确的端口号");
        }
    }
    public class SettingViewData : INotifyPropertyChanged
    {        
        private Hotkey _GlobalHotkey;
        public Hotkey GlobalHotkey
        {
            get { return _GlobalHotkey; }
            set
            {
                _GlobalHotkey= value;
                OnPropertyChanged(nameof(HotkeyStr));
            }
        }
        public string HotkeyStr
        {
            get
            {
                string res = string.Empty;
                if((GlobalHotkey.KeyModifier & KeyModifier.Ctrl) == KeyModifier.Ctrl)
                {
                    res += $"{KeyModifier.Ctrl}+";
                }
                if ((GlobalHotkey.KeyModifier & KeyModifier.Shift) == KeyModifier.Shift)
                {
                    res += $"{KeyModifier.Shift}+";
                }
                if ((GlobalHotkey.KeyModifier & KeyModifier.Alt) == KeyModifier.Alt)
                {
                    res += $"{KeyModifier.Alt}+";
                }
                res += GlobalHotkey.Key;
                return res;
            }
            set { }
        }


        private int _socksPort;
        public int SocksPort
        {
            get { return _socksPort; }
            set
            {
                if (_socksPort != value)
                {
                    _socksPort = value;
                    OnPropertyChanged(nameof(SocksPort));
                }
            }
        }


        private int _httpPort;
        public int HttpPort
        {
            get { return _httpPort; }
            set
            {
                if(_httpPort != value)
                {
                    _httpPort = value;
                    OnPropertyChanged(nameof(HttpPort));
                }
            }
        }

        public SystemProtocol SystemProtocol;

        public bool RadioHttpChecked
        {
            get => SystemProtocol == SystemProtocol.Http;
            set
            {
                if(value) SystemProtocol = SystemProtocol.Http;
                OnPropertyChanged(nameof(RadioHttpChecked));
            }
        }
        public bool RadioSocksChecked
        {
            get => SystemProtocol == SystemProtocol.Socks;
            set
            {
                if(value) SystemProtocol = SystemProtocol.Socks;
                OnPropertyChanged(nameof(RadioSocksChecked));
            }
        }

        private bool _radioHotkeyOn;
        public bool RadioHotkeyOn
        {
            get => _radioHotkeyOn;
            set
            {
                _radioHotkeyOn = value;
                OnPropertyChanged(nameof(RadioHotkeyOn));
            }
        }

        private bool _radioHotkeyOff;
        public bool RadioHotkeyOff
        {
            get => _radioHotkeyOff;
            set
            {
                _radioHotkeyOff = value;
                OnPropertyChanged(nameof(RadioHotkeyOff));
            }
        }

        private string _sysProxyBypass = string.Empty;
        public string SysProxyBypass
        {
            get => _sysProxyBypass;
            set
            {
                _sysProxyBypass = value;
                OnPropertyChanged(nameof(SysProxyBypass));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}

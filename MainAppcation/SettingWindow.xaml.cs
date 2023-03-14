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
using HandyControl.Themes;


namespace NetProxyController
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        private ProxyServerInfo ProxyServer = default!;
        private AppConfigration _appConfig;
        private bool _IsShowing = false;
        internal SettingWindow(AppConfigration appConfig)
        {
            InitializeComponent();
            Icon = ImageHelper.ChangeBitmapToImageSource(Resource.Setting);
            _appConfig = appConfig;            
        }

        public new void Show()
        {
            if(!_IsShowing)
            {
                ProxyServer = new ProxyServerInfo(_appConfig.ProxyUrl);
                TextBoxByServerAddr.Text = ProxyServer.ServerAddr;
                TextBoxByProxyPort.Text = ProxyServer.ServerPort;
                TextBoxByPassUrl.Text = _appConfig.Bypass;
                RadioByHotkeyEnable.IsChecked = _appConfig.IsHotkeyRegEnabled;
                RadioByHotkeyDisable.IsChecked = !_appConfig.IsHotkeyRegEnabled;
                RadioByProtocolHttp.IsChecked = ProxyServer.Protocol == ProxyType.Http;
                RadioByProtocolSocks.IsChecked = ProxyServer.Protocol == ProxyType.Socks;
                viewData.GlobalHotkey = _appConfig.ProxyHotkey;
                _appConfig.IsHotkeyPause = true;
                _IsShowing = true;
                base.Show();             
            }

            WindowState = WindowState.Normal;
            Activate();

        }

        private void RadioByHotkeyEnable_Checked(object sender, RoutedEventArgs e)
        {
            
            TextboxByHotkeySetting.IsEnabled= true;
            
        }

        private void RadioByHotkeyDisable_Checked(object sender, RoutedEventArgs e)
        {
            TextboxByHotkeySetting.IsEnabled = false;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(PortBybinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
            {
                TextBoxByProxyPort.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            if (ServerAddrByBinding.UpdateSourceTrigger == UpdateSourceTrigger.Explicit)
            {
                TextBoxByServerAddr.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            if (Validation.GetErrors(TextBoxByProxyPort).Count > 0)
            {
                string valueText = TextBoxByProxyPort.Text;
                var binding = new Binding()
                {
                    Path = PortBybinding.Path,
                    Mode = PortBybinding.Mode,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                foreach(var rules in PortBybinding.ValidationRules) binding.ValidationRules.Add(rules);
                BindingOperations.SetBinding(TextBoxByProxyPort, TextBox.TextProperty, binding);
                TextBoxByProxyPort.Text = valueText;
                return;
            }
            if(Validation.GetErrors(TextBoxByServerAddr).Count > 0)
            {
                string textValue = TextBoxByServerAddr.Text;
                var binding = new Binding()
                {
                    Path = ServerAddrByBinding.Path,
                    Mode = ServerAddrByBinding.Mode,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                foreach(var rules in ServerAddrByBinding.ValidationRules) binding.ValidationRules.Add(rules);
                BindingOperations.SetBinding(TextBoxByServerAddr, TextBox.TextProperty, binding);
                TextBoxByServerAddr.Text = textValue;
                return;
            }
            ProxyServer.Protocol = RadioByProtocolHttp.IsChecked ?? true ? ProxyType.Http : ProxyType.Socks;
            ProxyServer.ServerPort = TextBoxByProxyPort.Text;
            ProxyServer.ServerAddr = TextBoxByServerAddr.Text;
            _appConfig.ProxyHotkey = viewData.GlobalHotkey;
            _appConfig.ProxyUrl = ProxyServer.ToString();
            _appConfig.IsHotkeyRegEnabled = RadioByHotkeyEnable.IsChecked ?? false;
            _appConfig.Bypass = TextBoxByPassUrl.Text;
            _appConfig.Reload();
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

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            Visibility = Visibility.Collapsed;
            _IsShowing = false;
            _appConfig.IsHotkeyPause = false;
            _appConfig.IsHotkeyPause = false;
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
    public class IpAddrOrDnsNameValidationRule: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string dnsNamePattern = @"^[a-zA-Z][a-zA-Z0-9]+(\.[a-zA-Z0-9]+)*$";
            string ipAddrPattern = @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$";
            string input = value.ToString() ?? string.Empty;
            if(Regex.IsMatch(input,ipAddrPattern) || Regex.IsMatch(input,dnsNamePattern))
            {
                return ValidationResult.ValidResult;
            }
            else
            {
                return new ValidationResult(false, "IP/域名格式不正确");
            }

        }
    }
    public class SettingViewData : INotifyPropertyChanged
    {
        private string _portNumber = string.Empty;
        private Hotkey _GlobalHotkey;
        public Hotkey GlobalHotkey
        {
            get { return _GlobalHotkey; }
            set
            {
                _GlobalHotkey= value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HotkeyStr)));
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
        public string PortNumber
        {
            get { return _portNumber; }
            set
            {
                if (_portNumber != value)
                {
                    _portNumber = value;
                    OnPropertyChanged(nameof(PortNumber));
                }
            }
        }
        private string _ServerAddr = string.Empty;
        public string ServerAddr
        {
            get { return _ServerAddr; }
            set
            {
                if(_ServerAddr != value)
                {
                    _ServerAddr = value;
                    OnPropertyChanged(nameof(ServerAddr));
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }


}

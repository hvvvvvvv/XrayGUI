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
using NetProxyController.ViewModle;

namespace NetProxyController
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        private MainConfigration _appConfig;
        private bool _IsShowing = false;
        private SettingWindowViewModle viewModle;
        internal SettingWindow(MainConfigration appConfig)
        {
            _appConfig = appConfig;
            viewModle = new(_appConfig);
            DataContext = viewModle;
            InitializeComponent();           
            Icon = ImageHelper.IconToImageSource(Resource.Setting);
            //_appConfig = appConfig;            
        }

        public new void Show()
        {
            _IsShowing = true;
            base.Show();
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
                viewModle.Hotkey = new Hotkey((KeyModifier)Keyboard.Modifiers, _key);
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

}

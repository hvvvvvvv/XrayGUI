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
using XrayGUI.Tools;
using HandyControl.Themes;
using XrayGUI.Modle;
using Windows.Services.Store;
using XrayCoreConfigModle;
using XrayGUI.ViewModle;
using Windows.ApplicationModel.Store.LicenseManagement;

namespace XrayGUI
{
    /// <summary>
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    internal partial class SettingWindow : Window
    {
        public bool IsClosed = false;
        internal SettingWindow()
        {
            InitializeComponent();
            Closing += (_,_) => IsClosed = true;            
        }
        private static SettingWindow _instance = new SettingWindow();
        public static SettingWindow Instance
        {
            get
            {
                if(_instance.IsClosed) _instance = new SettingWindow();
                return _instance;
            }
        }

    }
}

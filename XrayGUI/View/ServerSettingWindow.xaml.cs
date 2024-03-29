﻿using XrayGUI.Modle.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using XrayGUI.ViewModle;

namespace XrayGUI.View
{
    /// <summary>
    /// ServerSettingWindow.xaml 的交互逻辑
    /// </summary>
    internal partial class ServerSettingWindow : Window
    {
        public ServerSettingWindow():this(new ServerItem())
        {
        }
        public ServerSettingWindow(ServerItem server)
        {

            DataContext = new ServerSettingViewModle(server);
            InitializeComponent();
            
        }
    }
}

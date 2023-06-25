﻿using NetProxyController.ViewModle;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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


namespace NetProxyController.View
{
    /// <summary>
    /// ServerManager.xaml 的交互逻辑
    /// </summary>
    internal partial class ServerManager : Window
    {
        public ServerManager(ServerManagerViewModle vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
        public ServerManager() : this(new ServerManagerViewModle())
        {

        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine($"SelectionChanged Seleted Count {listView.SelectedItems.Count}");
        }
    }
}

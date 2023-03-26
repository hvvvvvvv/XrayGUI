﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace NetProxyController.View
{
    /// <summary>
    /// TcpSetting.xaml 的交互逻辑
    /// </summary>
    public partial class TcpSetting : UserControl
    {
        public TcpSetting()
        {
            InitializeComponent();
        }
    }

    public class MyData
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public MyData(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

}

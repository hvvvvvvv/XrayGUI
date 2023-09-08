using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Hardcodet.Wpf.TaskbarNotification;
using System.Windows.Controls;
using WindowsProxy;
using System.Windows.Controls.Ribbon;
using System.Security.Cryptography;
using NetProxyController.ViewModle;
using System.Drawing;
using NetProxyController.Modle;

namespace NetProxyController.View
{
    public partial class NotifyIcon : TaskbarIcon
    {
        public NotifyIcon()
        {
            InitializeComponent();
        }
        public void Show() => Visibility = Visibility.Visible;
    }
}

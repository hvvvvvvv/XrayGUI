using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Hardcodet.Wpf.TaskbarNotification;
using ProxyNotifyWindow;
namespace NetProxyController
{
    internal class Program
    {
        [STAThread]
        static void Main()
        {
            var app = new Application
            {
                ShutdownMode = ShutdownMode.OnExplicitShutdown
            };
            //var res1 = new ResourceDictionary();
            //var res2 = new ResourceDictionary();
            //res1.Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDefault.xaml");
            //res2.Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml");
            //app.Resources = new ResourceDictionary();
            //app.Resources.MergedDictionaries.Add(res1);
            //app.Resources.MergedDictionaries.Add(res2);
            TaskBarIconByPorxyControl taskBar = new();
            taskBar.Show();
            app.Run();
        }
    }
}
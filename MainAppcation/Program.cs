using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Hardcodet.Wpf.TaskbarNotification;
using ProxyNotifyWindow;
using static Vanara.PInvoke.Kernel32;
using NetProxyController.View;

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
            SetProcessJobs();
            //var res1 = new ResourceDictionary();
            //var res2 = new ResourceDictionary();
            //res1.Source = new Uri("pack://application:,,,/HandyControl;component/Themes/SkinDefault.xaml");
            //res2.Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml");
            //app.Resources = new ResourceDictionary();
            //app.Resources.MergedDictionaries.Add(res1);
            //app.Resources.MergedDictionaries.Add(res2);
            NotifyIcon taskBar = new();
            taskBar.Show();
            
            app.Run();
        }
        static void SetProcessJobs()
        {
            var setClass = JOBOBJECTINFOCLASS.JobObjectExtendedLimitInformation;
            var jobInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION()
            {
                BasicLimitInformation = new()
                {
                    LimitFlags = JOBOBJECT_LIMIT_FLAGS.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE
                }
            };
            SetInformationJobObject(Modle.Global.ProcessJobs, setClass, jobInfo);            
        }
    }
}
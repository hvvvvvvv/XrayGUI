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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace NetProxyController.View
{
    /// <summary>
    /// NotifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyWindow : Window
    {
        private Storyboard _Storyboard;
        private ViewModle.NotifyWindowViewModle Vm;
        public NotifyWindow()
        {
            InitializeComponent();
            Vm = DataContext as ViewModle.NotifyWindowViewModle ?? new();
            _Storyboard = new();
            var ami = new DoubleAnimation()
            {
                From = Vm.MaxWindowOpacity,
                To = Vm.MinWindowOpacity,
                Duration = new(TimeSpan.FromSeconds(1)),
                FillBehavior = FillBehavior.Stop
            };
            ami.Completed += (s, e) => Visibility = Visibility.Hidden;
            Storyboard.SetTargetProperty(ami, new PropertyPath(nameof(Opacity)));
            Storyboard.SetTarget(ami, this);
            _Storyboard.Children.Add(ami);
        }
    }
}

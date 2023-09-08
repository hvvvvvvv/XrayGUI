using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;

namespace XrayGUI.View
{
    /// <summary>
    /// NotifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyWindow : Window
    {
        private Storyboard _Storyboard;
        private ViewModle.NotifyWindowViewModle Vm;
        private List<CancellationTokenSource> storyBoardCancelTokens;
        public NotifyWindow()
        {
            InitializeComponent();
            storyBoardCancelTokens = new();
            Vm = DataContext as ViewModle.NotifyWindowViewModle ?? new();
            _Storyboard = new();
            var ami = new DoubleAnimation()
            {
                From = ViewModle.NotifyWindowViewModle.MaxWindowOpacity,
                To = ViewModle.NotifyWindowViewModle.MinWindowOpacity,
                Duration = new(TimeSpan.FromMilliseconds(ViewModle.NotifyWindowViewModle.FadeOutTime)),
                FillBehavior = FillBehavior.Stop
            };
            ami.Completed += (s, e) => Visibility = Visibility.Hidden;
            Storyboard.SetTargetProperty(ami, new PropertyPath(nameof(Opacity)));
            Storyboard.SetTarget(ami, this);
            _Storyboard.Children.Add(ami);
            Vm.OnWindowIsVisible += StartStoryBoard;
        }
        private void StopStoryBoard()
        {
            storyBoardCancelTokens.ForEach(i => i.Cancel());
            storyBoardCancelTokens.Clear();
            _Storyboard.Stop();
        }
        private async void StartStoryBoard()
        {
            StopStoryBoard();
            var cts = new CancellationTokenSource();
            storyBoardCancelTokens.Add(cts);
            try
            {
                await Task.Delay(ViewModle.NotifyWindowViewModle.DelayTime, cts.Token);
                Dispatcher.Invoke(_Storyboard.Begin, DispatcherPriority.Normal, cts.Token);
            }
            catch(TaskCanceledException) { }
        }
    }
}

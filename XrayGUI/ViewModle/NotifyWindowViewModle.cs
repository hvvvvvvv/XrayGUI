using XrayGUI.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace XrayGUI.ViewModle
{
    internal class NotifyWindowViewModle : ViewModleBase
    {
        public const double MaxWindowOpacity = 0.8;
        public const double MinWindowOpacity = 0.3;
        public const int FadeOutTime = 1000;
        public const int DelayTime = 2000;
        private static BitmapImage ProxyEnabledImage = new(new Uri("pack://application:,,,/Images/ProxyEnable.png"));
        private static BitmapImage ProxyDisabledImage = new(new Uri("pack://application:,,,/Images/ProxyDisable.png"));
        public BitmapImage ProxyStatusImagePath => ConfigObject.Instance.ProxyEnable ? ProxyEnabledImage : ProxyDisabledImage;
        public event Action? OnWindowIsVisible;
        private Visibility windowVisiblity;
        public Visibility WindowVisiblity
        {
            get => windowVisiblity;
            set
            {                           
                if(value == Visibility.Visible)
                {
                    ResetView();
                    OnWindowIsVisible?.Invoke();
                }
                windowVisiblity = value;
                OnPropertyChanged();
            }
        }
        private double windowOpacity;
        public double WindowOpacity
        {
            get => windowOpacity;
            set
            {
                windowOpacity = value;
                OnPropertyChanged();
            }
        }
        private void ResetView()
        {
            OnPropertyChanged(nameof(ProxyStatusImagePath));
            WindowOpacity = MaxWindowOpacity;
        }
        public NotifyWindowViewModle()
        {
            windowOpacity = MinWindowOpacity;
            windowVisiblity = Visibility.Collapsed;
        }
    }
}

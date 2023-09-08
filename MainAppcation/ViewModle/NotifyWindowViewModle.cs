using NetProxyController.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;

namespace NetProxyController.ViewModle
{
    internal class NotifyWindowViewModle : ViewModleBase
    {
        public double MaxWindowOpacity => 0.8;
        public double MinWindowOpacity => 0.3;
        private Storyboard storyboard;
        public string ProxyStatusImagePath => ConfigObject.Instance.ProxyEnable ? "/Images/ProxyEnable.png" : "/Images/ProxyDisable.png";
        private Visibility windowVisiblity;
        public Visibility WindowVisiblity
        {
            get => windowVisiblity;
            set
            {
                windowVisiblity = value;              
                if(value == Visibility.Visible) ResetView();
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
                if (value <= MinWindowOpacity) WindowVisiblity = Visibility.Hidden;
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

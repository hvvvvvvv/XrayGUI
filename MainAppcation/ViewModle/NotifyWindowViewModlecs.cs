using NetProxyController.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetProxyController.ViewModle
{
    internal class NotifyWindowViewModlecs : ViewModleBase
    {
        private const double MaxWindowOpacity = 1.0;
        private const double MinWindowOpacity = 0.0;
        public string ProxyStatusImagePath => ConfigObject.Instance.ProxyEnable ? "/Images/ProxyEnable.png" : "/Images/ProxyDisable.png";
        private Visibility windowVisiblity;
        public Visibility WindowVisiblity
        {
            get => windowVisiblity;
            set
            {
                windowVisiblity = value;
                OnPropertyChanged();
            }
        }
        private double windowOpacity = MinWindowOpacity;
        public double WindowOpacity
        {
            get => windowOpacity;
            set
            {
                windowOpacity = value;
                OnPropertyChanged();
                WindowVisiblity = value <= MinWindowOpacity ? Visibility.Hidden : Visibility.Visible;
            }
        }
        public void ResetView()
        {
            OnPropertyChanged(nameof(ProxyStatusImagePath));
            WindowOpacity = MaxWindowOpacity;
        }
    }
}

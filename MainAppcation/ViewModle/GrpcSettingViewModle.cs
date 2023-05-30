using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class GrpcSettingViewModle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private GrpcInfo info;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(propertyName)));
        }
        public GrpcSettingViewModle(GrpcInfo info)
        {
            this.info = info;
        }
        public GrpcSettingViewModle()
        {
            info = new();
        }
        public string ServiceName
        {
            get => info.ServiceName;
            set
            {
                info.ServiceName = value;
                OnPropertyChanged(nameof(ServiceName));
            }
        }
        public int IdleTimeout
        {
            get => info.IdleTimeout;
            set
            {
                info.IdleTimeout = value;
                OnPropertyChanged(nameof(IdleTimeout));
            }
        }
        public int HealthCheckTimeout
        {
            get => info.HealthCheckTimeout; 
            set
            {
                info.HealthCheckTimeout = value;
                OnPropertyChanged(nameof(HealthCheckTimeout));
            }
        }
        public bool PermitWithoutStream
        {
            get => info.PermitWithoutStream; 
            set
            {
                info.PermitWithoutStream = value;
                OnPropertyChanged(nameof(PermitWithoutStream));
            }
        }
        public int InitialWindowsSize
        {
            get => info.InitialWindowsSize; 
            set
            {
                info.InitialWindowsSize = value;
                OnPropertyChanged(nameof(InitialWindowsSize));
            }
        }

    }
}

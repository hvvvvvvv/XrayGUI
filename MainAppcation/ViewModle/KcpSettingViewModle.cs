using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class KcpSettingViewModle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private KcpInfo info;
        public KcpSettingViewModle(KcpInfo info)
        {
            this.info = info;
        }
        public KcpSettingViewModle()
        {
            info = new();
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public int Mtu
        {
            get => info.Mtu;
            set
            {
                info.Mtu = value;
                OnPropertyChanged(nameof(Mtu));
            }
        }
        public int TTI
        {
            get => info.TTI;
            set
            {
                info.TTI = value;
                OnPropertyChanged(nameof(TTI));
            }
        }
        public int UplinkCapacity
        {
            get => info.UplinkCapacity;
            set
            {
                info.UplinkCapacity = value;
                OnPropertyChanged(nameof(UplinkCapacity));
            }
        }
        public int DownlinkCapacity
        { 
            get => info.DownlinkCapacity; 
            set
            {
                info.DownlinkCapacity = value;
                OnPropertyChanged(nameof(DownlinkCapacity));
            }
        }
        public int ReadBufferSize
        {
            get => info.ReadBufferSize; 
            set
            {
                info.ReadBufferSize = value;
                OnPropertyChanged(nameof(ReadBufferSize));
            }
        }
        public int WriteBufferSize
        { 
            get => info.WriteBufferSize; 
            set
            {
                info.WriteBufferSize = value;
                OnPropertyChanged(nameof(WriteBufferSize));
            }
        }
        public bool Congestion
        {
            get => info.Congestion; 
            set
            {
                info.Congestion = value;
                OnPropertyChanged(nameof(Congestion));
            }
        }
        public string Seed
        {
            get => info.Seed;
            set
            {
                info.Seed = value;
                OnPropertyChanged(nameof(Seed));
            }
        }
        public IEnumerable<FeignType> FeignValues { get; private set; } = Enum.GetValues<FeignType>().Cast<FeignType>();
        public FeignType FeignSelectedValue
        {
            get => info.Feign;
            set
            {
                info.Feign = value;
                OnPropertyChanged(nameof(FeignSelectedValue));
            }
        }
    }
}

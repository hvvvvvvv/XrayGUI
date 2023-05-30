using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class RealityInfoSettingViewModle : INotifyPropertyChanged
    {
        public RealityInfoSettingViewModle(RealityInfo info)
        {
            this.info = info;
        }
        public RealityInfoSettingViewModle()
        {
            info = new();
        }  
        public event PropertyChangedEventHandler? PropertyChanged;
        private RealityInfo info;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string ServerName
        {
            get => info.ServerName;
            set
            {
                info.ServerName = value;
                OnPropertyChanged(nameof(ServerName));
            }
        }
        public IEnumerable<TlsFingerPrint> FingerPrintValues { get;} = Enum.GetValues<TlsFingerPrint>().Cast<TlsFingerPrint>();
        public TlsFingerPrint FingerPrintSelectedValue
        {
            get => info.FingerPrint;
            set
            {
                info.FingerPrint = value;
                OnPropertyChanged(nameof(FingerPrintSelectedValue));
            }
        }
        public string ShortId
        {
            get => info.ShortId;
            set
            {
                info.ShortId = value;
                OnPropertyChanged(nameof(ShortId));
            }
        }
        public string PublicKey
        {
            get => info.PublicKey;
            set
            {
                info.PublicKey = value;
                OnPropertyChanged(nameof(PublicKey));
            }
        }
        public string SpiderX
        {
            get => info.SpiderX; 
            set
            {
                info.SpiderX = value;
                OnPropertyChanged(nameof(SpiderX));
            }
        }

    }
}

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
    internal class TlsSettingViewModle : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private TlsInfo info;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TlsSettingViewModle(TlsInfo info)
        {
            this.info = info;
        }
        public TlsSettingViewModle()
        {
            info = new();
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
        public IEnumerable<TlsFingerPrint> FingerPrintValues { get; private set; } = Enum.GetValues<TlsFingerPrint>().Cast<TlsFingerPrint>();
        public TlsFingerPrint FingerPrintSelectedValue
        {
            get => info.FingerPrint;
            set
            {
                info.FingerPrint = value;
                OnPropertyChanged(nameof(FingerPrintSelectedValue));
            }
        }
        public bool AllowInsecure
        {
            get => info.AllowInsecure;
            set
            {
                info.AllowInsecure = value;
                OnPropertyChanged(nameof(AllowInsecure));
            }
        }
    }
}

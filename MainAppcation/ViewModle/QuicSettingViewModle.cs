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
    internal class QuicSettingViewModle : INotifyPropertyChanged
    {
        private QuicInfo info;
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public QuicSettingViewModle(QuicInfo info)
        {
            this.info = info;
        }
        public QuicSettingViewModle()
        {
            info = new();
        }
        public IEnumerable<SecurityMode> SecurityModeValues { get; set; } = new List<SecurityMode>
        {
            SecurityMode.None,
            SecurityMode.Chacha20_poly1305,
            SecurityMode.Aes_128_gcm
        };
        public SecurityMode Security
        {
            get => info.Security;
            set
            {
                info.Security = value;
                OnPropertyChanged(nameof(Security));
            }
        }
        public string Key
        {
            get => info.Key;
            set
            {
                info.Key = value;
            }
        }
        public IEnumerable<FeignType> Feign { get; private set; } = Enum.GetValues<FeignType>().Cast<FeignType>();
        public FeignType FeignSelectedValue
        {
            get => info.Feign;
            set
            {
                info.Feign = value;
                OnPropertyChanged(nameof(Feign));
            }
        }

    }
}

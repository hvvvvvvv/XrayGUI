using XrayGUI.Modle;
using XrayGUI.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Windows.AI.MachineLearning.Preview;

namespace XrayGUI.ViewModle
{
    internal class ShadowSocksVerifyInfoViewModle : ViewModleBase
    {
        private ShadowSocksInfo info;
        public ShadowSocksVerifyInfoViewModle(ShadowSocksInfo info)
        {
            this.info = info;
        }
        public ShadowSocksVerifyInfoViewModle()
        {
            info = new();
        }
        public IEnumerable<SS_Ecrept> EncryptMethodVlues { get; private set; } = Enum.GetValues<SS_Ecrept>().Cast<SS_Ecrept>();
        public SS_Ecrept EnceryptMethodSelectedValue
        {
            get => info.Method;
            set
            {
                info.Method = value;
                OnPropertyChanged(nameof(EnceryptMethodSelectedValue));
            }
        }
        public string Password
        {
            get => info.Password;
            set
            {
                info.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Eamail
        {
            get => info.Email;
            set
            {
                info.Email = value;
                OnPropertyChanged(nameof(Eamail));
            }
        }
        public bool Uot
        {
            get => info.Uot; 
            set
            {
                info.Uot = value;
                OnPropertyChanged(nameof(Uot));
            }
        }

    }
}

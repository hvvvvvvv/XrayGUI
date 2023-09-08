using XrayGUI.Modle;
using XrayGUI.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayGUI.ViewModle
{
    internal class TlsSettingViewModle : ViewModleBase
    {
        private TlsInfo info;
        public TlsSettingViewModle(TlsInfo info)
        {
            this.info = info;
        }
        public TlsSettingViewModle()
        {
            info = new();
        }
        [Required(ErrorMessage = "ServerName不能为空")]
        public string ServerName
        {
            get => info.ServerName;
            set
            {
                info.ServerName = value;
                OnPropertyChanged(nameof(ServerName));
                ValidationProperty();
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

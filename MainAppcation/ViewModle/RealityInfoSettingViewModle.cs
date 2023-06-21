using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class RealityInfoSettingViewModle : ViewModleBase
    {
        public RealityInfoSettingViewModle(RealityInfo info)
        {
            this.info = info;
        }
        public RealityInfoSettingViewModle() : this(new())
        {

        }  
        private RealityInfo info;
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
        [Required(ErrorMessage = "PublicKey 不能为空")]
        public string PublicKey
        {
            get => info.PublicKey;
            set
            {
                info.PublicKey = value;
                OnPropertyChanged(nameof(PublicKey));
                ValidationProperty();
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

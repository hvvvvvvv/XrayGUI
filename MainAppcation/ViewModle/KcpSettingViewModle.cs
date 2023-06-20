using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.ViewModle
{
    internal class KcpSettingViewModle : ViewModleBase
    {
        private KcpInfo info;
        public KcpSettingViewModle(KcpInfo info)
        {
            this.info = info;
            _mtu = info.Mtu.ToString();
            _tti = info.TTI.ToString();
            uplinkCapacity = info.UplinkCapacity.ToString();
            downlinkCapacity = info.UplinkCapacity.ToString();
            readBufferSize = info.ReadBufferSize.ToString();
            writeBufferSize = info.WriteBufferSize.ToString();
        }
        public KcpSettingViewModle(): this(new())
        {
        }
        private string _mtu;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:576|5[8-9][0-9]|6[0-9]{2}|7[0-3][0-9]|14[0-5][0-9])$",
            ErrorMessage = "请输入正确的数值(576-1460)")]
        public string Mtu
        {
            get => _mtu;
            set
            {
                _mtu = value;
                OnPropertyChanged(nameof(Mtu));
                if(ValidationProperty())
                {
                    info.Mtu = Convert.ToInt32(Mtu);
                }
            }
        }
        private string _tti;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:[1-9][0-9]|100)$",ErrorMessage = "请输入正确的数值(10-100)")]
        public string TTI
        {
            get => _tti;
            set
            {
                _tti = value;
                OnPropertyChanged(nameof(TTI));
                if(ValidationProperty())
                {
                    info.TTI = Convert.ToInt32(TTI);
                }
            }
        }
        private string uplinkCapacity;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:0|[1-9]\d{0,8}|[1-2]\d{9}|3[0-1]\d{8}|32[0-1]\d{7}|32[0-9]{6}[0-3][0-9]" +
            @"|2[2-9]\d{7}|2[2-9]\d{6}[0-9]{1,2}|2[2-9]\d{5}[0-9]{1,4}|2[2-9]\d{4}[0-9]{1,6}|2[2-9]\d{3}" +
            @"[0-9]{1,8}|2[2-9]\d{2}[0-9]{1,10}|2[2-9]\d{1}[0-9]{1,12}|2[2-3][0-9]{12})$",
            ErrorMessage = "请输入正确的数值(0-2147483647)")]
        public string UplinkCapacity
        {
            get => uplinkCapacity;
            set
            {
                uplinkCapacity = value;
                OnPropertyChanged(nameof(UplinkCapacity));
                if(ValidationProperty())
                {
                    info.UplinkCapacity = Convert.ToInt32(UplinkCapacity);
                }
            }
        }
        private string downlinkCapacity;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:0|[1-9]\d{0,8}|[1-2]\d{9}|3[0-1]\d{8}|32[0-1]\d{7}|32[0-9]{6}[0-3][0-9]" +
            @"|2[2-9]\d{7}|2[2-9]\d{6}[0-9]{1,2}|2[2-9]\d{5}[0-9]{1,4}|2[2-9]\d{4}[0-9]{1,6}|2[2-9]\d{3}" +
            @"[0-9]{1,8}|2[2-9]\d{2}[0-9]{1,10}|2[2-9]\d{1}[0-9]{1,12}|2[2-3][0-9]{12})$",
            ErrorMessage = "请输入正确的数值(0-2147483647)")]
        public string DownlinkCapacity
        { 
            get => downlinkCapacity; 
            set
            {
                downlinkCapacity = value;
                OnPropertyChanged(nameof(DownlinkCapacity));
                if(ValidationProperty())
                {
                    info.DownlinkCapacity = Convert.ToInt32(DownlinkCapacity);
                }
            }
        }
        private string readBufferSize;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:0|[1-9]\d{0,8}|[1-2]\d{9}|3[0-1]\d{8}|32[0-1]\d{7}|32[0-9]{6}[0-3][0-9]" +
            @"|2[2-9]\d{7}|2[2-9]\d{6}[0-9]{1,2}|2[2-9]\d{5}[0-9]{1,4}|2[2-9]\d{4}[0-9]{1,6}|2[2-9]\d{3}" +
            @"[0-9]{1,8}|2[2-9]\d{2}[0-9]{1,10}|2[2-9]\d{1}[0-9]{1,12}|2[2-3][0-9]{12})$",
            ErrorMessage = "请输入正确的数值(0-2147483647)")]
        public string ReadBufferSize
        {
            get => readBufferSize; 
            set
            {
                readBufferSize = value;
                OnPropertyChanged(nameof(ReadBufferSize));
                if(ValidationProperty())
                {
                    info.ReadBufferSize = Convert.ToInt32(ReadBufferSize);
                }
            }
        }
        private string writeBufferSize;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:0|[1-9]\d{0,8}|[1-2]\d{9}|3[0-1]\d{8}|32[0-1]\d{7}|32[0-9]{6}[0-3][0-9]" +
            @"|2[2-9]\d{7}|2[2-9]\d{6}[0-9]{1,2}|2[2-9]\d{5}[0-9]{1,4}|2[2-9]\d{4}[0-9]{1,6}|2[2-9]\d{3}" +
            @"[0-9]{1,8}|2[2-9]\d{2}[0-9]{1,10}|2[2-9]\d{1}[0-9]{1,12}|2[2-3][0-9]{12})$",
            ErrorMessage = "请输入正确的数值(0-2147483647)")]
        public string WriteBufferSize
        { 
            get => writeBufferSize; 
            set
            {
                writeBufferSize = value;
                OnPropertyChanged(nameof(WriteBufferSize));
                if(ValidationProperty())
                {
                    info.WriteBufferSize = Convert.ToInt32(WriteBufferSize);
                }
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

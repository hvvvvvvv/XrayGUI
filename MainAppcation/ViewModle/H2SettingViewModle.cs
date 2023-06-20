using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;

namespace NetProxyController.ViewModle
{
    internal class H2SettingViewModle : ViewModleBase
    {
        private H2Info info;
        public H2SettingViewModle(H2Info info)
        {
            this.info = info;
            readIdleTimeout = info.ReadIdleTimeout.ToString();
            healthCheckTimeout = info.HealthCheckTimeout.ToString();
        }
        public H2SettingViewModle() :this(new())
        {
        }
        public string Hosts
        {
            get => info.Hosts;
            set
            {
                info.Hosts = value;
                OnPropertyChanged(nameof(Hosts));
            }
        }
        public string Path
        {
            get => info.Path;
            set
            {
                info.Path = value;
                OnPropertyChanged(nameof(Path));
            }
        }
        private string readIdleTimeout;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:0|[1-9]\d{0,8}|[1-2]\d{9}|3[0-1]\d{8}|32[0-1]\d{7}|32[0-9]{6}[0-3][0-9]" +
            @"|2[2-9]\d{7}|2[2-9]\d{6}[0-9]{1,2}|2[2-9]\d{5}[0-9]{1,4}|2[2-9]\d{4}[0-9]{1,6}|2[2-9]\d{3}" +
            @"[0-9]{1,8}|2[2-9]\d{2}[0-9]{1,10}|2[2-9]\d{1}[0-9]{1,12}|2[2-3][0-9]{12})$",
            ErrorMessage = "请输入正确的数值(0-2147483647)")]
        public string ReadIdleTimeout
        { 
            get => readIdleTimeout; 
            set
            {
                readIdleTimeout = value;
                OnPropertyChanged(nameof(ReadIdleTimeout));
                if (ValidationProperty())
                {
                    info.ReadIdleTimeout = Convert.ToInt32(ReadIdleTimeout);
                }
                
            }
        }
        private string healthCheckTimeout;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:0|[1-9]\d{0,8}|[1-2]\d{9}|3[0-1]\d{8}|32[0-1]\d{7}|32[0-9]{6}[0-3][0-9]" +
            @"|2[2-9]\d{7}|2[2-9]\d{6}[0-9]{1,2}|2[2-9]\d{5}[0-9]{1,4}|2[2-9]\d{4}[0-9]{1,6}|2[2-9]\d{3}" +
            @"[0-9]{1,8}|2[2-9]\d{2}[0-9]{1,10}|2[2-9]\d{1}[0-9]{1,12}|2[2-3][0-9]{12})$",
            ErrorMessage = "请输入正确的数值(0-2147483647)")]
        public string HealthCheckTimeout
        {
            get => healthCheckTimeout;
            set
            {
                healthCheckTimeout = value;
                OnPropertyChanged(nameof(HealthCheckTimeout));
                if (ValidationProperty())
                {
                    info.HealthCheckTimeout = Convert.ToInt32(HealthCheckTimeout);
                }
            }
        }
        public IEnumerable<HttpRequestType> MethodValues { get; private set; } = Enum.GetValues<HttpRequestType>().Cast<HttpRequestType>();
        public HttpRequestType MethodSelectedValue
        {
            get => info.Method;
            set
            {
                info.Method = value;
                OnPropertyChanged(nameof(MethodSelectedValue));
            }
        }
    }
}

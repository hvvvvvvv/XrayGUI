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
    internal class GrpcSettingViewModle : ViewModleBase
    {        
        private GrpcInfo info;
        public GrpcSettingViewModle(GrpcInfo info)
        {
            this.info = info;
            healthCheckTimeout = info.HealthCheckTimeout.ToString();
            idleTimeout = info.IdleTimeout.ToString();
            initialWindowsSize = info.InitialWindowsSize.ToString();
        }
        public GrpcSettingViewModle(): this(new())
        {

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
        private string idleTimeout;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:[1-9]\d{0,8}|[1-2]\d{9}|3[0-1]\d{8}|32[0-1]\d{7}|32[0-9]{6}[0-3][0-9]"+
            @"|2[2-9]\d{7}|2[2-9]\d{6}[0-9]{1,2}|2[2-9]\d{5}[0-9]{1,4}|2[2-9]\d{4}[0-9]{1,6}|2[2-9]\d{3}"+
            @"[0-9]{1,8}|2[2-9]\d{2}[0-9]{1,10}|2[2-9]\d{1}[0-9]{1,12}|2[2-3][0-9]{12})$", 
            ErrorMessage = "请输入正确的数值(10-2147483647)")]
        public string IdleTimeout
        {
            get => idleTimeout;
            set
            {
                idleTimeout = value;               
                OnPropertyChanged(nameof(IdleTimeout));
                if (ValidationProperty())
                {
                    info.IdleTimeout = Convert.ToInt32(IdleTimeout);
                }
            }
        }
        private string healthCheckTimeout;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:0|[1-9]\d{0,8}|[1-2]\d{9}|3[0-1]\d{8}|32[0-1]\d{7}|32[0-9]{6}[0-3][0-9]"+
            @"|2[2-9]\d{7}|2[2-9]\d{6}[0-9]{1,2}|2[2-9]\d{5}[0-9]{1,4}|2[2-9]\d{4}[0-9]{1,6}|2[2-9]\d{3}"+
            @"[0-9]{1,8}|2[2-9]\d{2}[0-9]{1,10}|2[2-9]\d{1}[0-9]{1,12}|2[2-3][0-9]{12})$",
            ErrorMessage = "请输入正确的数值(0-2147483647)")]
        public string HealthCheckTimeout
        {
            get => healthCheckTimeout; 
            set
            {
                HealthCheckTimeout = value;
                OnPropertyChanged();
                if(ValidationProperty())
                {
                    info.HealthCheckTimeout = Convert.ToInt32(HealthCheckTimeout);
                }
            }
        }
        public bool PermitWithoutStream
        {
            get => info.PermitWithoutStream; 
            set
            {
                info.PermitWithoutStream = value;
                OnPropertyChanged();
            }
        }
        private string initialWindowsSize;
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:[0-9]|[1-9][0-9]{1,3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$",
            ErrorMessage = "请输入正确的数值(0-65535)")]
        public string InitialWindowsSize
        {
            get => initialWindowsSize; 
            set
            {
                initialWindowsSize = value;
                OnPropertyChanged();
                if(ValidationProperty())
                {
                    info.InitialWindowsSize = Convert.ToInt32(InitialWindowsSize);
                }
            }
        }

    }
}

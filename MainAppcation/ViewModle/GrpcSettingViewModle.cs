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
        }
        public GrpcSettingViewModle()
        {
            info = new();
        }
        public string ServiceName
        {
            get => info.ServiceName;
            set
            {
                info.ServiceName = value;
                OnpropertyChannged(nameof(ServiceName));
            }
        }
        private string idleTimeout = "10";
        [Required(ErrorMessage = "值不能为空")]
        [RegularExpression(@"^(?:[1-9]\d{1,3}|[1-5]\d{4}|6[0-4]\d{3}|65[0-4]\d{2}|655[0-2]\d|6553[0-5])$",ErrorMessage = "请输入正确的数值(10-65535)")]
        public string IdleTimeout
        {
            get => idleTimeout;
            set
            {
                idleTimeout = value;               
                OnpropertyChannged(nameof(IdleTimeout));
                if (ValidationProperty())
                {
                    info.IdleTimeout = Convert.ToInt32(IdleTimeout);
                }
            }
        }
        public int HealthCheckTimeout
        {
            get => info.HealthCheckTimeout; 
            set
            {
                info.HealthCheckTimeout = value;
                OnpropertyChannged(nameof(HealthCheckTimeout));
            }
        }
        public bool PermitWithoutStream
        {
            get => info.PermitWithoutStream; 
            set
            {
                info.PermitWithoutStream = value;
                OnpropertyChannged(nameof(PermitWithoutStream));
            }
        }
        public int InitialWindowsSize
        {
            get => info.InitialWindowsSize; 
            set
            {
                info.InitialWindowsSize = value;
                OnpropertyChannged(nameof(InitialWindowsSize));
            }
        }

    }
}

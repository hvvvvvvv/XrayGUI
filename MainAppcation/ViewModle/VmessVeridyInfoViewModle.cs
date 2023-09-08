using XrayGUI.Modle;
using XrayGUI.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace XrayGUI.ViewModle
{
    internal class VmessVeridyInfoViewModle: ViewModleBase
    {
        private readonly VmessInfo Info;
        public VmessVeridyInfoViewModle(VmessInfo vmessInfo)
        {
            Info = vmessInfo;
            alterId = Info.AlterId.ToString();
        }
        public VmessVeridyInfoViewModle() : this(new())
        {

        }
        public IEnumerable<SecurityMode> SecurityModeValues { get; private set; } = Enum.GetValues<SecurityMode>().Cast<SecurityMode>();
        public SecurityMode SecurityModeSelectedValue
        {
            get => Info.Security;
            set
            {
                Info.Security = value;
                OnPropertyChanged(nameof(SecurityModeSelectedValue));
            }
        }
        public string Id
        {
            get => Info.Id;
            set
            {
                Info.Id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
        private string alterId;
        [Required(ErrorMessage = "AlterId不能为空")]
        [RegularExpression("^(?:[0-9]|[1-9][0-9]|[1-5][0-9]{2}|6[0-4][0-9]{1}|65[0-5])$",ErrorMessage = "请输入正确的数值(0-65535)")]
        public string AlterId
        {
            get => alterId; 
            set
            {
                alterId = value;
                OnPropertyChanged(nameof(AlterId));
                if(ValidationProperty())
                {
                    Info.AlterId = Convert.ToInt32(alterId);
                }
            }
        }
    }
}

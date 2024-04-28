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
    internal class VlessVerifyInfoViewModle: ViewModleBase
    {
        public VlessInfo info;
        public VlessVerifyInfoViewModle(VlessInfo vlessInfo)
        {
            info = vlessInfo;
        }
        public VlessVerifyInfoViewModle()
        {
            info = new();
        }
        [Required(ErrorMessage = "用户ID不能为空")]
        public string Id
        {
            get => info.Id;
            set
            {
                info.Id = value;
                OnPropertyChanged(nameof(Id));
                ValidationProperty();
            }
        }
        public IEnumerable<XtlsFlow> FlowValues { get; private set; } = Enum.GetValues<XtlsFlow>().Cast<XtlsFlow>();
        public XtlsFlow FlowSeletctedValue
        {
            get => info.Flow;
            set
            {
                info.Flow = value;
                OnPropertyChanged(nameof(FlowSeletctedValue));
            }
        }
    }
}

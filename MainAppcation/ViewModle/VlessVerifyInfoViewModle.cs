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
    internal class VlessVerifyInfoViewModle: INotifyPropertyChanged
    {
        private VlessInfo info;
        public VlessVerifyInfoViewModle(VlessInfo vlessInfo)
        {
            info = vlessInfo;
        }
        public VlessVerifyInfoViewModle()
        {
            info = new();
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public string Id
        {
            get => info.Id;
            set
            {
                info.Id = value;
                OnPropertyChanged(nameof(Id));
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

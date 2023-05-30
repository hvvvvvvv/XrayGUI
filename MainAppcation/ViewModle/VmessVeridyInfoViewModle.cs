using NetProxyController.Modle;
using NetProxyController.Modle.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace NetProxyController.ViewModle
{
    internal class VmessVeridyInfoViewModle: INotifyPropertyChanged
    {
        private readonly VmessInfo Info;
        public VmessVeridyInfoViewModle(VmessInfo vmessInfo)
        {
            Info = vmessInfo;
        }
        public VmessVeridyInfoViewModle()
        {
            Info = new();
        }
        private void OnProertyChanged(string proertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(proertyName)));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public IEnumerable<SecurityMode> SecurityModeValues { get; private set; } = Enum.GetValues<SecurityMode>().Cast<SecurityMode>();
        public SecurityMode SecurityModeSelectedValue
        {
            get => Info.Security;
            set
            {
                Info.Security = value;
                OnProertyChanged(nameof(SecurityModeSelectedValue));
            }
        }
        public string Id
        {
            get => Info.Id;
            set
            {
                Info.Id = value;
                OnProertyChanged(nameof(Id));
            }
        }
        public int AlterId
        {
            get => Info.AlterId; 
            set
            {
                Info.AlterId = value;
                OnProertyChanged(nameof(AlterId));
            }
        }
    }
}

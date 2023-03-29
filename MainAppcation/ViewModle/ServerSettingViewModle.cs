using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using NetProxyController.View;
using NetProxyController.Modle;

namespace NetProxyController.ViewModle
{
    internal class ServerSettingViewModle: INotifyPropertyChanged
    {
        private readonly Dictionary<OutboundProtocol, UserControl> VerifyInfoView = new()
        {
            {OutboundProtocol.trojan, new TrojanVerifyInfo() },
            {OutboundProtocol.vmess, new VmessVerifyInfo() },
            {OutboundProtocol.vless, new VlessVerifyInfo() },
            {OutboundProtocol.shadowsocks, new ShadowScoksVerifyInfo() },
            {OutboundProtocol.socks, new SocksVerifyInfo() }
        };
        public IEnumerable<OutboundProtocol> ProxyProtocolValues { get; private set; } = Enum.GetValues(typeof(OutboundProtocol)).Cast<OutboundProtocol>();
        public IEnumerable<TransportType> TransportProtocolValues {get; private set; } = Enum.GetValues<TransportType>().Cast<TransportType>();
        private TransportType transportProtocolSelectedValue;
        public TransportType TransportProtocolSelectedValue
        {
            get => transportProtocolSelectedValue;
            set
            {
                transportProtocolSelectedValue = value;
                OnpropertyChannged(nameof(TransportProtocolSelectedValue));
            }
        }

        public IEnumerable<TransportSecurity> SecurityValues { get; private set; } = Enum.GetValues<TransportSecurity>().Cast<TransportSecurity>();
        private TransportSecurity securitySelectedValue;
        public TransportSecurity SecuritySelectedValue
        {
            get => securitySelectedValue;
            set
            {
                securitySelectedValue = value;
                OnpropertyChannged(nameof(SecuritySelectedValue));
            }
        }
        public UserControl? ProxyUserSettingView
        {
            get => VerifyInfoView[selectedProtocol];
        }
        private OutboundProtocol selectedProtocol;
        public OutboundProtocol SelectedProtocol
        {
            get => selectedProtocol;
            set
            {
                selectedProtocol = value;
                OnSelectedProtocolChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnpropertyChannged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void OnSelectedProtocolChanged()
        {
            OnpropertyChannged(nameof(SelectedProtocol));
            OnpropertyChannged(nameof(ProxyUserSettingView));
        }
    }
}

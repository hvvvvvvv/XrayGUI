﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using CommunityToolkit;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;

namespace NetProxyController.ViewModle
{
    internal class SettingWindowViewModle : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public bool HasErrors => _Errors.Count > 0;
        public event PropertyChangedEventHandler? PropertyChanged;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        private Dictionary<string, List<string>> _Errors;
        private Modle.MainConfigration _configration;
        private int httpPort;
        public string HttpPort
        {
            get => httpPort.ToString();
            set
            {
                if(ValidationAndConvertPortNumber(nameof(HttpPort),value,out int _value))
                {
                    httpPort = _value;
                }
            }
        }
        private int socksPort;
        public string SocksPort
        {
            get => socksPort.ToString();
            set
            {
                if(ValidationAndConvertPortNumber(nameof(socksPort),value,out int _value))
                {
                    socksPort = _value;
                }
            }
        }

        private Modle.SystemProtocol proxyProtocol;
        public bool HttpProxyChecked
        {
            get => proxyProtocol == Modle.SystemProtocol.Http;
            set
            {
                if(value == true) 
                    proxyProtocol = Modle.SystemProtocol.Http;
            }
        }
        public bool SocksProxyChecked
        {
            get => proxyProtocol == Modle.SystemProtocol.Socks;
            set
            {
                if (value == true)
                    proxyProtocol = Modle.SystemProtocol.Socks;
            }
        }
        private bool hotKeyEnabled;
        public bool HotKeyEnableChecked
        {
            get => hotKeyEnabled;
            set
            {
                hotKeyEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HotKeyEnableChecked)));
            }
        }
        public bool HotKeyDisableChecked
        {
            get => !hotKeyEnabled;
            set => hotKeyEnabled = !value;
        }
        private string sysProxyByPass;
        public string SysProxyByPass
        {
            get => sysProxyByPass;
            set => sysProxyByPass = value;
        }

        private GlobalHotkey.Hotkey hotKey;
        public GlobalHotkey.Hotkey Hotkey
        {
            get => hotKey;
            set
            {
                hotKey = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(HotkeyStr)));
            }
        }
        public string HotkeyStr
        {
            get => Hotkey.ToString();
        }
        public RelayCommand SaveBtnCmd { get; set; }
        public SettingWindowViewModle(Modle.MainConfigration configration)
        {
            _configration = configration;
            _Errors = new();
            proxyProtocol = _configration.SystemProxySetting.UseProtocol;
            hotKeyEnabled = _configration.HotkeySetting.Enable;
            sysProxyByPass = _configration.SystemProxySetting.ByPassUrl;
            httpPort = _configration.LocalPort.Http;
            socksPort = _configration.LocalPort.Scoks;
            hotKey = _configration.HotkeySetting.Hotkey;
            SaveBtnCmd = new(SaveConfiguration, () => !HasErrors);
        }
        private bool ValidationAndConvertPortNumber(string propertyName, string value,out int convertVaue)
        {
            bool isSucess = false;
            if(int.TryParse(value,out convertVaue))
            {
                isSucess = convertVaue >= 0x1 && convertVaue <= 0xffff;
            }
            if(isSucess)
            {
                _Errors.Remove(propertyName);
            }
            else
            {
                _Errors[propertyName] = new() { "请输入正确的端口号(1-65535)" };
            }
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
            return isSucess;
        }
        public IEnumerable GetErrors(string? propertyName)
        {
            if(string.IsNullOrEmpty(propertyName) || !_Errors.ContainsKey(propertyName))
            {
                return null!;
            }
            return _Errors[propertyName];
        }
        private void SaveConfiguration()
        {
            var needRoadCore = socksPort != _configration.LocalPort.Scoks || httpPort != _configration.LocalPort.Http;
            _configration.LocalPort.Scoks = socksPort;
            _configration.LocalPort.Http = httpPort;
            _configration.SystemProxySetting.UseProtocol = proxyProtocol;
            _configration.SystemProxySetting.ByPassUrl = sysProxyByPass;
            _configration.HotkeySetting.Hotkey = Hotkey;
            if(needRoadCore)
            {
                _configration.xrayHanler.ReLoad();
            }
            _configration.UpdateSetting();
        }
    }
}

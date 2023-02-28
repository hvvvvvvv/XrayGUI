using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsProxy;
using System.Text.Json.Nodes;
using System.IO;
using System.ComponentModel;
using System.Text.Json;
using GlobalHotkey;
using System.Net.Sockets;
using System.Windows.Input;
using System.Drawing;
using Microsoft.Win32;
using System.Windows;
using RunAtStartup;

namespace NetProxyController
{
    public class AppConfigration: INotifyPropertyChanged
    {
        private string _ProxyUrl = default!;
        public string ProxyUrl
        {
            get { return _ProxyUrl; }
            set
            {
                _ProxyUrl = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProxyUrl)));
                SaveToJsonFile();
            }
        }
        public bool IsHotkeyRegEnabled { get; set; }

        private string _Bypass = default!;
        public string Bypass
        {
            get { return _Bypass; }
            set
            {
                _Bypass = value;
                SaveToJsonFile();
            }
        }

        private Hotkey _ProxyHotkey;

        public event Action? HotkeyHappendEvent;
        public Hotkey ProxyHotkey
        {
            get { return _ProxyHotkey; }
            set 
            {
                _ProxyHotkey = value;
                if(IsHotkeyRegEnabled) ExcuteRegisterHotkey();
                SaveToJsonFile();
            }
        }        
        private StartupService StartupService = new("NetProxyController");
        public bool IsAutoStart
        {
            get
            {
                bool res = false;
                try
                {
                    res = StartupService.Check(Environment.ProcessPath!);
                }
                catch(Exception ex)
                {                                       
                    
                }
                return res;
            }
            set
            {
                try
                {
                    if (value)
                    {
                        StartupService.Set(Environment.ProcessPath!);
                    }
                    else
                    {
                        StartupService.Delete();
                    }
                }
                catch(Exception ex)
                {
                    
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAutoStart)));
            }
        }
        public bool IsHotkeyPause { get; set; } = false;
        private bool _IsProxyEnable;
        public bool IsProxyEnable
        {
            get
            {
                return _IsProxyEnable;
            }
            set
            {
                _IsProxyEnable = value;
                if (value) StartSystemPorxy();
                else StopStartSystemPorxy();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsProxyEnable)));
            }
        }
        private readonly string SettingPath;
        public static Icon IconByProxyEnabled = Icon.FromHandle(Resource.ProxyEnable.GetHicon());
        public static Icon IconByProxyDisabled = Icon.FromHandle(Resource.ProxyDisable.GetHicon());
        private ProxyService proxyService = new();
        private GlobalHotkeyRegister hotkeyRegister = new();
        public event PropertyChangedEventHandler? PropertyChanged;

        public AppConfigration(string settingPath)
        {
            SettingPath = settingPath;
            _IsProxyEnable = proxyService.Query().IsProxy;
            try
            {
                var SettingJsonObj = JsonNode.Parse(File.ReadAllText(SettingPath))!.AsObject();
                _ProxyUrl = SettingJsonObj[nameof(ProxyUrl)]!.ToString();
                _Bypass = SettingJsonObj[nameof(Bypass)]!.ToString();
                _ProxyHotkey = new Hotkey((KeyModifier)SettingJsonObj[nameof(ProxyHotkey)]![nameof(ProxyHotkey.KeyModifier)]!.GetValue<int>(),
                    (Key)SettingJsonObj[nameof(ProxyHotkey)]![nameof(ProxyHotkey.Key)]!.GetValue<int>());
                IsHotkeyRegEnabled = SettingJsonObj[nameof(IsHotkeyRegEnabled)]!.GetValue<bool>();
            }
            catch
            {
                LoadDefaultSeting();
            }
            Reload();
        }

        private void StartSystemPorxy()
        {
            proxyService.Server = _ProxyUrl;
            proxyService.Bypass = _Bypass;
            proxyService.Global();
        }
        private void StopStartSystemPorxy() => proxyService.Direct();
        private void LoadDefaultSeting()
        {
            var proxyStatus = proxyService.Query();
            _ProxyHotkey = new Hotkey(KeyModifier.Alt, Key.F);
            _ProxyUrl = proxyStatus.ProxyServer ?? string.Empty;
            _Bypass = proxyStatus.ProxyBypass ?? string.Empty;
            IsHotkeyRegEnabled = true;
            SaveToJsonFile();
        }
        private void ExcuteRegisterHotkey()
        {
            hotkeyRegister.RemoveAll();
            if (_ProxyHotkey.Key != Key.None)
            {
                hotkeyRegister.Add(_ProxyHotkey, () =>
                {
                    if (!IsHotkeyPause)
                    {
                        HotkeyHappendEvent?.Invoke();
                    }
                });
            }
        }
        private void SaveToJsonFile()
        {
            var SettingJsonObj = new JsonObject()
            {
                [nameof(ProxyUrl)] = ProxyUrl,
                [nameof(Bypass)] = Bypass,
                [nameof(ProxyHotkey)] = new JsonObject()
                {
                    [nameof(ProxyHotkey.Key)] = Convert.ToInt32(ProxyHotkey.Key),
                    [nameof(ProxyHotkey.KeyModifier)] = Convert.ToInt32(ProxyHotkey.KeyModifier)
                },
                [nameof(IsHotkeyRegEnabled)] = IsHotkeyRegEnabled
            };
            FileStream fs = new FileStream(SettingPath, FileMode.OpenOrCreate, FileAccess.Write);
            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            var buff = Encoding.UTF8.GetBytes(SettingJsonObj.ToJsonString(jsonSerializerOptions));
            fs.Write(buff, 0, buff.Length);
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }

        public void Reload()
        {
            if(_IsProxyEnable) StartSystemPorxy();
            if(IsHotkeyRegEnabled) ExcuteRegisterHotkey();
        }

        private static GlobalHotkeyRegister globalHotkeyRegister = new();

    }
}

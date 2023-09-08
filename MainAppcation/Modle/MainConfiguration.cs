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
using XrayCoreConfigModle;
using static System.Windows.Forms.Design.AxImporter;

namespace XrayGUI.Modle
{
    internal class MainConfigration
    {

        private ConfigObject _Config;
        public HotkeySettingObject HotkeySetting => _Config.HotkeySetting;
        public SystemProxySettingObject SystemProxySetting => _Config.SystemProxySetting;
        public XrayCoreSettingObject XrayCoreSetting => _Config.XrayCoreSetting;
        public LocalPortObect LocalPort => _Config.localPort;
        public bool ProxyEnable
        {
            get => _Config.ProxyEnable;
            set => _Config.ProxyEnable = value;
        }
        public ProxyModes ProxyMode
        {
            get => _Config.ProxyMode;
            set => _Config.ProxyMode = value;
        }
        public bool EnableAutostart
        {
            get => _Config.EnableAutostart;
            set => _Config.EnableAutostart = value;
        }
        public readonly Handler.HotkeyHandler hotkeyHandler;
        public readonly Handler.SystemProxyHanler systemProyHanler;
        public readonly Handler.XrayHanler xrayHanler;
        public readonly Handler.AutoStartHandler autoStartHandler;
        public MainConfigration()
        {
            _Config = ReadConfig();
            hotkeyHandler = new();
            systemProyHanler = new(SystemProxySetting, LocalPort);
            autoStartHandler = new();
            //XrayCoreSetting.OutBoundServers = JsonHandler.JsonDeserializeFromFile<MainConfiguration>(
            //    @"C:\Users\万超\Desktop\小飞机\xrayDeamon\Xray\config.json").outbounds;
            UpdateSetting();
            xrayHanler.CoreStart();
        }

        private static ConfigObject ReadConfig()
        {
            ConfigObject? config = default;
            try
            {
                config = JsonHandler.JsonDeserializeFromFile<ConfigObject>(Global.AppConfigPath);
            }
            catch { }
            if (config != default)
            {
                return config;
            }
            else
            {
                return new ConfigObject();
            }
        }

        public void UpdateSetting()
        {
            if (autoStartHandler.Enable != EnableAutostart)
            {
                autoStartHandler.Enable = EnableAutostart;
            }
            
            Save();
        }

        public void Save()
        {
            JsonSerializerOptions options_ = new()
            {
                WriteIndented = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };
            string jsonContent = JsonSerializer.Serialize(_Config, options_);
            string path = Path.GetDirectoryName(Global.AppConfigPath)!;
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(Global.AppConfigPath, jsonContent);
        }
    }
}

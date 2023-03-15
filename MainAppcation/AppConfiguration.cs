﻿using System;
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
using NetProxyController.Modle;
using static System.Windows.Forms.Design.AxImporter;

namespace NetProxyController
{
    internal class AppConfigration
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
        public readonly Handler.SystemProyHanler systemProyHanler;
        public readonly Handler.XrayHanler xrayHanler;
        public readonly Handler.AutoStartHandler autoStartHandler;
        public AppConfigration()
        {
            _Config = new();
            hotkeyHandler = new(HotkeySetting);
            systemProyHanler = new(SystemProxySetting, LocalPort);
            autoStartHandler = new();
            xrayHanler = new(JsonHandler.JsonDeserializeFromFile<MainConfiguration>(@"C:\Users\万超\Desktop\小飞机\xrayDeamon\Xray\config.json")!,
                LocalPort);
            Init();
        }

        private void Init()
        {
            xrayHanler.CoreStart();
            UpdateSetting();
            
        }

        public void UpdateSetting()
        {
            if(autoStartHandler.Enable != EnableAutostart)
            {
                autoStartHandler.Enable = EnableAutostart;
            }
            if (ProxyEnable) systemProyHanler.OnProxy();
            else systemProyHanler.OffProxy();
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
            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            File.WriteAllText(Global.AppConfigPath, jsonContent);
        }
    }
}

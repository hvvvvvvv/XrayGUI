using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using XrayCoreConfigModle;

namespace NetProxyController.Modle
{
    internal class ConfigObject
    {
        public HotkeySettingObject HotkeySetting { get; set; } = new();       
        public LocalPortObect localPort { get; set; } = new();
        public bool ProxyEnable { get; set; } = false;
        public ProxyModes ProxyMode { get; set; } = ProxyModes.System;
        public bool EnableAutostart { get; set; } = false;
        public SystemProxySettingObject SystemProxySetting { get; set; } = new();
        public XrayCoreSettingObject XrayCoreSetting { get; set; } = new();
        public void Save()
        {
            lock (this)
            {
                JsonSerializerOptions options_ = new()
                {
                    WriteIndented = true,
                    DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
                };
                string jsonContent = JsonSerializer.Serialize(this, options_);
                string path = Path.GetDirectoryName(Global.AppConfigPath)!;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                File.WriteAllText(Global.AppConfigPath, jsonContent);
            }
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
        private static ConfigObject? _instance;
        public static ConfigObject Instance  => _instance ??= ReadConfig();
    }
    internal class LocalPortObect
    {
        public int Http { get; set; } = 10880;
        public int Scoks { get; set; } = 10881;
    }
}

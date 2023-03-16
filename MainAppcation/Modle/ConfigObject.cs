using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
    internal class LocalPortObect
    {
        public int Http { get; set; } = 10880;
        public int Scoks { get; set; } = 10881;
    }
}

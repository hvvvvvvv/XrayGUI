using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayGUI.Modle
{
    internal class SystemProxySettingObject
    {
        public SystemProtocol UseProtocol { get; set; } = SystemProtocol.Http;
        public string ByPassUrl { get; set; } = string.Empty;
    }
    
}


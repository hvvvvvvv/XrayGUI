using XrayGUI.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace XrayGUI.Handler
{
    internal class AutoStartHandler: RunAtStartup.StartupService
    {
        public AutoStartHandler() : base(Global.AutoStartItemName)
        {
            
        }
        public void LoadConfig()
        {
            if(ConfigObject.Instance.ProxyEnable)
            {
                Set(Environment.ProcessPath!);
            }
            else
            {
                Delete();
            }
        }
        public bool Enable
        {
            get => Check(Environment.ProcessPath!);
            set
            {
                if (value)
                    Set(Environment.ProcessPath!);
                else
                    Delete();
            }
        }
        private static AutoStartHandler? _instance;
        public static AutoStartHandler Instance => _instance ??= new AutoStartHandler();

    }
}

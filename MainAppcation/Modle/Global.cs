using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara.PInvoke;

namespace NetProxyController.Modle
{
    internal static class Global
    {
        public const string AutoStartItemName = "NetProxyController";
        public const string LoopBcakAddress = "127.0.0.1";
        public static readonly string AppConfigPath = AppContext.BaseDirectory + @"Config\AppCofing.json";
        public static readonly string XrayCoreConfigPath = AppContext.BaseDirectory + @"Config\XrayCoreConfig.json";
        public static readonly string XrayCoreApplictionPath = AppContext.BaseDirectory + @"bin\xray.exe";
        public static readonly Kernel32.SafeHJOB ProcessJobs = Kernel32.CreateJobObject();
    }
}

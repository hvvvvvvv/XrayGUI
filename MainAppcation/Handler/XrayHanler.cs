using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle;
using Windows.Gaming.Preview.GamesEnumeration;

namespace NetProxyController.Handler
{
    internal class XrayHanler
    {
        public MainConfiguration XrayConfig { get; set; }
        public bool Isrunning { get; private set; }
        private Process _coreProcess { get; set; }
        public XrayHanler(MainConfiguration xrayConfig)
        {
            XrayConfig = xrayConfig;
            _coreProcess = new Process();
            LoadConfig();
        }
        private void LoadConfig()
        {
            JsonHandler.JsonSerializeToFile(XrayConfig, Modle.Global.XrayCoreConfigPath);
        }
        public void CoreStart()
        {
            Isrunning = true;
            if (!_coreProcess.HasExited)
            {
                return;
            }
            _coreProcess.StartInfo = new()
            {
                FileName = Modle.Global.XrayCoreApplictionPath,
                Arguments = $"-t {Modle.Global.XrayCoreConfigPath}",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };
            _coreProcess.Start();
            
            if(!_coreProcess.WaitForExit(2000))
            {
                throw new Exception($"XrayCore进程启动失败：{_coreProcess.StandardError.ReadToEnd()}");
            }            
        }

        public void RedLoad()
        {
            _coreProcess.Close();
            _coreProcess.Dispose();
            LoadConfig();
            if(Isrunning)
            {
                CoreStart();
            }
        }

        public void Restart()
        {
            CoreStop();
            CoreStart();
        }

        public void CoreStop()
        {
            if (!_coreProcess.HasExited)
            {
                _coreProcess.Kill();
            }
            Isrunning = false;
        }

    }
}

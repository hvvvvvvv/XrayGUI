using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle;

namespace NetProxyController.Handler
{
    internal class XrayHanler
    {
        public MainConfiguration XrayConfig { get; set; }
        public bool Isrunning { get; private set; }
        private string _coreAppPath;
        private Process _coreProcess { get; set; }
        public XrayHanler(MainConfiguration xrayConfig, string appPath)
        {
            _coreProcess = new Process();
            XrayConfig = xrayConfig;
            _coreAppPath = appPath;
            LoadConfig();
        }
        private void LoadConfig()
        {
            if(File.Exists(_coreAppPath))
            {
                throw new FileNotFoundException();
            }
            string configPath = Path.Combine(Path.GetDirectoryName(_coreAppPath)!, "config.json");
            JsonHandler.JsonSerializeToFile(XrayConfig, configPath);
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
                FileName = _coreAppPath,
                WorkingDirectory = Path.GetDirectoryName(_coreAppPath),
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

        public void ReadLoad()
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

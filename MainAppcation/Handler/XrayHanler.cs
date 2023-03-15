using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle;
using Windows.Gaming.Preview.GamesEnumeration;
using NetProxyController.Modle;
using XrayCoreConfigModle.Inbound;
using Windows.System.Profile;

namespace NetProxyController.Handler
{
    internal class XrayHanler
    {
        public MainConfiguration XrayConfig { get; set; }
        public bool Isrunning { get; private set; } = false;
        private Process _coreProcess;
        private LocalPortObect _LocalPort;
        private ProcessStartInfo _CoreProcessStartInfo
        {
            get => new()
            {
                FileName = Global.XrayCoreApplictionPath,
                Arguments = $"-c {Global.XrayCoreConfigPath}",
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding = Encoding.UTF8,
            };
        }
            
        public XrayHanler(MainConfiguration xrayConfig,LocalPortObect localPort)
        {
            XrayConfig = xrayConfig;
            _coreProcess = new Process();
            _LocalPort = localPort;
            LoadConfig();
        }
        private void LoadConfig()
        {
            _coreProcess.Exited += OnCoreProcessAccidentExted;
            XrayConfig.inbounds = new List<InboundServerItemObject>
            {
                new()
                {
                    listen = Global.LoopBcakAddress,
                    protocol = "http",
                    port = _LocalPort.Http,
                    tag = "http",
                    settings = new HttpConfigurationObject()
                    {
                        allowTransparent = true
                    }                    
                },
                new()
                {
                    listen = Global.LoopBcakAddress,
                    protocol = "socks",
                    port = _LocalPort.Scoks,
                    tag = "scoks",
                    settings =  new SocksConfigurationObject()
                    {
                        auth = "none",
                        udp = true,
                    },
                    sniffing = new()
                    {
                        enabled = true,
                        destOverride = new(){ "http", "tls" },
                    }
                }
            };
            File.WriteAllText(Global.XrayCoreConfigPath, JsonHandler.JsonSerializeToString(XrayConfig));
        }

        private void OnCoreProcessAccidentExted(object? sender, EventArgs e)
        {
            _coreProcess.Refresh();
            _coreProcess.EnableRaisingEvents = false;
            _coreProcess.StartInfo = _CoreProcessStartInfo;
            _coreProcess.Start();
            
            if(_coreProcess.WaitForExit(5000))
            {
                throw new Exception($"检测到XrayCore进程意外终止，并尝试重新启动失败{_coreProcess.StandardError.ReadToEnd()}");
            }
            else
            {
                _coreProcess.EnableRaisingEvents = true;
                _coreProcess.Exited += OnCoreProcessAccidentExted;
            }

        }

        public void CoreStart()
        {
            if (Isrunning) return;

            _coreProcess.Refresh();
            _coreProcess.EnableRaisingEvents = true;
            _coreProcess.Exited += OnCoreProcessAccidentExted;
            _coreProcess.StartInfo = _CoreProcessStartInfo;
            _coreProcess.Start();
            
        }

        public void ReLoad()
        {
            var _isRunning = Isrunning;
            CoreStop();            
            _coreProcess.Close();
            _coreProcess.Dispose();
            _coreProcess = new();
            LoadConfig();
            if(_isRunning)
            {
                CoreStart();
            }
        }

        public void CoreStop()
        {

            if(Isrunning)
            {
                _coreProcess.EnableRaisingEvents = false;
                try
                {
                    _coreProcess.Kill();
                }
                catch(Exception ex)
                {
                    throw new Exception($"尝试终止XrayCore进程失败：{ex.Message}");
                }
                _coreProcess.EnableRaisingEvents = true;
                Isrunning = false;
            }
            
        }

    }
}

﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using Vanara.PInvoke;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrayCoreConfigModle;
using Windows.Gaming.Preview.GamesEnumeration;
using NetProxyController.Modle;
using XrayCoreConfigModle.Inbound;
using XrayCoreConfigModle.Routing;
using Windows.System.Profile;
using HandyControl.Controls;
using HandyControl.Data;
using NetProxyController.Modle.Server;

namespace NetProxyController.Handler
{
    internal class XrayHanler
    {
        public XrayCoreSettingObject XrayConfig { get; set; }
        public bool Isrunning { get; private set; } = false;
        private Process _coreProcess;
        private LocalPortObect _LocalPort;
        private bool _ExitedEventPause = false;
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
            
        public XrayHanler()
        {
            XrayConfig = ConfigObject.Instance.XrayCoreSetting;
            _LocalPort = ConfigObject.Instance.localPort;
            _coreProcess = new()
            {
                EnableRaisingEvents = true
            };
            _coreProcess.Exited += OnCoreProcessAccidentExted;
            LoadConfig();

            if (!File.Exists(Global.XrayCoreApplictionPath))
            {
                if (!Directory.Exists(Path.GetDirectoryName(Global.XrayCoreApplictionPath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(Global.XrayCoreApplictionPath)!);
                }
                throw new Exception($"未找到XrayCore程序，请将{Path.GetFileName(Global.XrayCoreApplictionPath)}文件放在{Path.GetDirectoryName(Global.XrayCoreApplictionPath)}目录下");
            }
        }
        private void LoadConfig()
        {
            var _inbounds = new List<InboundServerItemObject>
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
            var _outbounds = new List<OutboundServerItemObject>();
            foreach ( var item in ServerItem.GetItemsFromDataBase())
            {
                if(item.Index == XrayConfig.DefaultOutboundServerIndex)
                {
                    _outbounds.Insert(0, item.ToOutboundServerItemObject());
                }
                else
                {
                    _outbounds.Add(item.ToOutboundServerItemObject());
                }
            }
            _outbounds.Add(new OutboundServerItemObject()
            {
                protocol = "Freedom",
                tag = Global.XrayDirectTag,
            });
            var routing = new RoutingObject()
            {
                domainMatcher = XrayConfig.RouteMatchSetting.domainMatcher,
                domainStrategy = XrayConfig.RouteMatchSetting.domainStrategy,
            };

            var mainConfig = new MainConfiguration()
            {
                inbounds = _inbounds,
                outbounds = _outbounds,
                routing = routing
            };
           JsonHandler.JsonSerializeToFile(mainConfig, Global.XrayCoreConfigPath);       
            
        }

        private void OnCoreProcessAccidentExted(object? sender, EventArgs e)
        {
            if(_ExitedEventPause) return;

            _ExitedEventPause = true;
            _coreProcess.Close();           
            _coreProcess.StartInfo = _CoreProcessStartInfo;
            _coreProcess.Start();
            Kernel32.AssignProcessToJobObject(Global.ProcessJobs, _coreProcess);

            if (_coreProcess.WaitForExit(5000))
            {
                throw new Exception($"检测到XrayCore进程意外终止，并尝试重新启动失败{_coreProcess.StandardOutput.ReadToEnd()}");
            }
            else
            {
                _ExitedEventPause = false;
            }

        }

        public void CoreStart()
        {
            if (Isrunning) return;

            _coreProcess.Close();   
            _coreProcess.StartInfo = _CoreProcessStartInfo;
            _coreProcess.Start();
            _ExitedEventPause = false;
            Isrunning = true;
            Kernel32.AssignProcessToJobObject(Global.ProcessJobs, _coreProcess);
        }

        public void ReLoad()
        {
            var _isRunning = Isrunning;
            CoreStop();
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
                _ExitedEventPause = true;
                try
                {
                    _coreProcess.Kill();
                    _coreProcess.WaitForExit();
                }
                catch(Exception ex)
                {
                    throw new Exception($"尝试终止XrayCore进程失败：{ex.Message}");
                }
                Isrunning = false;
            }
            
        }

    }
}

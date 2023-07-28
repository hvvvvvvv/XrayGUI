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
using XrayCoreConfigModle.OutBound;
using NetProxyController.ViewModle;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;

namespace NetProxyController.Handler
{
    internal class XrayHanler
    {
        private static XrayHanler? _instance;
        public static XrayHanler Instance => _instance ??= new XrayHanler();
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
                StandardErrorEncoding = Encoding.UTF8              
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
            _coreProcess.OutputDataReceived += (_, e) => Debug.WriteLine(e.Data);            
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
            LoadConfig(ServerItem.ServerItemsDataList);   
        }
        private void LoadConfig(IEnumerable<ServerItem> serverItems)
        {
            var _inbounds = new List<InboundServerItemObject>
            {
                new()
                {
                    listen = Global.LoopBcakAddress,
                    protocol = "http",
                    port = _LocalPort.Http,
                    tag = "http",
                    settings = new XrayCoreConfigModle.Inbound.HttpConfigurationObject()
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
                    settings =  new XrayCoreConfigModle.Inbound.SocksConfigurationObject()
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
            var _outbounds = new List<OutboundServerItemObject>()
            {
                 new()
                 {
                     protocol = "freedom",
                     settings = new FreedomConfigurationObject()
                 }
            };
            foreach (var item in serverItems)
            {
                if (item.Index == XrayConfig.DefaultOutboundServerIndex)
                {
                    _outbounds.Insert(0, item.ToOutboundServerItemObject());
                }
                else if (item.IsActivated)
                {
                    _outbounds.Add(item.ToOutboundServerItemObject());
                }
            }
            var routing = new RoutingObject()
            {
                domainMatcher = XrayConfig.RouteMatchSetting.domainMatcher,
                domainStrategy = XrayConfig.RouteMatchSetting.domainStrategy,
            };

            var mainConfig = new MainConfiguration()
            {
                log = new LogObject
                {
                    loglevel = "info"
                },
                inbounds = _inbounds,
                outbounds = _outbounds,
                routing = routing
            };
            JsonHandler.JsonSerializeToFile(mainConfig, Global.XrayCoreConfigPath);

        }
        public void LoadTestConfig(List<ServerItemViewModle> serverVm)
        {
            var _outbounds = (from ServerItemViewModle item in serverVm select item.Server.ToOutboundServerItemObject()).ToList();
            _outbounds.Insert(0, new OutboundServerItemObject()
            {
                protocol = "blackhole"
            });
            var _inbounds = new List<InboundServerItemObject>();
            var routeRlues = new List<RuleObject>();
            var sockets = new List<Socket>();
            foreach(var item in serverVm)
            {
                Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Bind(new IPEndPoint(IPAddress.Loopback, 0));
                int port = ((IPEndPoint)socket.LocalEndPoint!).Port;
                item.TestProxyPort = port;
                sockets.Add(socket);
                _inbounds.Add(new InboundServerItemObject()
                {
                    protocol = "http",
                    listen = Global.LoopBcakAddress,
                    port = port,
                    settings = new XrayCoreConfigModle.Inbound.HttpConfigurationObject()
                    {
                        allowTransparent = true
                    },
                    tag = item.Server.Index.ToString()
                });
                routeRlues.Add(new RuleObject()
                {
                    inboundTag = new()
                    {
                        item.Server.Index.ToString()
                    },
                    outboundTag = item.Server.Index.ToString()
                });
            }
            if(Isrunning) CoreStop();
            JsonHandler.JsonSerializeToFile(new MainConfiguration()
            {
                inbounds = _inbounds,
                outbounds = _outbounds,
                routing = new RoutingObject()
                {
                    rules = routeRlues
                }
            },
            Global.XrayCoreConfigPath);
            sockets.ForEach(i => i.Dispose());
            CoreStart();
        }

        private void OnCoreProcessAccidentExted(object? sender, EventArgs e)
        {
            if(_ExitedEventPause) return;

            _ExitedEventPause = true;
            _coreProcess.CancelOutputRead();
            _coreProcess.Close();
            _coreProcess.StartInfo = _CoreProcessStartInfo;
            _coreProcess.Start();            
            _coreProcess.BeginOutputReadLine();
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
            _coreProcess.BeginOutputReadLine();
            _ExitedEventPause = false;
            Isrunning = true;
            Kernel32.AssignProcessToJobObject(Global.ProcessJobs, _coreProcess);
        }
        public void ReloadConfig()
        {
            var _isRunning = Isrunning;
            CoreStop();
            LoadConfig();
            if (_isRunning)
            {
                CoreStart();
            }
        }

        public void CoreStop()
        {

            if(Isrunning)
            {
                _ExitedEventPause = true;
                _coreProcess.CancelOutputRead();
                try
                {
                    _coreProcess.Kill();
                    if(!_coreProcess.WaitForExit(10000))
                    {
                        throw new Exception("等待进程退出超时");
                    }
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

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows;
using static Vanara.PInvoke.Kernel32;
using NetProxyController.View;
using NetProxyController.Modle;
using System.Threading;
using NetProxyController.Tools;

namespace NetProxyController
{
    public partial class App: Application
	{

        protected override void OnStartup(StartupEventArgs e)
		{
			string Pname = EncodeHelper.GetMD5(Environment.ProcessPath!);
            var ProgramStarted = new EventWaitHandle(false, EventResetMode.AutoReset, Pname, out bool bCreatedNew);
            if (!bCreatedNew)
            {
                ProgramStarted.Set();
                Current.Shutdown();
                Environment.Exit(0);
                return;
            }
            SetProcessJobs();
            NotifyIcon taskBar = new();
            taskBar.Show();
            base.OnStartup(e);
        }

        static void SetProcessJobs()
		{
			var setClass = JOBOBJECTINFOCLASS.JobObjectExtendedLimitInformation;
			var jobInfo = new JOBOBJECT_EXTENDED_LIMIT_INFORMATION()
			{
				BasicLimitInformation = new()
				{
					LimitFlags = JOBOBJECT_LIMIT_FLAGS.JOB_OBJECT_LIMIT_KILL_ON_JOB_CLOSE
				}
			};
			SetInformationJobObject(Modle.Global.ProcessJobs, setClass, jobInfo);
		}
	}
}
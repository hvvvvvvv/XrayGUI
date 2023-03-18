﻿using NetProxyController.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;

namespace NetProxyController.Handler
{
    internal class AutoStartHandler: RunAtStartup.StartupService
    {
        public AutoStartHandler():base(Global.AutoStartItemName)
        {
            
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
        

    }
}

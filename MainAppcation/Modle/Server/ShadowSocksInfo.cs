﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using XrayCoreConfigModle.OutBound;

namespace NetProxyController.Modle.Server
{
    internal class ShadowSocksInfo: OutBoundConfiguration
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string? Email { get; set; }
        public SS_Ecrept Method { get; set; }
        public string? Password { get; set; }
        public bool Uot { get; set; }
        public int? Level { get; set; }

        public override OutboundConfigurationObject ToOutboundConfigurationObject(string addr, int port)
        {
            return new ShadowsocksConfigurationObject()
            {
                servers = new()
                {
                    new ShadowsocksServerObject()
                    {
                        address = addr,
                        port = port,
                        password = Password,
                        uot = Uot,
                        email = Email,
                        level = Level,
                        method = Method.GetStringValue()
                    }
                }
            };
        }
    }
}

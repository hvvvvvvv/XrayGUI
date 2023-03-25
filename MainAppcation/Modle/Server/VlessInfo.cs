﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using XrayCoreConfigModle.OutBound;

namespace NetProxyController.Modle.Server
{

    internal class VlessInfo: OutBoundConfiguration
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string? Id { get; set; }
        public XtlsFlow Flow { get; set; }
        public int? Level { get; set; }

        public override OutboundConfigurationObject ToOutboundConfigurationObject(string addr, int port)
        {
            return new VlessConfigurationObject()
            {
                vnext = new()
                {
                    new VlessServerObject
                    {
                        address = addr,
                        port = port,
                        users = new()
                        {
                            new VlessUserObject()
                            {
                                id = Id,
                                flow = Flow.GetStringValue(),
                                level = Level
                            }
                        }
                    }
                }
            };
        }
    }
}
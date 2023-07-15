﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Modle.Subscription
{
    internal class VmessbyJson
    {
        /// <summary>
        ///
        /// </summary>
        public string v { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public string ps { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public string add { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public int port { get; set; } = 0;

        /// <summary>
        ///
        /// </summary>
        public string id { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public int aid { get; set; } = 0;

        /// <summary>
        ///
        /// </summary>
        public string scy { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public string net { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public string type { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public string host { get; set; } = string.Empty;

        /// <summary>
        ///
        /// </summary>
        public string path { get; set; } = string.Empty;

        /// <summary>
        /// TLS
        /// </summary>
        public string tls { get; set; } = string.Empty;

        /// <summary>
        /// TLS SNI
        /// </summary>
        public string sni { get; set; } = string.Empty;

        /// <summary>
        /// TLS alpn
        /// </summary>
        public string alpn { get; set; } = string.Empty;

        /// <summary>
        /// TLS fingerprint
        /// </summary>
        public string fp { get; set; } = string.Empty;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NetProxyController.Modle;
using SQLite;
using XrayCoreConfigModle.ProtocolSetting;
namespace NetProxyController.Modle.Server
{
    internal class TlsInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string ServerName { get; set; } = string.Empty;
        public TlsFingerPrint FingerPrint { get; set; }
        public bool AllowInsecure { get; set; }
        public TLSObject ToTLSObject()
        {
            return new()
            {
                serverName = ServerName,
                fingerprint = FingerPrint.ToString(),
                allowInsecure = AllowInsecure,
            };
        }
    }
    internal class RealityInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string ServerName { get; set; } = string.Empty;
        public TlsFingerPrint FingerPrint { get; set; }
        public string ShortId { get; set; } = string.Empty;
        public string PublicKey { get; set; } = string.Empty;
        public string SpiderX { get; set; } = string.Empty;
        public RealityObject ToRealityObject()
        {
            return new()
            {
                serverName = ServerName,
                fingerprint = FingerPrint.ToString(),
                shortId = ShortId,
                publicKey = PublicKey,
                spiderX = SpiderX,
            };
        }
    }
}

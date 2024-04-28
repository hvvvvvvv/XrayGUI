using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace XrayGUI.Modle
{
    [Table("OutboundServers")]
    public class OutboundServerItem : DataBaseItem<OutboundServerItem>
    {
        public string? Remarks { get; set; }
        public string? Address { get; set; }
        public uint Port { get; set; }
        public string? Protocol { get; set; }
        public long UpdatedTime { get; set; }
        public Guid GroupId { get; set; }

        #region Common Options
        public bool TcpFastOpen { get; set; }
        public string? PacketEncoding { get; set; }

        #region UdpOverTcp
        public bool? UdpOuverTcpEnbled { get; set; }
        public byte? UotVision { get; set; }
        #endregion

        #region Tls
        public bool? TlsEnbled { get; set; }
        public string? ServerName { get; set; }
        public string? Alpn { get; set; }
        public bool? AllowInsecure { get; set; }
        public bool? UtlsEnbled { get; set; }
        public string? UtlsFingerPrint { get; set; }
        public bool? RealityEnbled { get; set; }
        public string? ShortId { get; set; }
        public string? PublicKey { get; set; }

        #endregion

        #region V2RayTransport
        public bool V2RayTransportEnbled { get; set; }
        public string? TransportType { get; set; }
        public string? TransportHost { get; set; }
        public string? TransportPath { get; set; }
        #endregion

        #endregion

        #region Socks
        public string? SocksUsername { get; set; }
        public string? SocksPassword { get; set; }
        public string? SocksVision { get; set; }       
        #endregion

        #region Shadowsocks
        public string? SSMethod { get; set; }
        public string? SSPassword { get; set; }
        public string? SSPlugin { get; set; }
        public string? SSPluginOpts { get; set; }
        #endregion

        #region VMESS
        public string? VmessId { get; set; }
        public int VmessAlterId { get; set; }
        public string? VmessSecurity { get; set; }

        #endregion

        #region Trojan
        public string? TrojanPassword { get; set; }
        #endregion

        #region VLESS
        public string? VlessId { get; set; }
        public string? VlessFlow { get; set; }
        #endregion
    }
}

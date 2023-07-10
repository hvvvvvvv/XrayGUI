using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vanara.PInvoke;
using SQLite;
using System.Collections.ObjectModel;

namespace NetProxyController.Modle
{
    internal static class Global
    {
        public const string AutoStartItemName = "NetProxyController";
        public const string LoopBcakAddress = "127.0.0.1";
        public const string XrayDirectTag = "direct";
        public static readonly string AppConfigPath = AppContext.BaseDirectory + @"Config\AppCofing.json";
        public static readonly string XrayCoreConfigPath = AppContext.BaseDirectory + @"Config\XrayCoreConfig.json";
        public static readonly string XrayCoreApplictionPath = AppContext.BaseDirectory + @"bin\xray.exe";
        public static readonly string DbPath = AppContext.BaseDirectory + @"config\ServerConfig.db";
        public static readonly Kernel32.SafeHJOB ProcessJobs = Kernel32.CreateJobObject();
        public static readonly SQLiteConnection DBService = new(DbPath);
        public static readonly ReadOnlyCollection<FeignType> TcpFeignItems = new(new List<FeignType>()
        {
            FeignType.none,
            FeignType.http
        });
        public static readonly ReadOnlyCollection<FeignType> KcpOrQuicFeignItems = new(new List<FeignType>()
        {
            FeignType.none,
            FeignType.srtp,
            FeignType.utp,
            FeignType.wechat_video,
            FeignType.dtls,
            FeignType.wireguard
        });
        public static readonly ReadOnlyCollection<SecurityMode> QuicSecurityModeItems = new(new List<SecurityMode>()
        {
            SecurityMode.None,
            SecurityMode.Aes_128_gcm,
            SecurityMode.Chacha20_poly1305
        });

    }
}

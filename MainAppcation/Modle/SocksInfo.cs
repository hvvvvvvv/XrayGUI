using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NetProxyController.Modle
{
    internal class SocksInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string? User { get; set; }
        public string? Pass { get; set; }
        public int? Level { get; set; }
    }
}

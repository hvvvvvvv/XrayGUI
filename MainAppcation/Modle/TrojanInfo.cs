using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NetProxyController.Modle
{
    internal class TrojanInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int? Level { get; set; }
    }
}

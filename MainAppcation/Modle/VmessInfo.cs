using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NetProxyController.Modle
{
    internal class VmessInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string Id { get; set; } = string.Empty;
        public int AlterId { get; set; } = 0;
        public VmessSecurity Security { get; set; } = VmessSecurity.Auto;
        public int? Level { get; set; }
    }
}

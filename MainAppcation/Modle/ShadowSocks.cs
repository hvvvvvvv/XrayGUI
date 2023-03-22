using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NetProxyController.Modle
{
    internal class ShadowSocks
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string? Email { get; set; }
        public SS_Ecrept Method { get; set; }
        public string? Pssword { get; set; }
        public bool Uot { get; set; }
        public int? Level { get; set; }
    }
}

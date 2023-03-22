using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NetProxyController.Modle
{
    
    internal class VlessInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        public string? Id { get; set; }
        public XtlsFlow Flow { get; set; }
        public int? Level { get; set; }
    }
}

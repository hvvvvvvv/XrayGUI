using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace NetProxyController.Modle
{
    internal class StreamInfo
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Index { get; set; }
        TransportType TransportType { get; set; }
        public int TransportIndex { get; set; }

        public TransportSecurity Security { get; set; }

    }
}

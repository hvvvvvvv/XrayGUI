using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Inbound
{
    public class SocksInboundOptions : ListenOptions
    {
        [JsonPropertyName("users")]
        public List<AuthUserOptions>? Users { get; set; }
    }
}

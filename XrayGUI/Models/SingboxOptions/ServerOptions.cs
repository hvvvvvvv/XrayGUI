using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions
{
    public class ServerOptions
    {
        [JsonPropertyName("server")]
        public string? Server { get; set; }

        [JsonPropertyName("server_port")]
        public ushort? ServerPort { get; set; }
    }
}

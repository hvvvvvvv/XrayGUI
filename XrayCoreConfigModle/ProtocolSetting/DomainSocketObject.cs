using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle.ProtocolSetting
{
    public class DomainSocketObject 
    {
        /// <summary>
        /// 一个合法的文件路径。
        /// </summary>
        public string? path { get; set; }
        /// <summary>
        /// 是否为 abstract domain socket，默认值 false。
        /// </summary>
        [JsonPropertyName("abstract")]
        public bool? abstract_ { get; set; }
        /// <summary>
        /// abstract domain socket 是否带 padding，默认值 false。
        /// </summary>
        public bool? padding { get; set; }
    }
}

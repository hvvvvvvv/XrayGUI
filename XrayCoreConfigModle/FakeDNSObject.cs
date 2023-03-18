using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace XrayCoreConfigModle
{
    [JsonConverter(typeof(JsonConverters.FakeDNSObjectConverter))]
    public class FakeDNSObject: List<FakeDnsIpPoolObject>
    {
    }
    public class FakeDnsIpPoolObject
    {
        /// <summary>
        /// FakeDNS 将使用此选项指定的CIDR IP 块分配地址。
        /// </summary>
        public string? ipPool { get; set; }
        /// <summary>
        /// 指定 FakeDNS 储存的 域名-IP 映射的最大数目。当映射数超过此值后，会按照 LRU 规则淘汰映射。默认为 65535。
        ///  poolSize 必须小于或等于 ipPool 对应的地址总数。
        /// </summary>
        public int? poolSize { get; set; }
    }
}


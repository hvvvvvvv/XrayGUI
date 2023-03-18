using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayCoreConfigModle
{
    public class ApiObject 
    {
        /// <summary>
        /// 出站代理标识。
        /// </summary>
        public string? tag { get; set; }
        /// <summary>
        /// 开启的 API 列表
        /// 详情查阅 https://github.com/XTLS/Xray-docs-next/blob/main/docs/config/api.md#%E6%94%AF%E6%8C%81%E7%9A%84-api-%E5%88%97%E8%A1%A8
        /// </summary>
        public List<string>? services { get; set; }
    }
}

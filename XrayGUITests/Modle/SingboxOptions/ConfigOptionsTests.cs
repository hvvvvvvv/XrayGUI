using Microsoft.VisualStudio.TestTools.UnitTesting;
using XrayGUI.Modle.SingboxOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XrayGUI.Modle.SingboxOptions.Tests
{
    [TestClass()]
    public class ConfigOptionsTests
    {
        [TestMethod()]
        public void JsonDeserializeTest()
        {
            string json_ = File.ReadAllText(@"C:\Users\wanchao\Desktop\开发API\sing-box-1.9.0-rc.4\cmd\sing-box\config.json");
            var options = ConfigOptions.JsonDeserialize(json_);
            var jsonStr = options.JsonSerialize();
            Assert.Fail();
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetProxyController.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Handler.Tests
{
    [TestClass()]
    public class SubscribeHandleTests
    {
        [TestMethod()]
        public void ResolveSubFromSubctentTest()
        {
            string input = Tools.EncodeHelper.GetClipboardText();
            var result = SubscribeHandle.ResolveSubFromSubContent(input);
            Assert.AreNotEqual(result.Count, 0);
        }
    }
}
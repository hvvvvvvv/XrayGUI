using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetProxyController.Handler.SubResolve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetProxyController;
using NetProxyController.Tools;
using System.Windows.Forms;
using Windows.ApplicationModel.DataTransfer;

namespace NetProxyController.Handler.SubResolve.Tests
{
    
    [TestClass()]
    public class VmessResolverTests
    {
        [STAThread]
        [TestMethod()]
        public void ResolveTest()
        {
            Console.WriteLine($"resolve text is \n {EncodeHelper.GetClipboardText()}");
            var res = VmessResolver.ResolveByJson(EncodeHelper.GetClipboardText()) ?? VmessResolver.ResolveByJson(EncodeHelper.GetClipboardText());
            Console.WriteLine(res is null);
            //Assert.Fail();
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetProxyController.Handler.SubResolve;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Handler.SubResolve.Tests
{
    [TestClass()]
    public class ShadowSocksResolverTests
    {
        [TestMethod()]
        public void ResolveByUrlType02Test()
        {
            var res = ShadowSocksResolver.ResolveByUrlType02(@"ss://Y2hhY2hhMjAtaWV0Zi1wb2x5MTMwNTpINVMwaHZjeFJpI1lMbWdV@38.91.107.245:1230#%F0%9F%87%BA%F0%9F%87%B8_US_%E7%BE%8E%E5%9B%BD");
            Assert.IsNotNull(res);
            //Assert.Fail();
        }
    }
}
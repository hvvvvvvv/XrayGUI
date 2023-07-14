using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetProxyController.Modle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetProxyController.Modle.Tests
{
    [TestClass()]
    public class EnumExtensionsTests
    {
        [TestMethod()]
        public void GetParseEunmExTest()
        {
            Console.WriteLine(EnumExtensions.ParseEunmEx<TransportSecurity>("reality1"));
           // Assert.Fail();
            
        }

    }
}
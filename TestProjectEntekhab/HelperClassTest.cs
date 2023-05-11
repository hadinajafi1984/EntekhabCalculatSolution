
using OvetimePolicies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OvetimePolicies.Helper;

namespace TestProjectEntekhab
{
    [TestClass]
    public class HelperClassTest
    {
        [TestMethod]
        public void Test_CalcurlatorA()
        {
           
            var result = HelperClass.ConverDate("14010801");
            Assert.AreEqual(result, new DateTime(2022, 10, 30));
        }
    }
}

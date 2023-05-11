using OvetimePolicies;

namespace TestProjectEntekhab
{
    [TestClass]
    public class OvetimeServicesTest
    {
        [TestMethod]
        public void Test_CalcurlatorA()
        {
            OvetimeServices services = new OvetimeServices(50000000, 10000000, 2000000, 9 / 100);
            var result = services.CalculatorA();
            Assert.AreEqual(result, 122000000);
        }
        [TestMethod]
        public void Test_CalcurlatorB()
        {
            OvetimeServices services = new OvetimeServices(50000000, 10000000, 2000000, 9 / 100);
            var result = services.CalculatorB();
            Assert.AreEqual(result, 60000000);
        }
        [TestMethod]
        public void Test_CalcurlatorC()
        {
            OvetimeServices services = new OvetimeServices(50000000, 10000000, 2000000, 9 / 100);
            var result = services.CalculatorB();
            Assert.AreEqual(result, 60000000);
        }
    }
}
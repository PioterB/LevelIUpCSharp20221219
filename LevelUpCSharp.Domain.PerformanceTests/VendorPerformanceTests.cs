using System.Linq;
using System.Threading;
using LevelUpCSharp.Collections.Concurrent;
using LevelUpCSharp.Production;
using LevelUpCSharp.Products;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LevelUpCSharp.Domain.PerformanceTests
{
    [TestClass]
    public class VendorPerformanceTests
    {
        [TestMethod]
        public void AAAKicker()
        {

        }

        [TestMethod]
        public void Jdg()
        {
            var vendor = new Vendor("jdg", new Warehouse<Sandwich>(), new SandwichMaster());
            Thread.Sleep(5*1000);
            vendor.Shutdown();
            Assert.AreEqual(1, vendor.GetStock().Sum(x => x.Count));
        }

        [TestMethod]
        public void SmallFactory()
        {
            var vendor = new SmallFactory("SmallFactory", new Warehouse<Sandwich>(), new SandwichMaster());
            Thread.Sleep(5 * 1000);
            vendor.Shutdown();
            Assert.AreEqual(1, vendor.GetStock().Sum(x => x.Count));
        }
    }
}

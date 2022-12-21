using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LevelUpCSharp.Collections
{
    [TestClass]
    public class WarehouseTests
    {
        [TestMethod]
        public void Add_NewItem_Added()
        {
            var unitUnderTest = new Warehouse<object>();

            unitUnderTest.Add(new object());

            Assert.AreEqual(1, unitUnderTest.Count());
        }

        [TestMethod]
        public void Peak_NotEmpty_Removed()
        {
            var item = new object();
            var unitUnderTest = new Warehouse<object>(new object[]{item});

            var taken = unitUnderTest.Peak();

            Assert.IsNotNull(taken);
        }

        [TestMethod]
        public void Peak_FewItems_FirstAdded()
        {
            var first = new object();
            var second = new object();
            var unitUnderTest = new Warehouse<object>(new object[] { first, second });

            var taken = unitUnderTest.Peak();

            Assert.AreEqual(first, taken);
        }

        [TestMethod]
        public void PeakRange_Nothing_Empty()
        {
            var unitUnderTest = new Warehouse<object>();

            var taken = unitUnderTest.PeakRange(1);

            Assert.AreEqual(0, taken.Count());
        }

        [TestMethod]
        public void PeakRange_MoreThanAsked_AskedAmount()
        {
            var first = new object();
            var second = new object();
            var unitUnderTest = new Warehouse<object>(new object[] { first, second });

            var taken = unitUnderTest.PeakRange(1);

            Assert.AreEqual(1, taken.Count());
        }

        [TestMethod]
        public void PeakRange_MoreThanAsked_WarehouseReduced()
        {
            var first = new object();
            var second = new object();
            var unitUnderTest = new Warehouse<object>(new object[] { first, second });

            var taken = unitUnderTest.PeakRange(1);

            Assert.AreEqual(1, unitUnderTest.Count());
        }
    }
}

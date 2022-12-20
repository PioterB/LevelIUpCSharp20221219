using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using LevelUpCSharp.Products;
using LevelUpCSharp.Retail;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LevelUpCSharp.Domain.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void Has_EmptyMachine_False()
        {
            var unitUnderTest = new VendingMachine();

            var has = unitUnderTest.Has(SandwichKind.Beef);

            Assert.IsFalse(has);
        }

        [TestMethod]
        public void Has_NoRequestedKind_False()
        {
            var unitUnderTest = new VendingMachine();
            unitUnderTest.Put(new Sandwich(SandwichKind.Cheese, DateTimeOffset.Now.AddDays(1)));

            var has = unitUnderTest.Has(SandwichKind.Beef);

            Assert.IsFalse(has);
        }

        [TestMethod]
        public void Has_WithRequestedKind_False()
        {
            var unitUnderTest = new VendingMachine();
            unitUnderTest.Put(new Sandwich(SandwichKind.Beef, DateTimeOffset.Now.AddDays(1)));

            var has = unitUnderTest.Has(SandwichKind.Beef);

            Assert.IsTrue(has);
        }

        [TestMethod]
        public void Put_Sandwich_Added()
        {
            var unitUnderTest = new VendingMachine();
            var sandwich = new Sandwich(SandwichKind.Beef, DateTimeOffset.Now.AddDays(1));
            
            unitUnderTest.Put(sandwich);
            
            var removed = unitUnderTest.Take(sandwich.Kind);
            Assert.AreEqual(sandwich, removed);
        }
    }
}

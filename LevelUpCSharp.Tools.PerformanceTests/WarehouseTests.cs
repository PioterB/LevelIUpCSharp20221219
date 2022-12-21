using System.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LevelUpCSharp.Tools.PerformanceTests
{
    [TestClass]
    public class WarehouseTests
    {
        private int _rounds = 10 * 1000 * 1000;

        [TestMethod]
        public void AAAKicker()
        {

        }

        [TestMethod]
        public void Add_NotSafe()
        {
            var unitUnderTest = new LevelUpCSharp.Collections.Warehouse<object>();
            for (int i = 0; i < _rounds; i++)
            {
                unitUnderTest.Add(new object());
            }
        }

        [TestMethod]
        public void Add_Safe()
        {
            var unitUnderTest = new LevelUpCSharp.Collections.Concurrent.Warehouse<object>();
            for (int i = 0; i < _rounds; i++)
            {
                unitUnderTest.Add(new object());
            }
        }
    }
}

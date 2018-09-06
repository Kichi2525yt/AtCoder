using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace C.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void Example1()
        {
            var result = Program.Run("2 2");
            Assert.AreEqual(0, result);
        }

        [TestMethod()]
        public void Example2()
        {
            var result = Program.Run("1 7");
            Assert.AreEqual(5, result);
        }

        [TestMethod()]
        public void Example3()
        {
            var result = Program.Run("314 1592");
            Assert.AreEqual(496080, result);
        }
    }
}
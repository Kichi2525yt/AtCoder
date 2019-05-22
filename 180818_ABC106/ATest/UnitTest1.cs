using AtCoder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace AtCoder
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void 入力例_1_Copy()
        {
            string input =
                @"1 6 3";
            string output =
                @"5";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_2_Copy()
        {
            string input =
                @"11 5 5";
            string output =
                @"6";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_3_Copy()
        {
            string input =
                @"100 100 100";
            string output =
                @"0";

            AssertIO(input, output);
        }

        private void AssertIO(string input, string output)
        {
            StringReader reader = new StringReader(input);
            Console.SetIn(reader);

            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            Program.Main(new string[0]);

            Assert.AreEqual(output + Environment.NewLine, writer.ToString());
        }
    }
}

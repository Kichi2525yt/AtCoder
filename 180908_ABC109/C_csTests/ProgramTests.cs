using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using B_CS;

namespace AtCoder
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void 入力例_1_Copy()
        {
            string input =
                @"3 3
1 7 11";
            string output =
                @"2";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_2_Copy()
        {
            string input =
                @"3 81
33 105 57";
            string output =
                @"24";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_3_Copy()
        {
            string input =
                @"1 1
1000000000";
            string output =
                @"999999999";

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

using C;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace AtCoder
{
    [TestClass]
    public class ProgramTest
    {
        [TestMethod]
        public void “ü—Í—á_1_Copy()
        {
            string input =
                @"1214
4";
            string output =
                @"2";

            AssertIO(input, output);
        }

        [TestMethod]
        public void “ü—Í—á_2_Copy()
        {
            string input =
                @"3
157";
            string output =
                @"3";

            AssertIO(input, output);
        }

        [TestMethod]
        public void “ü—Í—á_3_Copy()
        {
            string input =
                @"299792458
9460730472580800";
            string output =
                @"2";

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

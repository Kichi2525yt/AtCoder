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
                @"2 700
3 500
5 800";
            string output =
                @"3";

            AssertIO(input, output);
        }

        [TestMethod]
        public void “ü—Í—á_2_Copy()
        {
            string input =
                @"2 2000
3 500
5 800";
            string output =
                @"7";

            AssertIO(input, output);
        }

        [TestMethod]
        public void “ü—Í—á_3_Copy()
        {
            string input =
                @"2 400
3 500
5 800";
            string output =
                @"2";

            AssertIO(input, output);
        }

        [TestMethod]
        public void “ü—Í—á_4_Copy()
        {
            string input =
                @"5 25000
20 1000
40 1000
50 1000
30 1000
1 1000";
            string output =
                @"66";

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

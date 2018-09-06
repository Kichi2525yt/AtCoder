using B;
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
                @"105";
            string output =
                @"1";

            AssertIO(input, output);
        }

        [TestMethod]
        public void “ü—Í—á_2_Copy()
        {
            string input =
                @"7";
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

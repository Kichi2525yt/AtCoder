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
                @"11";
            string output =
                @"Yes";

            AssertIO(input, output);
        }

        [TestMethod]
        public void “ü—Í—á_2_Copy()
        {
            string input =
                @"40";
            string output =
                @"Yes";

            AssertIO(input, output);
        }

        [TestMethod]
        public void “ü—Í—á_3_Copy()
        {
            string input =
                @"3";
            string output =
                @"No";

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

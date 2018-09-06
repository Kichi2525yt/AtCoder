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
        public void ���͗�_1_Copy()
        {
            string input =
                @"AtCoder";
            string output =
                @"AC";

            AssertIO(input, output);
        }

        [TestMethod]
        public void ���͗�_2_Copy()
        {
            string input =
                @"ACoder";
            string output =
                @"WA";

            AssertIO(input, output);
        }

        [TestMethod]
        public void ���͗�_3_Copy()
        {
            string input =
                @"AcycliC";
            string output =
                @"WA";

            AssertIO(input, output);
        }

        [TestMethod]
        public void ���͗�_4_Copy()
        {
            string input =
                @"AtCoCo";
            string output =
                @"WA";

            AssertIO(input, output);
        }

        [TestMethod]
        public void ���͗�_5_Copy()
        {
            string input =
                @"Atcoder";
            string output =
                @"WA";

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

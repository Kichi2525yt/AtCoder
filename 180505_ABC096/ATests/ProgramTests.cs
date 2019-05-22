﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
                @"5 3 11
1";
            string output =
                @"30";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_2_Copy()
        {
            string input =
                @"3 3 4
2";
            string output =
                @"22";

            AssertIO(input, output);
        }

        private void AssertIO(string input, string output)
        {
            StringReader reader = new StringReader(input);
            Console.SetIn(reader);

            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            A.Program.Main(new string[0]);

            Assert.AreEqual(output + Environment.NewLine, writer.ToString());
        }
    }
}
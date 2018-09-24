using Microsoft.VisualStudio.TestTools.UnitTesting;
using B_CS;
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
                @"4
hoge
english
hoge
enigma";
            string output =
                @"No";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_2_Copy()
        {
            string input =
                @"9
basic
c
cpp
php
python
nadesico
ocaml
lua
assembly";
            string output =
                @"Yes";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_3_Copy()
        {
            string input =
                @"8
a
aa
aaa
aaaa
aaaaa
aaaaaa
aaa
aaaaaaa";
            string output =
                @"No";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_4_Copy()
        {
            string input =
                @"3
abc
arc
agc";
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

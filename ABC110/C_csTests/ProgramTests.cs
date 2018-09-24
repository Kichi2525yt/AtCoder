using C_cs;
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
                @"azzel
apple";
            string output =
                @"Yes";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_2_Copy()
        {
            string input =
                @"chokudai
redcoder";
            string output =
                @"No";

            AssertIO(input, output);
        }

        [TestMethod]
        public void 入力例_3_Copy()
        {
            string input =
                @"abcdefghijklmnopqrstuvwxyz
ibyhqfrekavclxjstdwgpzmonu";
            string output =
                @"Yes";

            AssertIO(input, output);
        }

        [TestMethod]
        public void Test1()
        {
            AssertIO(@"srflgsjorjgowevhrtsehynbriuvgetvngbyuinderukbnsieyguitebynivesyuitsjeurgniuseyirtusuenbisyeisetyuriugsyjmtidvrtyiusjmieurtmeawssvetsrdgbsdrbvgdrf
sjhdftuishuirtvhsyehrtawetrunkirtnuindkiudtnhkiudergfykihhvkyerhnikuernhyvktiundheuirtnibyskerjvyutsuiervnkuettttttttttttttkuetkuetkuetyndtkisudy
", "No");
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

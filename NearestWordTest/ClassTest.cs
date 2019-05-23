using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NearestWord;

namespace NearestWordTest
{
    [TestClass]
    public class ClassTest
    {
        [TestMethod]
        public void ReplaceRegexTest()
        {
            var current = "I am on vacation! Tomorrow is Mother's Day";
            var expected = "I am vacation! Tomorrow Mother's Day";
            var treated = current.ReplaceRegex(" is| on| of|");

            Debug.WriteLine(treated);

            Assert.AreEqual(treated, expected);
        }

        [TestMethod]
        public void RemoveStopWordTest()
        {
            IEnumerable<string> current = new[] { "is", "on", "of" };
            var expected = "is | on | of";
            var treated = current.RemoveStopWord();

            Debug.WriteLine(treated);

            Assert.AreEqual(treated, expected);
        }

        [TestMethod]
        public void RemoveSpecialCharactersTest()
        {
            var current = "Testing! Method for removing special characters. Thank you! :)";
            var expected = "TestingMethodforremovingspecialcharactersThankyou";
            var treated = current.RemoveSpecialCharacters();

            Debug.WriteLine(treated);

            Assert.AreEqual(treated, expected);
        }
    }
}

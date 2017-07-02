using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using OctopusTest.Data;

namespace OctopusTest
{
    internal class Utilities
    {
        public static IWebDriver driver { get; set; }


        public static string GetRandomString(int length)
        {
            var r = new Random();
            return new string(Enumerable.Range(0, length).Select(n => (char)(r.Next(32, 127))).ToArray());
        }


        public static string GetStringValueThatDoesNotExistInDb(List<DataCollection> dataCollection, string column)
        {
            var i = 0;
            var incorrectName = GetRandomString(15);
            while (dataCollection.Where(c => c.ColName == column).Any(c => c.ColValue == incorrectName) && i < 10)
            {
                incorrectName = GetRandomString(15);
                i++;
            }
            return incorrectName;
        }
    }
}

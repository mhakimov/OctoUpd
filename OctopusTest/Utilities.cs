using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;

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

       
        public static T SelectRandomElement<T>(IEnumerable<T> enumerable)
        {
            var index = new Random().Next(0, enumerable.Count());
            return enumerable.ToList()[index];
        }


      
    }
}

using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusTest
{
   static class GetMethods
    {
        /// <summary>
        /// Extended method for getting webelement's text value
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetText( IWebElement element)
        {
            return element.GetAttribute("value");
        }
    }
}

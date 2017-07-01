using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusTest
{
   public static class SetMethods
    {
        /// <summary>
        /// Extended method for selecting required value from the dropdown menu
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void SelectDropDownItem(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByText(value);
        }


        /// <summary>
        /// Extended method for clicking web element 
        /// </summary>
        /// <param name="element"></param>
        public static void ClickIt (this IWebElement element)
        {
            element.Click();
        }

        /// <summary>
        /// Extended method for typing text
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void TypeInText(this IWebElement element, string value)
        {
            element.SendKeys(value);
        }

        public static void SwitchWindows()
        {
            GeneralProperties.driver.SwitchTo().Window(GeneralProperties.driver.WindowHandles.Last());
        }

        /// <summary>
        /// Extended method for moving to web element
        /// </summary>
        /// <param name="element"></param>
        public static void MoveIntoElement(this IWebElement element)
        {
            Actions builder = new Actions(GeneralProperties.driver);
            builder.MoveToElement(element).Perform();
        }

        public static void Scroll(int horizontal, int vertical)
        {
            IJavaScriptExecutor jse = (IJavaScriptExecutor)GeneralProperties.driver;
            //soft code it
            jse.ExecuteScript($"scroll({horizontal},{vertical});");
        }
    }
}

using System;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace OctopusTest.Methods
{
   public static class SeleniumMethods
    {
       /// <summary>
       /// Extended method for selecting required value from the dropdown menu
       /// </summary>
       /// <param name="element">
       /// </param>
       public static void ClickIt (this IWebElement element)
        {
            ExplicitWait(element, 10000);
            element.Click();
        }

        /// <summary>
        /// Extended method for typing text
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void TypeInText(this IWebElement element, string value)
        {
            ExplicitWait(element, 5000);
            element.SendKeys(value);
        }

      /// <summary>
      /// Switches to the latest tab
      /// </summary>
       public static void SwitchTabs()
        {
            Thread.Sleep(100);
            Utilities.driver.SwitchTo().Window(Utilities.driver.WindowHandles.Last());
        }

        /// <summary>
        /// Extended method for moving to web element
        /// </summary>
        /// <param name="element"></param>
        public static void MoveIntoElement(this IWebElement element)
        {
            Actions builder = new Actions(Utilities.driver);
            builder.MoveToElement(element).Perform();
        }


        /// <summary>
        /// waits for webelement
        /// </summary>
        /// <param name="element"></param>
        /// <param name="miliseconds"></param>
        public static void ExplicitWait(IWebElement element,int miliseconds)
        {
            new WebDriverWait(Utilities.driver, TimeSpan.FromMilliseconds(miliseconds)).
                          Until(ExpectedConditions.ElementToBeClickable(element));
        }
 
    }
}

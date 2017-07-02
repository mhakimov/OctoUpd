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
            new WebDriverWait(Utilities.driver, TimeSpan.FromMilliseconds(10000)).
                           Until(ExpectedConditions.ElementToBeClickable(element));
            element.Click();
        }

        /// <summary>
        /// Extended method for typing text
        /// </summary>
        /// <param name="element"></param>
        /// <param name="value"></param>
        public static void TypeInText(this IWebElement element, string value)
        {
            new WebDriverWait(Utilities.driver, TimeSpan.FromMilliseconds(5000)).
                           Until(ExpectedConditions.ElementToBeClickable(element));
            element.SendKeys(value);
        }

       /// <summary>
       /// Extended method for clearing the textbox
       /// </summary>
       //public static void ClearUp(this IWebElement element)
       //{
       //    element.Clear();
       //}
       public static void SwitchWindows()
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
          //  new WebDriverWait(Utilities.driver, TimeSpan.FromMilliseconds(5000)).
           //                Until(ExpectedConditions.ElementToBeClickable(element));
            Actions builder = new Actions(Utilities.driver);
            builder.MoveToElement(element).Perform();
        }

        //public static void Scroll(int horizontal, int vertical)
        //{
        //    IJavaScriptExecutor jse = (IJavaScriptExecutor)Utilities.driver;
        //    //soft code it
        //    jse.ExecuteScript($"scroll({horizontal},{vertical});");
        //}


        public static void ScrollToElement(IWebElement element)
        {
            Actions actions = new Actions(Utilities.driver);
            actions.MoveToElement(element);
            actions.Perform();
        }
 
    }
}

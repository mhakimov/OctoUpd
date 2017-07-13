using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;

namespace OctopusTest
{
    class Actionz
    {
        IWebDriver driver;
        public Actionz(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void ClickAt(IWebElement element)
        {
            ExplicitWait(element, 10000);
            element.Click();
        }


        public void TypeInText(IWebElement element, string value)
        {
            ExplicitWait(element, 5000);
            element.SendKeys(value);
        }


        public void MoveIntoElement(IWebElement element)
        {
            Actions builder = new Actions(driver);
            builder.MoveToElement(element).Perform();
        }



        public void ExplicitWait(IWebElement element, int miliseconds)
        {
            new WebDriverWait(driver, TimeSpan.FromMilliseconds(miliseconds)).
                          Until(ExpectedConditions.ElementToBeClickable(element));
        }
    }
}

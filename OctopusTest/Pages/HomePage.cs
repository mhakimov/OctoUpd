using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusTest
{
    class HomePage
    {
        public HomePage()
        {
            PageFactory.InitElements(GeneralProperties.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div/div[1]/a/div/span")]
        public IWebElement AdviserBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[4]/div/div/div[3]/a[1]")]
        public IWebElement CookiesContinueBtn { get; set; }

        public AdviserPage GoToAdviserPage()
        {
            AdviserBtn.Click();
           // SetMethods.Click(AdviserBtn);
            return new AdviserPage();
        }
    }
}

using OctopusTest.Methods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OctopusTest.Pages
{
    internal class HomePage
    {
        public HomePage()
        {
            PageFactory.InitElements(Utilities.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Adviser information']")]
        public IWebElement AdviserBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[3]/a/span[text()='Continue']")]
        public IWebElement CookiesContinueBtn { get; set; }

        public AdviserPage GoToAdviserPage()
        {
            AdviserBtn.ClickIt();
            return new AdviserPage();
        }
    }
}

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

        [FindsBy(How = How.XPath, Using = "/html/body/div[2]/div/div/div/div[1]/a/div/span")]
        public IWebElement AdviserBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[4]/div/div/div[3]/a[1]")]
        public IWebElement CookiesContinueBtn { get; set; }

        public AdviserPage GoToAdviserPage()
        {
            AdviserBtn.ClickIt();
            return new AdviserPage();
        }
    }
}

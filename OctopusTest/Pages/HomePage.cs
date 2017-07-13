using OctopusTest.Methods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OctopusTest.Pages
{
    internal class HomePage
    {
       private IWebDriver driver;
        private Actionz _actionz;

        public HomePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            _actionz = new Actionz(driver);
        }

        [FindsBy(How = How.XPath, Using = "//span[text()='Adviser information']")]
        public IWebElement AdviserBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[3]/a/span[text()='Continue']")]
        public IWebElement CookiesContinueBtn { get; set; }

        public AdviserPage GoToAdviserPage()
        {
            _actionz.ClickAt(AdviserBtn);
            return new AdviserPage(driver);
        }

        public void ClickCookiesContinueBtn()
        {
            _actionz.ClickAt(CookiesContinueBtn);
        }
    }
}

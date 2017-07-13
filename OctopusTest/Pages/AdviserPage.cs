using OctopusTest.Methods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OctopusTest.Pages
{
    class AdviserPage
    {
        private IWebDriver driver;
        private Actionz _actionz;

        public AdviserPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            _actionz = new Actionz(driver);
        }

        [FindsBy(How = How.XPath, Using = ".//*[@id=\"myAdviser\"]/div/div/div[2]/a")]
        public IWebElement ContinueBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "//div/ul/li[2]/a[text()='Our people']")]
        public IWebElement OurPeopleBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "//li[5]/a[@class='nav-toggle caret-right']")]
        public IWebElement AboutUsDdm { get; set; }

        public OurPeoplePage GoToOurPeoplePage()
        {
            _actionz.MoveIntoElement(AboutUsDdm);
            _actionz.ClickAt(OurPeopleBtn);
            return new OurPeoplePage(driver);
        }

        public void ClickContinueBtn()
        {
            _actionz.ClickAt(ContinueBtn);
        }

        public void ClickWebElement(IWebElement element)
        {
            _actionz.ClickAt(element);
        }
    }
}

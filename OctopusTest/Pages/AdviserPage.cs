using OctopusTest.Methods;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace OctopusTest.Pages
{
    class AdviserPage
    {
        public AdviserPage()
        {
            PageFactory.InitElements(Utilities.driver, this);
        }

        [FindsBy(How = How.XPath, Using = ".//*[@id=\"myAdviser\"]/div/div/div[2]/a")]
        public IWebElement ContinueBtn { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/header/div[2]/div/div/div/ul/li[5]/div/div/div/ul/li[2]/a")]
        public IWebElement OurPeopleBtn { get; set; }
        
        [FindsBy(How = How.XPath, Using = "/html/body/header/div[2]/div/div/div/ul/li[5]/a")]
        public IWebElement AboutUsDdm { get; set; }

        public OurPeoplePage GoToOurPeoplePage()
        {
            AboutUsDdm.MoveIntoElement();
            OurPeopleBtn.ClickIt();
            return new OurPeoplePage();
        }
    }
}

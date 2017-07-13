using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using OctopusTest.Methods;

namespace OctopusTest.Pages
{
    class OurPeoplePage
    {
        private IWebDriver driver;
        private Actionz _actionz;

        public OurPeoplePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            _actionz = new Actionz(driver);
        }


        [FindsBy(How = How.XPath, Using = "//input[@placeholder='e.g Simon Rogerson']")]
        public IWebElement SearchTxf { get; set; }

        [FindsBy(How = How.XPath, Using = "//p[text()='No results found']")]
        public IWebElement NoResultsFoundTx { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[3]/div/div/button/span[@class='filter-option pull-left']")]
        public IWebElement SortByDdm { get; set; }

        public IWebElement GetPerson(string person)
        {
            return driver.FindElement(By.XPath($"//div[@class='search-container']//h2[text()='{person}']"));
        }

        public IReadOnlyList<IWebElement> GetAllEmployees()
        {
           return driver.FindElements(By.XPath("//div[@class='content']/h2"));         
        }

        public List<string> GetListOfDisplayedEmpoyeeNames()
        {
            List<string> employeesNames = new List<string>();
            foreach (var employee in GetAllEmployees())
            {
                employeesNames.Add(employee.Text);
            }
            return employeesNames;
        }

        public void SetValueForOrdering(string order)
        {
            _actionz.ClickAt(SortByDdm);
            _actionz.ClickAt(driver.FindElement(By.XPath($"//div[@class='dropdown-menu open']/ul/li/a/span[text()='{order}']")));
        }

        public IWebElement GetTeamCheckBox(string teamName)
        {
           return driver.FindElement(By.XPath($"//ul[@role='menu']/li/a/span[text()='{teamName}']/following-sibling::span"));    
        }

        public void TypeText(IWebElement element, string value)
        {
            _actionz.TypeInText(element, value);
        }

        public void TypeTextInSearchTxf(string value)
        {
            _actionz.TypeInText(SearchTxf, value);
        }

        public void ClickCheckbox(string teamName)
        {
            _actionz.ClickAt(GetTeamCheckBox(teamName));
        }
    }
}

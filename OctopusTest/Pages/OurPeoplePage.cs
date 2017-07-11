using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System.Collections.Generic;
using OctopusTest.Methods;

namespace OctopusTest.Pages
{
    class OurPeoplePage
    {
        public OurPeoplePage()
        {
            PageFactory.InitElements(Utilities.driver, this);
        }

        [FindsBy(How = How.XPath, Using = "//input[@placeholder='e.g Simon Rogerson']")]
        public IWebElement SearchTxf { get; set; }

        [FindsBy(How = How.XPath, Using = "//p[text()='No results found']")]
        public IWebElement NoResultsFoundTx { get; set; }

        [FindsBy(How = How.XPath, Using = "//div[3]/div/div/button/span[@class='filter-option pull-left']")]
        public IWebElement SortByDdm { get; set; }

        public IWebElement GetPerson(string person)
        {
            return Utilities.driver.FindElement(By.XPath($"//div[@class='search-container']//h2[text()='{person}']"));
        }

        public IReadOnlyList<IWebElement> GetAllEmployees()
        {
           return Utilities.driver.FindElements(By.XPath("//div[@class='content']/h2"));         
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
            SortByDdm.ClickIt();
            Utilities.driver.FindElement(By.XPath($"//div[@class='dropdown-menu open']/ul/li/a/span[text()='{order}']")).ClickIt();
        }

        public IWebElement GetTeamCheckBox(string teamName)
        {
           return Utilities.driver.FindElement(By.XPath($"//ul[@role='menu']/li/a/span[text()='{teamName}']/following-sibling::span"));    
        }

    }
}

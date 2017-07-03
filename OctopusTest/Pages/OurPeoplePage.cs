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

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[1]/div[2]/div[1]/div/div/input")]
        public IWebElement SearchTxf { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[2]/div[2]/p")]
        public IWebElement NoResultsFoundTx { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[1]/div[2]/div[3]/div/div/button/span[1]")]
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

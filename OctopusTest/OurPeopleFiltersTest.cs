using NUnit.Framework;
using OctopusTest.Data;
using OctopusTest.Methods;
using OctopusTest.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace OctopusTest
{
    class OurPeopleFiltersTest
    {
        private string _tableLocation, _nameColumn;
        private OurPeoplePage _ourPeoplePage;
     //   private List<IWebElement> _listOfTextfieldsToClear, _listOfElementsToUnclick;
        private List<DataCollection> _employeesDataCollection;


        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _employeesDataCollection = new List<DataCollection>();
            //_listOfTextfieldsToClear = new List<IWebElement>();
            //_listOfElementsToUnclick = new List<IWebElement>();
            _nameColumn = "Name";
            _tableLocation = AppDomain.CurrentDomain.BaseDirectory.Replace("OctopusTest\\bin\\Debug\\", "OctopusEmployees.xlsx");
            TestData.PopulateInCollection(_tableLocation, _employeesDataCollection);

            // ((IJavaScriptExecutor)driver).ExecuteScript("window.resizeTo(1024, 768);");
          
        }

        [SetUp]
        public void Start()
        {
            Utilities.driver = new ChromeDriver();
            Utilities.driver.Navigate().GoToUrl("https://www.octopusinvestments.com/");
            Utilities.driver.Manage().Window.Maximize();

            HomePage homePage = new HomePage();
            if (homePage.CookiesContinueBtn.Displayed) homePage.CookiesContinueBtn.ClickIt();
            SetMethods.ScrollToElement(homePage.AdviserBtn);

            AdviserPage adviserPage = homePage.GoToAdviserPage();
            SetMethods.SwitchWindows();
            // GeneralProperties.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(6000);
            adviserPage.ContinueBtn.ClickIt();

            _ourPeoplePage = adviserPage.GoToOurPeoplePage();
        }

        [TearDown]
        public void Close()
        {
            Utilities.driver.Quit();
            //foreach (var webelement in _listOfTextfieldsToClear)
            //{
            //    webelement.ClearUp();
            //}

            //foreach (var webelement in _listOfElementsToUnclick)
            //{
            //    webelement.ClickIt();
            //}
        }

        [OneTimeTearDown]
        public void FinalClose()
        {
           
        }

        [Test, Description("Verify that search textfield returns correct person"), Author("Marat")]
        public void FilterTest_01_SearchValid()
        {
            var personName = TestData.ReadData(1, _nameColumn, _employeesDataCollection);
            _ourPeoplePage.SearchTxf.TypeInText(personName);
            Assert.That(_ourPeoplePage.GetPerson(personName).Displayed, $"ERROR! web element {personName} is not displayed!");
        }


        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_02_SearchInvalid()
        {
            var incorrectName = TestData.GetStringValueThatDoesNotExistInDb(_employeesDataCollection, _nameColumn);
            _ourPeoplePage.SearchTxf.TypeInText(incorrectName);
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element {incorrectName} is displayed!");
        }


        [Test, Description("Tick a team and search for an employee from a different team"), Author("Marat")]
        public void FilterTest_03_TeamFiltering()
        {     
         //   ((IJavaScriptExecutor)GeneralProperties.driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 350)");
           // GeneralProperties.driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[1]/div[2]/div[4]/div/div/div/ul/li[1]/a/span[2]")).Click();
            Utilities.driver.FindElement(By.XPath("/html/body/div[3]/div/div/div[1]/div[2]/div[4]/div/div/div/ul/li[3]/a/span[2]")).Click();

            //_ourPeoplePage.GetTeamCheckBox("Business development team").Click();

            var indexOfEmployeeFromDifferentTeam = _employeesDataCollection.First(e => e.colName == "Team" && e.colValue != "Corporate Development team").rowNumber;
            var nameOfEmployeeFromDifferentTeam = _employeesDataCollection.
                First(e => e.colName == _nameColumn && e.rowNumber == indexOfEmployeeFromDifferentTeam).colValue;

            _ourPeoplePage.SearchTxf.TypeInText(nameOfEmployeeFromDifferentTeam);
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element for {nameOfEmployeeFromDifferentTeam} is displayed!");
        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_04_SearchInvalid()
        {
            //select dropdown
            _ourPeoplePage.SetValueForOrdering("Z-A");
            List<string> employeesNames = new List<string>();
            foreach (var person in _ourPeoplePage.GetAllEmployees())
            {
                employeesNames.Add(person.Text);
            }
            // proveritj chto otsorten
        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_05_SearchInvalid()
        {

        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_06_SearchInvalid()
        {

        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_07_SearchInvalid()
        {

        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_08_SearchInvalid()
        {

        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_09_SearchInvalid()
        {

        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_10_SearchInvalid()
        {

        }

    }
}

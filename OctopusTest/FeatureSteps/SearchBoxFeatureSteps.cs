using NUnit.Framework;
using OctopusTest.Data;
using OctopusTest.Pages;
using OctopusTest.Tests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace OctopusTest.FeatureSteps
{

    [Binding]
    class SearchBoxFeatureSteps : BaseTest
    {
        private List<DataCollection> _employeesDataCollection = new List<DataCollection>();
        private string _nameColumn = "Name";
        private OurPeoplePage _ourPeoplePage;
        private string _tableLocation = AppDomain.CurrentDomain.BaseDirectory.Replace("OctopusTest\\bin\\Debug\\", "OctopusEmployees.xlsx");
        private string _randomPersonName, _incorrectName;

        //[Given(@"I type a (.*) name in the searchbox")]
        //public void GivenITypeANameInTheSearchbox(string person)
        //{
        //    var personName = TestData.ReadData(TestData.GetRandomIndexFromTable(_employeesDataCollection), _nameColumn, _employeesDataCollection);
        //    _ourPeoplePage.TypeTextInSearchTxf(personName);
        //}

        [Given(@"I type a person name in the searchbox")]
        public string GivenITypeAPersonNameInTheSearchbox()
        {
            _randomPersonName = TestData.ReadData(TestData.GetRandomIndexFromTable(_employeesDataCollection), _nameColumn, _employeesDataCollection);
            _ourPeoplePage.TypeTextInSearchTxf(_randomPersonName);
            return _randomPersonName;
        }


        [Given(@"I launch chrome")]
        public void GivenILaunchChrome()
        {
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [Given(@"I load employees table data")]
        public void GivenILoadEmployeesTableData()
        {
            TestData.PopulateInCollection(_tableLocation, _employeesDataCollection);
        }


        [Given(@"I navigate to Octopus home page")]
        public void GivenINavigateToOctopusHomePage()
        {
            driver.Navigate().GoToUrl("https://www.octopusinvestments.com/");
        }

        [Given(@"I maximise browser window")]
        public void GivenIMaximiseBrowserWindow()
        {
            driver.Manage().Window.Maximize();
        }


        [Given(@"I navigate to Our People page")]
        public void GivenINavigateToOurPeoplePage()
        {
            HomePage homePage = new HomePage(driver);
            homePage.ClickCookiesContinueBtn();
            AdviserPage adviserPage = homePage.GoToAdviserPage();
            SwitchTabs();

            adviserPage.ClickContinueBtn();

            _ourPeoplePage = adviserPage.GoToOurPeoplePage();
        }


        [Given(@"I type a name that does not exist in Employees Table")]
        public void GivenITypeANameThatDoesNotExistInEmployeesTable()
        {
            _incorrectName = TestData.GetStringValueThatDoesNotExistInDb(_employeesDataCollection, _nameColumn);
            _ourPeoplePage.TypeTextInSearchTxf(_incorrectName);
        }

        [Then(@"No Results text is displayed")]
        public void ThenNoResultsTextIsDisplayed()
        {
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element {_incorrectName} is displayed!");
        }


        [Then(@"person with such name appears on the screen")]
        public void ThenPersonWithSuchNameAppearsOnTheScreen()
        {
            Assert.That(_ourPeoplePage.GetPerson(_randomPersonName).Displayed, $"ERROR! web element {_randomPersonName} is not displayed!");
            driver.Quit();
        }


        public void SwitchTabs()
        {
            Thread.Sleep(100);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }
    }
}

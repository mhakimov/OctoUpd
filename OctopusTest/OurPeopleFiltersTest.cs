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
        private List<IWebElement> _listOfTextfieldsToClear, _listOfElementsToUnclick;
        private List<DataCollection> _employeesDataCollection;


        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _employeesDataCollection = new List<DataCollection>();
            _listOfTextfieldsToClear = new List<IWebElement>();
            _listOfElementsToUnclick = new List<IWebElement>();
            _nameColumn = "Name";
            _tableLocation = AppDomain.CurrentDomain.BaseDirectory.Replace("OctopusTest\\bin\\Debug\\", "OctopusEmployees.xlsx");
            TestData.PopulateInCollection(_tableLocation, _employeesDataCollection);

            GeneralProperties.driver = new ChromeDriver();
            GeneralProperties.driver.Navigate().GoToUrl("https://www.octopusinvestments.com/");
            // ((IJavaScriptExecutor)driver).ExecuteScript("window.resizeTo(1024, 768);");
            GeneralProperties.driver.Manage().Window.Maximize();
            
           // SetMethods.Scroll(0, 500);

            HomePage homePage = new HomePage();
            if (homePage.CookiesContinueBtn.Displayed) homePage.CookiesContinueBtn.ClickIt();
            SetMethods.ScrollToElement(homePage.AdviserBtn);

            AdviserPage adviserPage = homePage.GoToAdviserPage();
            SetMethods.SwitchWindows();
            //  GeneralProperties.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(6000);
            adviserPage.ContinueBtn.ClickIt();

             _ourPeoplePage = adviserPage.GoToOurPeoplePage();
        }

        [SetUp]
        public void Start()
        {

        }

        [TearDown]
        public void Close()
        {
            foreach (var webelement in _listOfTextfieldsToClear)
            {
                webelement.ClearUp();
            }

            foreach (var webelement in _listOfElementsToUnclick)
            {
                webelement.ClickIt();
            }
        }

        [OneTimeTearDown]
        public void FinalClose()
        {
            GeneralProperties.driver.Quit();
        }

        [Test, Description("Verify that search textfield returns correct person"), Author("Marat")]
        public void FilterTest_01_SearchValid()
        {
            _ourPeoplePage.SearchTxf.TypeInText(TestData.ReadData(1, _nameColumn, _employeesDataCollection));
            _listOfTextfieldsToClear.Add(_ourPeoplePage.SearchTxf);
            Assert.That(_ourPeoplePage.AnnaPollinsTx.Displayed, $"ERROR! web element for {_ourPeoplePage.AnnaPollinsTx} is displayed!");

        }


        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_02_SearchInvalid()
        {
            var incorrectName = TestData.GetStringValueThatDoesNotExistInDb(_employeesDataCollection, _nameColumn);
            _ourPeoplePage.SearchTxf.TypeInText(incorrectName);
            _listOfTextfieldsToClear.Add(_ourPeoplePage.SearchTxf);

            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element for {_ourPeoplePage.NoResultsFoundTx.GetCssValue("p")} is displayed!");
           // Assert.That(_ourPeoplePage.ResultsCountTx) IS EQUAL 0. gET ELEMENT VALUE SOMEHOW!!!
        }


        [Test, Description("Tick a team and search for an employee from a different team"), Author("Marat")]
        public void FilterTest_03_TeamFiltering()
        {
            //SetMethods.Scroll(0, 2700);
            //Actions actions = new Actions(GeneralProperties.driver);
            //actions.MoveToElement(_ourPeoplePage.SalesSupportTeamChb);
            //actions.Perform();

            ((IJavaScriptExecutor)GeneralProperties.driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 350)");

            _ourPeoplePage.SalesSupportTeamChb.ClickIt();
            _listOfElementsToUnclick.Add(_ourPeoplePage.SalesSupportTeamChb);

            var indexOfEmployeeFromDifferentTeam = _employeesDataCollection.First(e => e.colName == "Team" && e.colValue != "Sales support team").rowNumber;
               // Where(e => e.colName == "Team" && e.colValue != "Sales support team").F;
            var nameOfEmployeeFromDifferentTeam = _employeesDataCollection.
                First(e => e.colName == _nameColumn && e.rowNumber == indexOfEmployeeFromDifferentTeam).colValue;

            _ourPeoplePage.SearchTxf.TypeInText(nameOfEmployeeFromDifferentTeam);
            _listOfTextfieldsToClear.Add(_ourPeoplePage.SearchTxf);
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element for {_ourPeoplePage.NoResultsFoundTx.GetCssValue("p")} is displayed!");
        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_04_SearchInvalid()
        {
            _ourPeoplePage.SearchTxf.ClickIt();
            _ourPeoplePage.SortByDdm.SelectDropDownItem("Z-A");
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

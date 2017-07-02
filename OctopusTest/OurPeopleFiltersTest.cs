using NUnit.Framework;
using OctopusTest.Data;
using OctopusTest.Pages;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using OctopusTest.Methods;

namespace OctopusTest
{
    class OurPeopleFiltersTest
    {
        private string _tableLocation, _nameColumn, _teamColumn, _corporateDevelopmentTeam;
        private OurPeoplePage _ourPeoplePage;
        private List<DataCollection> _employeesDataCollection;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _employeesDataCollection = new List<DataCollection>();
            _nameColumn = "Name";
            _teamColumn = "Team";
            _corporateDevelopmentTeam = "Corporate Development team";
            _tableLocation = "C:\\services\\octopus2\\OctopusEmployees.xlsx";
          //  _tableLocation = AppDomain.CurrentDomain.BaseDirectory.Replace("OctopusTest\\bin\\Debug\\", "OctopusEmployees.xlsx");
            TestData.PopulateInCollection(_tableLocation, _employeesDataCollection);

        }

        [SetUp]
        public void Start()
        {
            Utilities.driver = new ChromeDriver();
            Utilities.driver.Navigate().GoToUrl("https://www.octopusinvestments.com/");
            Utilities.driver.Manage().Window.Maximize();

            HomePage homePage = new HomePage();
            homePage.CookiesContinueBtn.ClickIt();
            SeleniumMethods.ScrollToElement(homePage.AdviserBtn);          
            AdviserPage adviserPage = homePage.GoToAdviserPage();
            SeleniumMethods.SwitchWindows();

             Utilities.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            adviserPage.ContinueBtn.ClickIt();

            _ourPeoplePage = adviserPage.GoToOurPeoplePage();
        }

        [TearDown]
        public void Close()
        {
            Utilities.driver.Quit();
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
            var incorrectName = Utilities.GetStringValueThatDoesNotExistInDb(_employeesDataCollection, _nameColumn);
            _ourPeoplePage.SearchTxf.TypeInText(incorrectName);
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element {incorrectName} is displayed!");
        }


        [Test, Description("Tick a team and search for an employee from a different team"), Author("Marat")]
        public void FilterTest_03_TeamFiltering()
        {     
            _ourPeoplePage.GetTeamCheckBox(_corporateDevelopmentTeam).ClickIt();

            var indexOfEmployeeFromDifferentTeam = _employeesDataCollection.First(e => e.ColName == _teamColumn && e.ColValue != _corporateDevelopmentTeam).RowNumber;
            var nameOfEmployeeFromDifferentTeam = _employeesDataCollection.
                First(e => e.ColName == _nameColumn && e.RowNumber == indexOfEmployeeFromDifferentTeam).ColValue;

            _ourPeoplePage.SearchTxf.TypeInText(nameOfEmployeeFromDifferentTeam);
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element for {nameOfEmployeeFromDifferentTeam} is displayed!");
        }


        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_04_SearchInvalid()
        {
            _ourPeoplePage.GetTeamCheckBox("Business development team").ClickIt();

            _ourPeoplePage.SetValueForOrdering("Z-A");
            List<string> employeesNames = new List<string>();

            foreach (var employee in _ourPeoplePage.GetAllEmployees())
            {
                employeesNames.Add(employee.Text);
                //Regex myReg = new Regex("^[A-Za-z]*");
                //employeeFirstNames.Add(myReg.Match(person.Text).Value);
            }

            Assert.That(employeesNames, Is.Ordered.Descending);
        }

        [Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        public void FilterTest_05_SearchInvalid()
        {
            _ourPeoplePage.GetTeamCheckBox(_corporateDevelopmentTeam).ClickIt();
            List<string> employeesNames = new List<string>();
            List<string> employeesNamesInDb = new List<string>();

            foreach (var employee in _ourPeoplePage.GetAllEmployees())
            {
                employeesNames.Add(employee.Text);
            }

            List<int> employeeDbIndexes = new List<int>(from em in _employeesDataCollection
                where (em.ColName == _teamColumn && em.ColValue == _corporateDevelopmentTeam)
                select em.RowNumber);

            foreach (var index in employeeDbIndexes)
            {
                employeesNamesInDb.Add(_employeesDataCollection.
                    First(e=>e.ColName == _nameColumn && e.RowNumber == index).ColValue);
            }

            foreach (var employeeInDb in employeesNamesInDb)
            {
                Assert.That(employeesNames.Contains(employeeInDb));
            }

            Assert.That(employeesNamesInDb.Count, Is.EqualTo(employeesNames.Count));    
        }

        //[Test, Description("Verify that search textfield returns empty list when search value is incorrect"), Author("Marat")]
        //public void FilterTest_06_SearchInvalid()
        //{

        //}

       

    }
}

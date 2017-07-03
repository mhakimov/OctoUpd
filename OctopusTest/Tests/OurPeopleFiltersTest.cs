using NUnit.Framework;
using OctopusTest.Data;
using OctopusTest.Pages;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using OctopusTest.Methods;
using System.Text.RegularExpressions;

namespace OctopusTest
{
    class OurPeopleFiltersTest
    {
        private string _tableLocation, _nameColumn, _teamColumn;
        private string _businessDevelopmentTeam, _corporateDevelopmentTeam, _institutionalFundsTeam;
        private string _za;
        private OurPeoplePage _ourPeoplePage;
        private List<DataCollection> _employeesDataCollection;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
            _employeesDataCollection = new List<DataCollection>();
            _nameColumn = "Name";
            _teamColumn = "Team";
            _businessDevelopmentTeam = "Business development team";
            _corporateDevelopmentTeam = "Corporate Development team";
            _institutionalFundsTeam = "Institutional Funds team";
            _za = "Z-A";

            //gets the location of the excel data source
            _tableLocation = AppDomain.CurrentDomain.BaseDirectory.Replace("OctopusTest\\bin\\Debug\\", "OctopusEmployees.xlsx");

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
            AdviserPage adviserPage = homePage.GoToAdviserPage();
            SeleniumMethods.SwitchTabs();

             Utilities.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            adviserPage.ContinueBtn.ClickIt();

            _ourPeoplePage = adviserPage.GoToOurPeoplePage();
        }

        [TearDown]
        public void Close()
        {
            Utilities.driver.Quit();
        }


        [Test] 
        [Description("Verify that when searching for an existing employee, the correct employee information is returned.")]
        [Author("Marat")]
        public void FilterTest_01_SearchValid()
        {
            var personName = TestData.ReadData(TestData.GetRandomIndexFromTable(_employeesDataCollection), _nameColumn, _employeesDataCollection);
            _ourPeoplePage.SearchTxf.TypeInText(personName);
            Assert.That(_ourPeoplePage.GetPerson(personName).Displayed, $"ERROR! web element {personName} is not displayed!");
        }


        [Test] 
        [Description("Verify that when searching for a non-existing employee, no employee information is returned")]
        [Author("Marat")]
        public void FilterTest_02_SearchInvalid()
        {
            var incorrectName = TestData.GetStringValueThatDoesNotExistInDb(_employeesDataCollection, _nameColumn);
            _ourPeoplePage.SearchTxf.TypeInText(incorrectName);
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element {incorrectName} is displayed!");
        }


        [Test] 
        [Description("Verify that when ticking a team and searching for an employee who is not in that team, his/her name will not be returned.")]
        [Author("Marat")]
        public void FilterTest_03_TeamFiltering()
        {     
            _ourPeoplePage.GetTeamCheckBox(_corporateDevelopmentTeam).ClickIt();
            var indexOfEmployeeFromDifferentTeam = _employeesDataCollection.First(e => e.ColName == _teamColumn && e.ColValue != _corporateDevelopmentTeam).RowNumber;
            var nameOfEmployeeFromDifferentTeam = _employeesDataCollection.
                First(e => e.ColName == _nameColumn && e.RowNumber == indexOfEmployeeFromDifferentTeam).ColValue;

            _ourPeoplePage.SearchTxf.TypeInText(nameOfEmployeeFromDifferentTeam);
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element for {nameOfEmployeeFromDifferentTeam} is displayed!");
        }


        [Test]
        [Description("After applying Team filtering and ‘Z-A’ sorting, verify that all employee names are displayed in the descending order")]
        [Author("Marat")]
        public void FilterTest_04_TeamFilterZaSort()
        {
            _ourPeoplePage.GetTeamCheckBox(_businessDevelopmentTeam).ClickIt();
            _ourPeoplePage.SetValueForOrdering(_za);
            List<string> employeesNames = _ourPeoplePage.GetListOfDisplayedEmpoyeeNames();

            Assert.That(employeesNames, Is.Ordered.Descending);
        }


        [Test]
        [Description("Verify that when ticking a team, all employees from that team are returned")]
        [Author("Marat")]
        public void FilterTest_05_EmployeesFromTeam()
        {
            _ourPeoplePage.GetTeamCheckBox(_corporateDevelopmentTeam).ClickIt();
            List<string> displayedEmployeesNames = _ourPeoplePage.GetListOfDisplayedEmpoyeeNames();

            List<int> employeeDbIndexes = new List<int>(from em in _employeesDataCollection
                where (em.ColName == _teamColumn && em.ColValue == _corporateDevelopmentTeam)
                select em.RowNumber);

            List<string> employeesNamesInDb = TestData.
                GetEmployeeNamesFromIndexes(employeeDbIndexes, _employeesDataCollection, _nameColumn);

            foreach (var employeeInDb in employeesNamesInDb)
            {
                Assert.That(displayedEmployeesNames.Contains(employeeInDb), $"ERROR! {employeeInDb} is not displayed");
            }

            Assert.That(employeesNamesInDb.Count, Is.EqualTo(displayedEmployeesNames.Count));    
        }


        [Test]
        [Description("Verify that when ticking multiple team, all employees from that teams are returned.")]
        [Author("Marat")]
        public void FilterTest_06_MultipleTeams()
        {
            _ourPeoplePage.GetTeamCheckBox(_institutionalFundsTeam).ClickIt();
            _ourPeoplePage.GetTeamCheckBox(_corporateDevelopmentTeam).ClickIt();

            List <string> displayedEmployeesNames = _ourPeoplePage.GetListOfDisplayedEmpoyeeNames();

            List<int> employeeDbIndexes = new List<int>(from em in _employeesDataCollection
                                                                where (em.ColName == _teamColumn && (em.ColValue == _institutionalFundsTeam || em.ColValue== _corporateDevelopmentTeam))
                                                                select em.RowNumber);

            List<string> employeesNamesInDb = TestData.
                GetEmployeeNamesFromIndexes(employeeDbIndexes, _employeesDataCollection, _nameColumn);

            Regex spaceAtTheEnd = new Regex(" $");
            foreach (var employeeInDb in employeesNamesInDb)
            {
                if (!spaceAtTheEnd.IsMatch(employeeInDb))
                    Assert.That(displayedEmployeesNames.Contains(employeeInDb), $"ERROR! {employeeInDb} is not displayed");
                else
                    Assert.That(displayedEmployeesNames.Contains(spaceAtTheEnd.Replace(employeeInDb, "")), $"ERROR! {employeeInDb} is not displayed");
            }

            Assert.That(employeesNamesInDb.Count, Is.EqualTo(displayedEmployeesNames.Count));
        }

    }
}

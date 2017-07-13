using NUnit.Framework;
using OctopusTest.Data;
using OctopusTest.Pages;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using OctopusTest.Methods;
using System.Text.RegularExpressions;
using OpenQA.Selenium;
using System.Threading;
using OctopusTest.Tests;

namespace OctopusTest
{
    class OurPeopleFiltersTest : BaseTest
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
            HomePage homePage = new HomePage(driver);
            homePage.ClickCookiesContinueBtn();
            AdviserPage adviserPage = homePage.GoToAdviserPage();
            SwitchTabs();

            adviserPage.ClickContinueBtn();

            _ourPeoplePage = adviserPage.GoToOurPeoplePage();
        }


        [Test] 
        [Description("Verify that when searching for an existing employee, the correct employee information is returned.")]
        [Author("Marat")]
        public void FilterTest_01_SearchValid()
        {
            var personName = TestData.ReadData(TestData.GetRandomIndexFromTable(_employeesDataCollection), _nameColumn, _employeesDataCollection);
            _ourPeoplePage.TypeTextInSearchTxf(personName);
            Assert.That(_ourPeoplePage.GetPerson(personName).Displayed, $"ERROR! web element {personName} is not displayed!");
        }


        [Test] 
        [Description("Verify that when searching for a non-existing employee, no employee information is returned")]
        [Author("Marat")]
        public void FilterTest_02_SearchInvalid()
        {
            var incorrectName = TestData.GetStringValueThatDoesNotExistInDb(_employeesDataCollection, _nameColumn);
            _ourPeoplePage.TypeTextInSearchTxf(incorrectName);
            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element {incorrectName} is displayed!");
        }


        [Test] 
        [Description("Verify that when ticking a team and searching for an employee who is not in that team, his/her name will not be returned.")]
        [Author("Marat")]
        public void FilterTest_03_TeamFiltering()
        {
            _ourPeoplePage.ClickCheckbox(_corporateDevelopmentTeam);
            var nameOfEmployeeFromDifferentTeam = TestData.GetNameOfEmployeeNotFromThisTeam(_employeesDataCollection,
                _corporateDevelopmentTeam);
            _ourPeoplePage.TypeTextInSearchTxf(nameOfEmployeeFromDifferentTeam);

            Assert.That(_ourPeoplePage.NoResultsFoundTx.Displayed, $"ERROR! web element for {nameOfEmployeeFromDifferentTeam} is displayed!");
        }


        [Test]
        [Description("After applying Team filtering and ‘Z-A’ sorting, verify that all employee names are displayed in the descending order")]
        [Author("Marat")]
        public void FilterTest_04_TeamFilterZaSort()
        {
            _ourPeoplePage.ClickCheckbox(_businessDevelopmentTeam);               
            _ourPeoplePage.SetValueForOrdering(_za);
            var employeesNames = _ourPeoplePage.GetListOfDisplayedEmpoyeeNames();

            Assert.That(employeesNames, Is.Ordered.Descending);
        }


        [Test]
        [Description("Verify that when ticking a team, all employees from that team are returned")]
        [Author("Marat")]
        public void FilterTest_05_EmployeesFromTeam()
        {
            _ourPeoplePage.ClickCheckbox(_corporateDevelopmentTeam);

            var displayedEmployeesNames = _ourPeoplePage.GetListOfDisplayedEmpoyeeNames();
            var employeesNamesInDb = TestData.GetAllTeammatesFromDb(_employeesDataCollection,
               new string[] { _corporateDevelopmentTeam });

            AssertThatList1ContainsAllElementsOfList2(displayedEmployeesNames, employeesNamesInDb);
            Assert.That(employeesNamesInDb.Count, Is.EqualTo(displayedEmployeesNames.Count));    
        }


        [Test]
        [Description("Verify that when ticking multiple team, all employees from that teams are returned.")]
        [Author("Marat")]
        public void FilterTest_06_MultipleTeams()
        {
            _ourPeoplePage.ClickCheckbox(_institutionalFundsTeam);
            _ourPeoplePage.ClickCheckbox(_corporateDevelopmentTeam);

            var displayedEmployeesNames = _ourPeoplePage.GetListOfDisplayedEmpoyeeNames();
            var employeesNamesInDb = TestData.GetAllTeammatesFromDb(_employeesDataCollection,
               new string[] {_institutionalFundsTeam, _corporateDevelopmentTeam });

            AssertThatList1ContainsAllElementsOfList2(displayedEmployeesNames, employeesNamesInDb);
            Assert.That(employeesNamesInDb.Count, Is.EqualTo(displayedEmployeesNames.Count));
        }


        public void SwitchTabs()
        {
            Thread.Sleep(100);
            driver.SwitchTo().Window(driver.WindowHandles.Last());
        }


        public static void AssertThatList1ContainsAllElementsOfList2(List<string> list1, List<string> list2)
        {
            Regex spaceAtTheEnd = new Regex(" $");
            foreach (var element in list2)
            {
                if (spaceAtTheEnd.IsMatch(element) && !list1.Contains(element))
                    Assert.That(list1.Contains(spaceAtTheEnd.Replace(element, "")), $"ERROR! {element} is not displayed");
                else
                    Assert.That(list1.Contains(element), $"ERROR! {element} is not displayed");
            }
        }
    }
}

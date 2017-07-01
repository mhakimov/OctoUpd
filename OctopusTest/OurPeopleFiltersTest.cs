using NUnit.Framework;
using OctopusTest.Pages;
using OpenQA.Selenium.Chrome;
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
        private string abc, xyz;
        private OurPeoplePage _ourPeoplePage;


        [OneTimeSetUp]
        public void FixtureSetup()
        {
            abc = AppDomain.CurrentDomain.BaseDirectory;
            xyz = abc.Replace("OctopusTest\\bin\\Debug\\", "OctopusEmployees.xlsx");
            TestData.PopulateInCollection(xyz);

            GeneralProperties.driver = new ChromeDriver();
            GeneralProperties.driver.Navigate().GoToUrl("https://www.octopusinvestments.com/");
            // ((IJavaScriptExecutor)driver).ExecuteScript("window.resizeTo(1024, 768);");
            GeneralProperties.driver.Manage().Window.Maximize();
            SetMethods.Scroll(0, 500);

            HomePage homePage = new HomePage();
            AdviserPage adviserPage = homePage.GoToAdviserPage();
            SetMethods.SwitchWindows();
            //  GeneralProperties.driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(6000);
            adviserPage.ContinueBtn.ClickIt();

             _ourPeoplePage = adviserPage.GoToOurPeoplePage();
        }

        [OneTimeTearDown]
        public void Close()
        {
            GeneralProperties.driver.Quit();
        }

        [Test, Description("Verify that search textfield returns correct person"), Author("Marat")]
        public void FilterTest_01()
        {
            _ourPeoplePage.SearchTxf.TypeInText(TestData.ReadData(1, "Name"));
            Assert.That(_ourPeoplePage.AnnaPollinsTx.Displayed, $"ERROR! web element for {_ourPeoplePage.AnnaPollinsTx} is displayed!");
         //   Assert.That(!_ourPeoplePage.EmilyJamesTx.Displayed);

        }

    }
}

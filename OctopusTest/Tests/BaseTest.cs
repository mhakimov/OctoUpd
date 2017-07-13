using NUnit.Framework;
using OctopusTest.Data;
using OctopusTest.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusTest.Tests
{
   public class BaseTest
    {
        public IWebDriver driver;

        [OneTimeSetUp]
        public void FixtureSetup()
        {
       
        }

        [SetUp]
        public void Start()
        {
            driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://www.octopusinvestments.com/");
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);          
        }

        [TearDown]
        public void Close()
        {
            driver.Quit();
        }
    }
}

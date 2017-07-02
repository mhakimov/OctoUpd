using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctopusTest.Pages
{
    class OurPeoplePage
    {
        public OurPeoplePage()
        {
            PageFactory.InitElements(GeneralProperties.driver, this);
        }
        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[1]/div[2]/div[1]/div/div/input")]
        public IWebElement SearchTxf { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[2]/div[2]/a[1]/div/h2")]
        public IWebElement AnnaPollinsTx { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[2]/div[2]/a/div/h2")]
        public IWebElement EmilyJamesTx { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[2]/div[2]/p")]
        public IWebElement NoResultsFoundTx { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[1]/div[2]/div[5]")]
        public IWebElement ResultsCountTx { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[1]/div[2]/div[4]/div/div/div/ul/li[10]/a/span[2]")]
        public IWebElement SalesSupportTeamChb { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div[3]/div/div/div[1]/div[2]/div[3]/div/div/button/span[1]")]
        public IWebElement SortByDdm { get; set; }


        //                                   /html/body/div[3]/div/div/div[1]/div[2]/div[4]/div/div/div/ul/li[10]/a/span[2]
        // /html/body/div[3]/div/div/div[2]/div[2]/a[1]/div
        // /html/body/div[3]/div/div/div[2]/div[2]/a[2]/div
        // /html/body/div[3]/div/div/div[2]/div[2]/a[3]/div
        // /html/body/div[3]/div/div/div[2]/div[2]/a[156]/div


        // /html/body/div[3]/div/div/div[2]/div[2]/p
        // /html/body/div[3]/div/div/div[2]/div[2]/a[1]/div
    }
}

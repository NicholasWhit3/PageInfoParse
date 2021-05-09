using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PageInfoParse
{
    class infoParse : textParse
    {
        private string startingSite = "https://de.wikipedia.org/wiki/Software";
        private int deepLvl = 10;
        private IWebDriver driver;
        private int sizeOfLinksList = 0;
        private int counterArr = 0;
        private string fileName = ".txt";
        private string linkTitle;
        private IList<IWebElement> linksList { get; set; } = new List<IWebElement>();
        public string LinkTitle { get => linkTitle; set => linkTitle = value; }
        public string FileName { get => fileName; set => fileName = value; }

        private string preCheckUrl;
        

        public void OpenBroser()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(startingSite);
        }

        public void deepLevel()
        {
            for(int i = 0; i < deepLvl; i++)
            {
                linkTitle = driver.Title;
                linksList = driver.FindElements(By.XPath("//a[contains(@href, 'wiki')]"));
                sizeOfLinksList = linksList.Count;
                string[] alreadyVisetedLinks = new string[sizeOfLinksList];
                StreamWriter writer = new StreamWriter("Links - " + linkTitle + fileName, true);

                foreach (IWebElement link in linksList)
                {
                    preCheckUrl = link.GetAttribute("href").ToString();
                    if (preCheckUrl.StartsWith("https://de.wikipedia.org"))
                    {
                        alreadyVisetedLinks[counterArr] = preCheckUrl;
                        writer.WriteLine(alreadyVisetedLinks[counterArr]);
                    }
                    counterArr++;
                };

                SourceTextParse(LinkTitle, FileName);

                Thread.Sleep(3000);
                driver.Navigate().GoToUrl(alreadyVisetedLinks[i]);
                counterArr = 0;

                Array.Clear(alreadyVisetedLinks, 0, alreadyVisetedLinks.Length);
                linksList = new List<IWebElement>();
            }

        }


    }
}

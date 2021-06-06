using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PageInfoParse
{
    class InfoParse : TextParse
    {
        private string startingSite = "https://de.wikipedia.org/wiki/Software";
        private string fileName = ".txt";
        private string linkTitle;
        private string preCheckUrl;
        private int sizeOfLinksList = 0;
        private int counterArr = 0;
        private IWebDriver driver;
        private IList<IWebElement> linksList { get; set; } = new List<IWebElement>();
        public string LinkTitle { get => linkTitle; set => linkTitle = value; }
        public string FileName { get => fileName; set => fileName = value; }



        public void OpenBroser()
        {
            driver = new OpenQA.Selenium.Chrome.ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl(startingSite);
        }
        public bool CatLinkCheck(IWebElement catLinkDiv)
        {

            string className = catLinkDiv.GetAttribute("className");

            string junkClassReferences = "internal mw-magiclink-isbn";
            string junkClassThumbinner = "thumbinner";
            string junkClassImage = "image";
            string junkClassThumbRight = "thumb tright";
            string JunkClassInternal = "internal";


            if (className == junkClassReferences)
            {
                return false;
            }
            else if (className == junkClassImage)
            {
                return false;
            }
            else if (className == junkClassThumbinner)
            {
                return false;
            }
            else if (className == junkClassThumbRight)
            {
                return false;
            }
            else if (className == JunkClassInternal)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public void parsePagesRecurvevily(int deepLvl)
        {
            for(int i = 0; i < deepLvl; i++)
            {
                Thread.Sleep(3000);
                linkTitle = driver.Title;
                var searchClass = driver.FindElement(By.CssSelector("#mw-content-text > div.mw-parser-output"));
                linksList = searchClass.FindElements(By.CssSelector("a[href*='wiki']"));
                sizeOfLinksList = linksList.Count;

                string[] alreadyVisetedLinks = new string[sizeOfLinksList];
                StreamWriter writer = new StreamWriter("Links - " + linkTitle + fileName, true);

                foreach (IWebElement link in linksList)
                {
                    if(CatLinkCheck(link) == true)
                    {
                        preCheckUrl = link.GetAttribute("href").ToString();
                        if (preCheckUrl.StartsWith("https://de.wikipedia.org"))
                        {
                            alreadyVisetedLinks[counterArr] = preCheckUrl;
                            writer.WriteLine(alreadyVisetedLinks[counterArr]);
                        }
                    }
                    counterArr++;
                };

                writer.Close();
                GetFirstParagrap(searchClass, LinkTitle, FileName);
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

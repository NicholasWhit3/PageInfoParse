﻿using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace PageInfoParse
{
    class infoParse : textParse
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

            //List<string> junkId = new List<string>()
            //{
            //    "internal mw-magiclink-isbn",
            //    "catlinks normdaten-typ-s",
            //    "references",
            //    "vector-menu-content",
            //    "mw-wiki-logo",
            //    "mw-portlet mw-portlet-personal vector-user-menu-legacy vector-menu",
            //    "mw-normal-catlinks",
            //    "catlinks",
            //    "pt-anontalk",
            //    "sisterproject"
            //};

            //foreach(string iD in junkId)
            //{
            //    try
            //    {
            //        string divName = catLinkDiv.GetAttribute("className");

            //        if (divName != iD) break;
            //    }
            //    catch(Exception e)
            //    {
            //        Console.WriteLine(e.Message);
            //    }
            //}
            //return true;

            string className = catLinkDiv.GetAttribute("className");

            string junkClassReferenceText = "internal mw-magiclink-isbn";
            string junkClassCatLinksNormdaten = "catlinks normdaten-typ-s";
            string junkClassEinzelNachweise = "references";
            string junkClassCatlinks = "mw-normal-catlinks";
            string junkClassVectorMenuContent = "vector-menu-content";
            string junkClassWikiLogo = "mw-wiki-logo";
            string junkClassvectorContentList = "vector-menu-content-list";



            if (className == junkClassCatLinksNormdaten)
            {
                return false;
            }
            else if (className == junkClassEinzelNachweise)
            {
                return false;
            }
            else if (className == junkClassCatlinks)
            {
                return false;
            }
            else if (className == junkClassReferenceText)
            {
                return false;
            }
            else if (className == junkClassVectorMenuContent)
            {
                return false;
            }
            else if (className == junkClassWikiLogo)
            {
                return false;
            }
            else if (className == junkClassvectorContentList)
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

                    //if (CatLinkCheck(link) == true)
                    //{
                        preCheckUrl = link.GetAttribute("href").ToString();
                        if (preCheckUrl.StartsWith("https://de.wikipedia.org"))
                        {
                            alreadyVisetedLinks[counterArr] = preCheckUrl;
                            writer.WriteLine(alreadyVisetedLinks[counterArr]);
                        }
                    //}
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

using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace PageInfoParse
{
    class textParse
    {

        public string getArticleNameFromTitleAttribute(string linkTitle)
        {
            var articleName = linkTitle.Replace(' ', '_');            // Ersetzen die Lücken in den LinkTitle
            if (articleName.Contains("_–_"))
            {
                try
                {
                    articleName = articleName.Split("_–_").First();   // Korrigieren des Linksname um wiki API zu akzeptieren könnte.
                }
                catch(Exception e)
                {  
                    Console.WriteLine("Unexpected link title"); 
                }
            }

            return articleName;
        }


    public void SourceTextParse(string linkTitle, string fileName)
        {

            var articleName = getArticleNameFromTitleAttribute(linkTitle);
            var webclient = new WebClient();
            string link = "https://de.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + articleName + "&explaintext";
            var pagesource = webclient.DownloadString(link);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pagesource);
            var fnode = doc.GetElementsByTagName("extract")[0];     // single()
            string stringSource = fnode.InnerText;

            File.WriteAllTextAsync("Text - " + articleName + fileName, stringSource);
        }

    }
}

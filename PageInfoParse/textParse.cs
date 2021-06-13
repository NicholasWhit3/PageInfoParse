using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;

namespace PageInfoParse
{
    class TextParse : ParseTextStripped
    {
        private string stringSource;
        private string strippedStringSource;

    public string getArticleNameFromTitleAttribute(string linkTitle)
        {
            var articleName = linkTitle.Replace(' ', '_');
            if (articleName.Contains("_–_"))
            {
                try
                {
                    articleName = articleName.Split("_–_").First();
                }
                catch(Exception titleAttribute)
                {  
                    Console.WriteLine(titleAttribute.Message); 
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
            var fnode = doc.GetElementsByTagName("extract")[0];
            stringSource = fnode.InnerText;
            strippedStringSource = TextStripped(stringSource);

            File.WriteAllTextAsync("Text - " + articleName + fileName, stringSource);
            File.WriteAllTextAsync("StrippedText- " + linkTitle + fileName, strippedStringSource);

        }
    }
}

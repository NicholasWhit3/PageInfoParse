using System.IO;
using System.Net;
using System.Xml;

namespace PageInfoParse
{
    class textParse
    {
        public void SourceTextParse(string LinkTitle, string fileName)
        {
            if(LinkTitle.Contains("Wikipedia"))
            {
                LinkTitle = LinkTitle.Split(" ")[0];
            }
            else
            {
                LinkTitle = LinkTitle.Replace(' ', '_');       // check here
            }
     
            var webclient = new WebClient();
            string link = "https://de.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + LinkTitle + "&explaintext";
            var pagesource = webclient.DownloadString(link);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pagesource);
            var fnode = doc.GetElementsByTagName("extract")[0];
            string stringSource = fnode.InnerText;

            File.WriteAllTextAsync("Text -" + LinkTitle + fileName, stringSource);
        }

    }
}

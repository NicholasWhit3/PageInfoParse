using System.IO;
using System.Net;
using System.Xml;

namespace PageInfoParse
{
    class textParse
    {
        public void SourceTextParse(string LinkTitle, string fileName)
        {
            LinkTitle = LinkTitle.Replace(' ', '_');            // Ersetzen die Lücken in den LinkTitle
            if (LinkTitle.Contains("_–_"))
            {
                LinkTitle = LinkTitle.Split("_–_")[0];          // Korrigieren des Linksname um wiki API zu akzeptieren könnte.
            }
     
            var webclient = new WebClient();
            string link = "https://de.wikipedia.org/w/api.php?format=xml&action=query&prop=extracts&titles=" + LinkTitle + "&explaintext";
            var pagesource = webclient.DownloadString(link);

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(pagesource);
            var fnode = doc.GetElementsByTagName("extract")[0];
            string stringSource = fnode.InnerText;

            File.WriteAllTextAsync("Text - " + LinkTitle + fileName, stringSource);
        }

    }
}

using OpenQA.Selenium;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace PageInfoParse
{
    class ParagraphParse
    {
        private IWebElement searchParagraph;
        private string firstParagraph;
        private string strippedParagraph;

        public void GetFirstParagrap(IWebElement link, string linkTitle, string fileName)
        {
            try 
            {
                searchParagraph = link.FindElement(By.CssSelector("#mw-content-text > div.mw-parser-output > p:nth-child(2)"));
                firstParagraph = searchParagraph.Text;
                strippedParagraph = ParagraphStripped(firstParagraph);

                File.WriteAllTextAsync("Paragraph - " + linkTitle + fileName, strippedParagraph);
            }
            catch(Exception ParagraphExcept)
            {
                Console.WriteLine(ParagraphExcept.Message);
            }

        }

        string RemoveJunkChars(string inputStr, char begin, char end)
        {
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(inputStr, string.Empty);
        }


        public string ParagraphStripped(string paragraph)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);

            try
            {
                paragraph = RemoveJunkChars(paragraph, '(', ')');
                paragraph = RemoveJunkChars(paragraph, '[', ']'); 
            }
            catch (Exception strippedExcept)
            {
                Console.WriteLine(strippedExcept.Message);
            }

            paragraph = regex.Replace(paragraph, " ");
            return paragraph; 
        }

    }
}

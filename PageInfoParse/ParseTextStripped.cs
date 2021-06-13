using System;
using System.Text.RegularExpressions;

namespace PageInfoParse
{
    class ParseTextStripped
    {
        string RemoveJunkChars(string inputStr, char begin, char end)
        {
            Regex regex = new Regex(string.Format("\\{0}.*?\\{1}", begin, end));
            return regex.Replace(inputStr, string.Empty);
        }


        public string TextStripped(string souceText)
        {
            RegexOptions options = RegexOptions.None;
            Regex regex = new Regex("[ ]{2,}", options);

            try
            {
                souceText = RemoveJunkChars(souceText, '(', ')');
                souceText = RemoveJunkChars(souceText, '[', ']'); 
            }
            catch (Exception strippedExcept)
            {
                Console.WriteLine(strippedExcept.Message);
            }

            souceText = regex.Replace(souceText, " ");
            return souceText;
        }

    }
}

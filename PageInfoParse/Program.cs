
namespace PageInfoParse
{
    class Program
    {
        //private readonly int deepLvl = 10;
        static void Main(string[] args)
        {
            infoParse linksParse = new infoParse();
            linksParse.OpenBroser();
            linksParse.parsePagesRecurvevily(10);
        }
    }
}


namespace PageInfoParse
{
    class Program
    {
        //private readonly int deepLvl = 10;
        static void Main(string[] args)
        {
            InfoParse linksParse = new InfoParse();
            linksParse.OpenBroser();
            linksParse.parsePagesRecurvevily(10);
        }
    }
}

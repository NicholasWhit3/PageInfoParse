
namespace PageInfoParse
{
    class Program
    {
        static void Main(string[] args)
        {
            infoParse linksParse = new infoParse();
            linksParse.OpenBroser();
            linksParse.deepLevel();
        }
    }
}

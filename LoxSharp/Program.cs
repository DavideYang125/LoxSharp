namespace LoxSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestScanner();
            Console.ReadKey();
        }


        static void TestScanner()
        {
            string source = @"
            var language = ""LoxSharp"";
            print(language + "" is awesome!"");
            if (true) print(123);";

            Scanner scanner = new Scanner(source);
            var tokens = scanner.ScanTokens();

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
    }
}
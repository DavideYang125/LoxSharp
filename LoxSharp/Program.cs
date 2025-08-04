namespace LoxSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintAst();
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

        static void PrintAst()
        {
            Expr expression = new Expr.Binary(
                new Expr.Literal(1.0),
                new Token(TokenType.PLUS, "+", null, 1),
                new Expr.Binary(new Expr.Literal(2.0),
                new Token(TokenType.STAR, "*", null, 1),
                new Expr.Literal(3.0)));
            var visualizer = new AstVisualizer();
            Console.WriteLine(visualizer.Print(expression));
        }
    }
}
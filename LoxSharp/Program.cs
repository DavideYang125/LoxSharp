namespace LoxSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            TestInterpreter();
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

        static void TestInterpreter()
        {
            // 测试用例列表
            string[] testCases = {
                "1 + 2",                    // 简单加法: 3
                "1 + 2 * 3",                // 运算优先级: 7
                "(1 + 2) * 3",              // 括号: 9
                "-123",                     // 负数: -123
                "!true",                    // 逻辑非: false
                "5 > 3",                    // 大于: true
                "5 < 3",                    // 小于: false
                "1 == 1",                   // 相等: true
                "1 == 2",                   // 不等: false
                "\"hello\" + \" world\"",   // 字符串连接: "hello world"
                "nil == nil"                // nil 比较: true
            };

            Console.WriteLine("=== 表达式求值测试 ===\n");

            foreach (string source in testCases)
            {
                Console.WriteLine($"表达式: {source}");

                try
                {
                    Scanner scanner = new Scanner(source);
                    List<Token> tokens = scanner.ScanTokens();

                    Parser parser = new Parser(tokens);
                    Expr expression = parser.Parse();

                    Interpreter interpreter = new Interpreter();
                    object result = interpreter.Interpret(expression);

                    Console.WriteLine($"结果: {StringifyResult(result)}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"错误: {ex.Message}");
                }

                Console.WriteLine();
            }
        }

        // 将结果转换为字符串（用于显示）
        static string StringifyResult(object value)
        {
            if (value == null) return "nil";
            if (value is double d)
            {
                // 如果是整数，去掉小数点
                if (d == (int)d)
                {
                    return ((int)d).ToString();
                }
                return d.ToString();
            }
            return value.ToString();
        }

        #region test parser



        #endregion
    }
}
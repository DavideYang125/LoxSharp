using System;

namespace LoxSharp
{
    public class AstVisualizer : Expr.Visitor<string>
    {
        public string Print(Expr expr)
        {
            return expr.Accept(this);
        }

        private string Indent(string text, string prefix)
        {
            var lines = text.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                lines[i] = prefix + lines[i];
            }
            return string.Join("\n", lines);
        }

        public string VisitBinaryExpr(Expr.Binary expr)
        {
            var left = Indent(expr.Left.Accept(this), "├── ");
            var right = Indent(expr.Right.Accept(this), "└── ");
            return $"Binary {expr.Operator.Lexeme}\n{left}\n{right}";
        }

        public string VisitGroupingExpr(Expr.Grouping expr)
        {
            var inner = Indent(expr.Expression.Accept(this), "└── ");
            return $"Grouping\n{inner}";
        }

        public string VisitLiteralExpr(Expr.Literal expr)
        {
            return $"Literal {expr.Value}";
        }

        public string VisitUnaryExpr(Expr.Unary expr)
        {
            var right = Indent(expr.Right.Accept(this), "└── ");
            return $"Unary {expr.Operator.Lexeme}\n{right}";
        }
    }
}

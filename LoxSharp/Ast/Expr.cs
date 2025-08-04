using System;

namespace LoxSharp
{
    /// <summary>
    /// 抽象表达式
    /// Representing Code
    /// 目标：把代码变成代码表示成结构化的数据，即获取抽象语法树”（AST）
    /// </summary>
    public abstract class Expr
    {
        public interface Visitor<T>
        {
            T VisitBinaryExpr(Binary expr);
            T VisitGroupingExpr(Grouping expr);
            T VisitLiteralExpr(Literal expr);
            T VisitUnaryExpr(Unary expr);
        }

        public abstract T Accept<T>(Visitor<T> visitor);

        /// <summary>
        /// 二元表达式
        /// </summary>
        public class Binary : Expr
        {
            public Expr Left { get; }
            public Token Operator { get; }
            public Expr Right { get; }

            public Binary(Expr left, Token op, Expr right)
            {
                Left = left;
                Operator = op;
                Right = right;
            }

            public override T Accept<T>(Visitor<T> visitor)
            {
                return visitor.VisitBinaryExpr(this);
            }
        }

        /// <summary>
        /// 分组表达式
        /// </summary>
        public class Grouping : Expr
        {
            public Expr Expression { get; }

            public Grouping(Expr expression)
            {
                Expression = expression;
            }

            public override T Accept<T>(Visitor<T> visitor)
            {
                return visitor.VisitGroupingExpr(this);
            }
        }

        /// <summary>
        /// 字面量
        /// </summary>
        public class Literal : Expr
        {
            public object Value { get; }

            public Literal(object value)
            {
                Value = value;
            }

            public override T Accept<T>(Visitor<T> visitor)
            {
                return visitor.VisitLiteralExpr(this);
            }
        }

        /// <summary>
        /// 一元表达式
        /// </summary>
        public class Unary : Expr
        {
            public Token Operator { get; }
            public Expr Right { get; }

            public Unary(Token op, Expr right)
            {
                Operator = op;
                Right = right;
            }

            public override T Accept<T>(Visitor<T> visitor)
            {
                return visitor.VisitUnaryExpr(this);
            }
        }
    }
}

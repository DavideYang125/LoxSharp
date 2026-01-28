using System;

namespace LoxSharp
{
    /// <summary>
    /// 表达式解释器
    /// 实现访问者模式，遍历 AST 并计算表达式值
    /// </summary>
    public class Interpreter : Expr.Visitor<object>
    {
        /// <summary>
        /// 解释表达式的入口方法
        /// </summary>
        public object Interpret(Expr expression)
        {
            try
            {
                return expression.Accept(this);
            }
            catch (RuntimeError error)
            {
                ErrorReporter.RuntimeError(error);
                return null;
            }
        }

        // 访问字面量表达式
        public object VisitLiteralExpr(Expr.Literal expr)
        {
            return expr.Value;
        }

        // 访问分组表达式
        public object VisitGroupingExpr(Expr.Grouping expr)
        {
            return Evaluate(expr.Expression);
        }

        // 访问一元表达式 (!expr 或 -expr)
        public object VisitUnaryExpr(Expr.Unary expr)
        {
            object right = Evaluate(expr.Right);

            switch (expr.Operator.Type)
            {
                case TokenType.BANG:
                    return !IsTruthy(right);
                case TokenType.MINUS:
                    CheckNumberOperand(expr.Operator, right);
                    return -(double)right;
            }

            // 永远不会到达这里
            return null;
        }

        // 访问二元表达式 (算术、比较、相等性运算)
        public object VisitBinaryExpr(Expr.Binary expr)
        {
            object left = Evaluate(expr.Left);
            object right = Evaluate(expr.Right);

            switch (expr.Operator.Type)
            {
                // 算术运算
                case TokenType.MINUS:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left - (double)right;

                case TokenType.SLASH:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left / (double)right;

                case TokenType.STAR:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left * (double)right;

                case TokenType.PLUS:
                    if (left is double leftNum && right is double rightNum)
                    {
                        return leftNum + rightNum;
                    }
                    if (left is string leftStr || right is string rightStr)
                    {
                        return Stringify(left) + Stringify(right);
                    }
                    throw new RuntimeError(expr.Operator,
                        "Operands must be two numbers or two strings.");

                // 比较运算
                case TokenType.GREATER:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left > (double)right;

                case TokenType.GREATER_EQUAL:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left >= (double)right;

                case TokenType.LESS:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left < (double)right;

                case TokenType.LESS_EQUAL:
                    CheckNumberOperands(expr.Operator, left, right);
                    return (double)left <= (double)right;

                // 相等性比较
                case TokenType.BANG_EQUAL:
                    return !IsEqual(left, right);

                case TokenType.EQUAL_EQUAL:
                    return IsEqual(left, right);
            }

            // 永远不会到达这里
            return null;
        }

        // 辅助方法：求值表达式
        private object Evaluate(Expr expr)
        {
            return expr.Accept(this);
        }

        // 判断值是否为真
        // false 和 nil 是假，其他都是真
        private bool IsTruthy(object value)
        {
            if (value == null) return false;
            if (value is bool b) return b;
            return true;
        }

        // 判断两个值是否相等
        private bool IsEqual(object a, object b)
        {
            if (a == null && b == null) return true;
            if (a == null) return false;
            return a.Equals(b);
        }

        // 检查一元运算符的操作数是否为数字
        private void CheckNumberOperand(Token op, object operand)
        {
            if (operand is double) return;
            throw new RuntimeError(op, "Operand must be a number.");
        }

        // 检查二元运算符的两个操作数是否都为数字
        private void CheckNumberOperands(Token op, object left, object right)
        {
            if (left is double && right is double) return;
            throw new RuntimeError(op, "Operands must be numbers.");
        }

        // 将值转换为字符串表示
        private string Stringify(object value)
        {
            if (value == null) return "nil";

            if (value is double d)
            {
                // 处理小数部分为整数的情况
                if (d == (int)d)
                {
                    return d.ToString("F0");
                }
                return d.ToString();
            }

            return value.ToString();
        }
    }
}

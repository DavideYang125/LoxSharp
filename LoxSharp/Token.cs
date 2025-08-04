using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoxSharp
{
    /// <summary>
    /// 词法单元类型
    /// </summary>
    public enum TokenType
    {
        // 单字符 token
        LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE, RIGHT_BRACE,
        COMMA, DOT, MINUS, PLUS, SEMICOLON, SLASH, STAR,

        // 一或两个字符的 token，也就是运算符
        BANG, BANG_EQUAL,
        EQUAL, EQUAL_EQUAL,
        GREATER, GREATER_EQUAL,
        LESS, LESS_EQUAL,

        // 字面量
        IDENTIFIER, STRING, NUMBER,

        // 关键字
        AND, CLASS, ELSE, FALSE, FUN, FOR, IF, NIL, OR,
        PRINT, RETURN, SUPER, THIS, TRUE, VAR, WHILE,

        // 文件结束
        EOF
    }

    /// <summary>
    /// 词法单元
    /// </summary>
    public class Token
    {
        public TokenType Type { get; }

        /// <summary>
        /// 原始
        /// </summary>
        public string Lexeme { get; }

        /// <summary>
        /// 字面量值
        /// </summary>
        public object Literal { get; }

        /// <summary>
        /// 源代码行号
        /// </summary>
        public int Line { get; }

        public Token(TokenType type, string lexeme, object literal, int line)
        {
            Type = type;
            Lexeme = lexeme;
            Literal = literal;
            Line = line;
        }

        public override string ToString()
        {
            return $"{Type} {Lexeme} {Literal}";
        }
    }

}

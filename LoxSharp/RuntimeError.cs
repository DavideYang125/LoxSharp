using System;

namespace LoxSharp
{
    /// <summary>
    /// 运行时错误异常
    /// 在表达式求值过程中抛出
    /// </summary>
    public class RuntimeError : Exception
    {
        public Token Token { get; }

        public RuntimeError(Token token, string message) : base(message)
        {
            Token = token;
        }
    }
}

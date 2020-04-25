using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Text;

namespace MyInterpreter.Parser
{
    public class LexicalAnalyzer : ILexicalAnalyzer
    {
        private readonly IExpressionAccessor accessor;

        private string currentParameter;
        private double currentValue;

        private bool hasNext = true;

        public string CurrentParameter
        {
            get { return currentParameter; }
        }

        public double CurrentValue
        {
            get { return currentValue; }
        }

        public LexicalAnalyzer(IExpressionAccessor expressionAccessor)
        {
            accessor = expressionAccessor
                ?? throw new ArgumentNullException(nameof(expressionAccessor));
        }

        public Token GetCurrentToken()
        {
            if (accessor.CheckEndOfExpression())
            {
                return Token.Null;
            }

            if(!hasNext)
            {
                return Token.Null;
            }

            var currentToken = Token.Illegal;
            var moved = false;
            var currentChar = accessor.GetCurrentChar();
            switch (currentChar)
            {
                case '+':
                    currentToken = Token.Plus;
                    break;
                case '-':
                    currentToken = Token.Sub;
                    break;
                case '*':
                    currentToken = Token.Mul;
                    break;
                case '/':
                    currentToken = Token.Div;
                    break;
                case '(':
                    currentToken = Token.OParen;
                    break;
                case ')':
                    currentToken = Token.CParen;
                    break;
                case ',':
                    currentToken = Token.Comma;
                    break;
                case '$':   // 参数名解析
                    currentParameter = accessor.ExtractParameterKeyFromCurrent();
                    currentToken = Token.Param;
                    moved = true;
                    break;
                default:
                    if (char.IsDigit(currentChar) || currentChar == '.')
                    {
                        currentValue = accessor.ExtractValueFromCurrent();
                        currentToken = Token.Double;
                        moved = true;
                    }
                    else if (char.IsLetter(currentChar)) // sin cos tan cot random
                    {
                        var temp = accessor.ExtractMathSignFromCurrent().ToLower();
                        if (temp == "sin")
                        {
                            currentToken = Token.Sin;
                        }
                        else if (temp == "cos")
                        {
                            currentToken = Token.Cos;
                        }
                        else if (temp == "tan")
                        {
                            currentToken = Token.Tan;
                        }
                        else if (temp == "cot")
                        {
                            currentToken = Token.Cot;
                        }
                        else if (temp == "random")
                        {
                            currentToken = Token.Random;
                        }
                        moved = true;
                    }
                    else
                    {
                        throw new Exceptions.IllegalTokenException("Illegal Token: Calc error");
                    }
                    break;
            }

            if(!moved)
            {
                hasNext = accessor.MoveToNext();
            }

            return currentToken;
        }
    }
}

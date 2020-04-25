using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace MyInterpreter.Parser
{
    /// <summary>
    /// 表达式访问器，为词法分析器提供内容，如当前字符，提取到参数名，当前值
    /// </summary>
    public class ExpressionAccessor : IExpressionAccessor
    {
        private char[] expressionChars;

        private int currentIndex;
        private int expressionLength;

        public ExpressionAccessor(string expression)
        {
            if(string.IsNullOrEmpty(expression))
            {
                throw new ArgumentNullException(nameof(expression));
            }
            var newExpression = expression.Replace(" ", "").Replace("\t", "");
            expression = newExpression;

            expressionChars = expression.ToCharArray();
            currentIndex = 0;
            expressionLength = expressionChars.Length;
        }

        public bool CheckEndOfExpression()
        {
            return currentIndex >= expressionLength;
        }

        /// <summary>
        /// 从当前位置提取参数名
        /// </summary>
        /// <remarks>
        /// 参数以$符开始的字符和数字的组合串
        /// </remarks>
        public string ExtractParameterKeyFromCurrent()
        {
            var temp = "";

            var hasNext = MoveToNext();
            while(hasNext)
            {
                var ch = GetCurrentChar();
                if(char.IsLetterOrDigit(ch))
                {
                    temp += ch;
                    hasNext = MoveToNext();
                }
                else
                {
                    break;
                }
            }

            return temp;
        }

        /// <summary>
        /// 从当前位置提取数值
        /// </summary>
        /// <remarks>
        /// 以数值或者'.'开始（支持直接从'.'开头的浮点数）
        /// </remarks>
        public double ExtractValueFromCurrent()
        {
            var temp = "";

            var hasNext = true;
            var isFloat = false;
            while(hasNext)
            {
                var ch = GetCurrentChar();
                if(char.IsDigit(ch))
                {
                    temp += ch;
                    hasNext = MoveToNext();
                }
                else if(ch == '.')
                {
                    if(!isFloat)
                    {
                        temp += ch;
                        isFloat = true;
                        hasNext = MoveToNext();
                    }
                    else
                    {
                        throw new Exceptions.ExpressionInvalidException(
                            $"Invalid char at: {currentIndex}");
                    }
                }
                else
                {
                    // 没有数字或者'.'了就退出
                    break;
                }
            }

            if(temp.EndsWith('.'))
            {
                throw new Exceptions.IllegalTokenException(
                    "Illegal Token: Number error");
            }

            return Convert.ToDouble(temp);
        }

        public string ExtractMathSignFromCurrent()
        {
            var temp = "";
            var hasNext = true;

            while (hasNext)
            {
                var ch = GetCurrentChar();
                if(char.IsLetter(ch))
                {
                    temp += ch;
                    hasNext = MoveToNext();
                }
                else
                {
                    break;
                }
            }

            return temp;
        }

        public char GetCurrentChar()
        {
            if(currentIndex >= expressionLength)
            {
                throw new IndexOutOfRangeException(nameof(currentIndex));
            }

            return expressionChars[currentIndex];
        }

        public bool MoveToNext()
        {
            if(currentIndex + 1 >= expressionLength)
            {
                return false;
            }
            else
            {
                ++currentIndex;
                return true;
            }
        }
    }
}

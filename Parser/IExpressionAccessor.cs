using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Parser
{
    /// <summary>
    /// 表达式访问器，为词法分析器提供内容
    /// </summary>
    public interface IExpressionAccessor
    {
        /// <summary>
        /// 获取当前位置的字符
        /// </summary>
        /// <remarks>
        /// 位置只能内置根据情形变更，主要是<seealso cref="MoveToNext"/>
        /// </remarks>
        char GetCurrentChar();

        /// <summary>
        /// 移动字符位置指针
        /// </summary>
        bool MoveToNext();

        /// <summary>
        /// 当前字符串是否已经结束
        /// </summary>
        bool CheckEndOfExpression();

        /// <summary>
        /// 从当前位置提取参数名
        /// </summary>
        string ExtractParameterKeyFromCurrent();

        /// <summary>
        /// 从当前位置提取数值
        /// </summary>
        double ExtractValueFromCurrent();

        /// <summary>
        /// 从当前位置提取数学三角函数符sin,cos,tan,cot
        /// </summary>
        string ExtractMathSignFromCurrent();
    }
}

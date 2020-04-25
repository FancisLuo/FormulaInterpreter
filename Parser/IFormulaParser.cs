using MyInterpreter.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Parser
{
    /// <summary>
    /// 数学算式解析器，得到算式的表达式树
    /// </summary>
    public interface IFormulaParser
    {
        /// <summary>
        /// 构建表达式树
        /// </summary>
        IExpression ConstructExpression();
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Parser
{
    public enum Token
    {
        Illegal,    // 非法
        Plus,       // 加（正号）
        Sub,        // 减（负号）
        Mul,        // 乘
        Div,        // 除
        OParen,     // 左括号 open parenthesis
        CParen,     // 右括号 closed parenthesis
        Double,     // 数字
        Param,      // 参数
        Sin,        // Sine
        Cos,        // Cos
        Tan,        // Tan
        Cot,        // Cot
        Random,     // Random
        Comma,      // 逗号
        Null        // 结束
    }
}

using MyInterpreter.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Builder
{
   public interface IExpressionBuilder
    {
        IExpression GetExpression();
    }
}

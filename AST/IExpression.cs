using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public interface IExpression
    {
        double Evaluate(Context context);
    }
}

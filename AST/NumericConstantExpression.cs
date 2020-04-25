using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public class NumericConstantExpression : IExpression
    {
        private readonly double numericValue;

        public NumericConstantExpression(double value)
        {
            numericValue = value;
        }

        public double Evaluate(Context context)
        {
            return numericValue;
        }
    }
}

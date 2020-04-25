using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public class SinExpression : IExpression
    {
        private IExpression expression;

        public SinExpression(IExpression expression)
        {
            this.expression = expression
                ?? throw new ArgumentNullException(nameof(expression));
        }

        public double Evaluate(Context context)
        {
            var value = expression.Evaluate(context);
            return Math.Sin(value);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public class TanExpression : IExpression
    {
        private IExpression expression;

        public TanExpression(IExpression expression)
        {
            this.expression = expression
                ?? throw new ArgumentNullException(nameof(expression));
        }

        public double Evaluate(Context context)
        {
            var value = expression.Evaluate(context);
            return Math.Tanh(value);
        }
    }
}

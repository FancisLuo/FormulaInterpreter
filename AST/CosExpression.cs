using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public class CosExpression : IExpression
    {
        private IExpression expression;

        public CosExpression(IExpression expression)
        {
            this.expression = expression
                ?? throw new ArgumentNullException(nameof(expression));
        }

        public double Evaluate(Context context)
        {
            var value = expression.Evaluate(context);
            return Math.Cos(value);
        }
    }
}

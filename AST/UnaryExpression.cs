using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public class UnaryExpression : IExpression
    {
        private IExpression expression;
        private OperationType operation;

        public UnaryExpression(IExpression expression, OperationType operation)
        {
            if(expression == null ||
               (operation != OperationType.Minus && operation != OperationType.Plus))
            {
                throw new ArgumentException();
            }

            this.expression = expression;
            this.operation = operation;
        }

        public double Evaluate(Context context)
        {
            return operation switch
            {
                OperationType.Plus => expression.Evaluate(context),
                OperationType.Minus => -expression.Evaluate(context),
                _ => double.NaN,
            };
        }
    }
}

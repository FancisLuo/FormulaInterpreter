using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public class BinaryExpression : IExpression
    {
        private IExpression leftExpression;
        private IExpression rightExpression;
        private OperationType operation;

        public BinaryExpression(IExpression left,
                                IExpression right,
                                OperationType operationType)
        {
            if(left == null ||
               right == null ||
               operationType == OperationType.Illegal)
            {
                throw new ArgumentNullException();
            }

            leftExpression = left;
            rightExpression = right;
            operation = operationType;
        }

        public double Evaluate(Context context)
        {
            return operation switch
            {
                OperationType.Plus => leftExpression.Evaluate(context) + rightExpression.Evaluate(context),
                OperationType.Minus => leftExpression.Evaluate(context) - rightExpression.Evaluate(context),
                OperationType.Mul => leftExpression.Evaluate(context) * rightExpression.Evaluate(context),
                OperationType.Div => leftExpression.Evaluate(context) / rightExpression.Evaluate(context),
                _ => double.NaN,
            };
        }
    }
}

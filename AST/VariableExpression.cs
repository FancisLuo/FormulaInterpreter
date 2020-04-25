using System;
using System.Collections.Generic;
using System.Text;

using MyInterpreter.Exceptions;

namespace MyInterpreter.AST
{
    public class VariableExpression : IExpression
    {
        private string parameterKey;

        public VariableExpression(string paramKey)
        {
            if(string.IsNullOrEmpty(paramKey))
            {
                throw new ArgumentNullException(nameof(paramKey));
            }

            parameterKey = paramKey;
        }

        public double Evaluate(Context context)
        {
            if(context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.GetParameterValue(parameterKey, out var result))
            {
                return result;
            }
            else
            {
                throw new ParameterNotFoundException($"Not found parameter key: {parameterKey}");
            }
        }
    }
}

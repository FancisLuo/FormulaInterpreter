using MyInterpreter.AST;
using MyInterpreter.Parser;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Builder
{
    public class ExpressionBuilder : IExpressionBuilder
    {
        private IFormulaParser formulaParser;

        public ExpressionBuilder(string expressionString)
        {
            if(string.IsNullOrEmpty(expressionString))
            {
                throw new ArgumentNullException(nameof(expressionString));
            }

            formulaParser = new FormulaParser(expressionString);
        }

        public IExpression GetExpression()
        {
            return formulaParser.ConstructExpression();
        }
    }
}

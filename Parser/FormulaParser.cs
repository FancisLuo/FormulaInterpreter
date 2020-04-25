using MyInterpreter.AST;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.Parser
{
    /// <summary>
    /// 根据词法分析器<see cref="LexicalAnalyzer"/>得到的标签构建表达式树
    /// </summary>
    public class FormulaParser : IFormulaParser
    {
        private readonly IExpressionAccessor expressionAccessor;
        private readonly ILexicalAnalyzer lexicalAnalyzer;

        private Token currentToken;

        public FormulaParser(string formula)
        {
            if (string.IsNullOrEmpty(formula))
            {
                throw new ArgumentNullException(nameof(formula));
            }

            expressionAccessor = new ExpressionAccessor(formula);
            lexicalAnalyzer = new LexicalAnalyzer(expressionAccessor);
        }

        /*
         * expression->expression+term|expression-term|term
         * term->term x factor|term/factor|factor
         * factor->digit|(expression)
         */

        public IExpression ConstructExpression()
        {
            currentToken = lexicalAnalyzer.GetCurrentToken();
            return DoExpression();
        }

        private IExpression DoExpression()
        {
            Token old;
            var expression = DoTerm();

            while (currentToken == Token.Plus || currentToken == Token.Sub)
            {
                old = currentToken;
                currentToken = lexicalAnalyzer.GetCurrentToken();
                var e1 = DoExpression();

                expression =
                    new BinaryExpression(expression,
                                         e1,
                                         old == Token.Plus ? OperationType.Plus : OperationType.Minus);
            }
            return expression;
        }

        private IExpression DoTerm()
        {
            Token old;
            var expression = DoFactor();

            while (currentToken == Token.Mul || currentToken == Token.Div)
            {
                old = currentToken;
                currentToken = lexicalAnalyzer.GetCurrentToken();

                var e1 = DoTerm();
                expression =
                    new BinaryExpression(expression,
                                         e1,
                                         old == Token.Mul ? OperationType.Mul : OperationType.Div);
            }

            return expression;
        }

        private IExpression DoFactor()
        {
            if (currentToken == Token.Double)
            {
                var expression =
                    new NumericConstantExpression(lexicalAnalyzer.CurrentValue);
                currentToken = lexicalAnalyzer.GetCurrentToken();
                return expression;
            }
            else if (currentToken == Token.Param)
            {
                var expression = new VariableExpression(lexicalAnalyzer.CurrentParameter);
                currentToken = lexicalAnalyzer.GetCurrentToken();
                return expression;
            }
            else if (currentToken == Token.Sin ||
                     currentToken == Token.Cos ||
                     currentToken == Token.Tan ||
                     currentToken == Token.Cot)
            {
                var old = currentToken;
                currentToken = lexicalAnalyzer.GetCurrentToken();
                if (currentToken != Token.OParen)
                {
                    throw new Exception("Illegal Token");
                }

                currentToken = lexicalAnalyzer.GetCurrentToken();
                var expression = DoExpression();
                if (currentToken != Token.CParen)
                {
                    throw new Exception("Missing Closeing Parenthesis\n");
                }

                if (old == Token.Cos)
                {
                    expression = new CosExpression(expression);
                }
                else
                {
                    expression = new SinExpression(expression);
                }
                currentToken = lexicalAnalyzer.GetCurrentToken();

                return expression;
            }
            else if (currentToken == Token.OParen)
            {
                currentToken = lexicalAnalyzer.GetCurrentToken();
                var expression = DoExpression();
                if (currentToken != Token.CParen)
                {
                    throw new Exception("Missing Closing Parenthesis\n");
                }
                currentToken = lexicalAnalyzer.GetCurrentToken();

                return expression;
            }
            else if (currentToken == Token.Plus || currentToken == Token.Sub)
            {
                var old = currentToken;
                currentToken = lexicalAnalyzer.GetCurrentToken();
                var expression = DoFactor();

                expression = new UnaryExpression(expression,
                    old == Token.Plus ? OperationType.Plus : OperationType.Minus);

                return expression;

            }
            else if(currentToken == Token.Random) // random(1,2)
            {
                currentToken = lexicalAnalyzer.GetCurrentToken();
                if (currentToken != Token.OParen)
                {
                    throw new Exceptions.IllegalTokenException(
                        "Illegal Token: Need Open Parenthesis!");
                }

                currentToken = lexicalAnalyzer.GetCurrentToken();
                if(currentToken != Token.Double)
                {
                    throw new Exceptions.IllegalTokenException(
                        "Illegal token: Need Double Token 1!");
                }
                var minValue = lexicalAnalyzer.CurrentValue;

                currentToken = lexicalAnalyzer.GetCurrentToken();
                if(currentToken != Token.Comma)
                {
                    throw new Exceptions.IllegalTokenException(
                        "Illegal token: Need Comma Token!");
                }

                currentToken = lexicalAnalyzer.GetCurrentToken();
                if (currentToken != Token.Double)
                {
                    throw new Exceptions.IllegalTokenException(
                        "Illegal token: Need Double Token 2!");
                }
                var maxValue = lexicalAnalyzer.CurrentValue;

                currentToken = lexicalAnalyzer.GetCurrentToken();
                if (currentToken != Token.CParen)
                {
                    throw new Exceptions.IllegalTokenException(
                        "Missing Closeing Parenthesis!");
                }

                IExpression expression = new RandomExpression((int)minValue,
                                                              (int)maxValue);
                currentToken = lexicalAnalyzer.GetCurrentToken();

                return expression;
            }
            else
            {
                throw new Exceptions.IllegalTokenException(
                    $"Illegal Token: {currentToken}!");
            }
        }
    }
}

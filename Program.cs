using MyInterpreter.AST;
using MyInterpreter.Builder;
using System;

namespace MyInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            Context context = new Context();
            context.AddParameter("c", 1.2);

            string expressionString = "-(1*$c+(2+3)*4-5) + sin(15) + random(1, 10) + 20";

            IExpressionBuilder builder = new ExpressionBuilder(expressionString);
            IExpression expression = builder.GetExpression();
            var value = expression.Evaluate(context);
            Console.WriteLine($"get value = {value}");

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}

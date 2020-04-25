using System;
using System.Collections.Generic;
using System.Text;

namespace MyInterpreter.AST
{
    public class RandomExpression : IExpression
    {
        private int minValue;
        private int maxValue;

        private Random random = new Random();

        public RandomExpression(int minValue, int maxValue)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
        }

        public double Evaluate(Context context)
        {
            return random.Next(minValue, maxValue);
        }
    }
}

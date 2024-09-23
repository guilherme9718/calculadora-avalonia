using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain.MathExpressions
{
    public class LiteralMathExpression : MathExpression
    {
        public LiteralMathExpression(double value) 
        {
            Value = value;
        }
        public double Value { get; set; }
        public override double Calculate() => Value;
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Calculadora.Domain.Operators;

namespace Calculadora.Domain.MathExpressions
{
    public class BinaryMathExpression : MathExpression
    {
        public BinaryMathExpression(Operator @operator, MathExpression expr1, MathExpression expr2)
        {
            Operator = @operator;
            Expression1 = expr1;
            Expression2 = expr2;
        }

        public Operator Operator { get; set; }   
        public MathExpression Expression1 { get; set; }   
        public MathExpression Expression2 { get; set; }

        public override double Calculate()
        {
            return Operator.Resolve(Expression1.Calculate(), Expression2.Calculate());
        }
        public override string ToString()
        {
            return $"({Expression1.ToString()} {Operator.ToString()} {Expression2.ToString()})";
        }
    }
}
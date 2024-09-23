using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain
{
    public class Calculator
    {
        public double Calculate(string expression) 
        {
            var fac = new ExpressionFactory();
            var expr = fac.BuildExpression(expression);
            return expr.Calculate();
        }
    }
}
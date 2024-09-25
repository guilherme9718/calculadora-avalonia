using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain
{
    public class Calculator
    {
        private readonly ExpressionFactory _factory;
        public Calculator(ExpressionFactory factory)
        {
            _factory = factory;
        }
        public double Calculate(string expression) 
        {
            var expr = _factory.BuildExpression(expression);
            return expr.Calculate();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain.MathExpressions
{
    public abstract class MathExpression : IMathNode
    {
        public abstract double Calculate();
    }
}
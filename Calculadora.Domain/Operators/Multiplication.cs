using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain.Operators
{
    public class Multiplication : Operator
    {
        public override int Priority => 4;

        public override double Resolve(double value1, double value2)
        {
            return value1 * value2;
        }
        public override string ToString()
        {
            return "*";
        }
    }
}
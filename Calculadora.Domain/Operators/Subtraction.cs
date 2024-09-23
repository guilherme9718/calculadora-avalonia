using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain.Operators
{
    public class Subtracao : Operator
    {
        public override int Priority => 2;

        public override double Resolve(double value1, double value2)
        {
            return value1 - value2;
        }
        public override string ToString()
        {
            return "-";
        }
    }
}
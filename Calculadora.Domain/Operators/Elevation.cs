using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain.Operators
{
    public class Elevation : Operator
    {
        public override int Priority => 5;

        public override double Resolve(double value1, double value2)
        {
            return (double)Math.Pow((double)value1,  (double)value2);
        }
        public override string ToString()
        {
            return "^";
        }
    }
}
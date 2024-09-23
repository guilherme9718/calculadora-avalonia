using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain.Operators
{
    public class Division : Operator
    {
        public override int Priority => 4;

        public override double Resolve(double value1, double value2)
        {
            if (value2 == 0)
            {
                if (value1 > 0)
                    return double.PositiveInfinity;
                else
                    return double.NegativeInfinity;
            }

            return value1 / value2;
        }
        public override string ToString()
        {
            return "/";
        }
    }
}
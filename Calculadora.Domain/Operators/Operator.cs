using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain.Operators
{
    public abstract class Operator : IMathNode
    {
        public abstract int Priority { get; }
        public abstract double Resolve(double value1, double value2);

        public static Dictionary<string, Func<Operator>> OperatorDict  { get; } = new()
        {
            {"+", () => new Sum()},
            {"-", () => new Subtracao()},
            {"*", () => new Multiplication()},
            {"/", () => new Division()},
            {"^", () => new Elevation()},
        };
        
    }
}
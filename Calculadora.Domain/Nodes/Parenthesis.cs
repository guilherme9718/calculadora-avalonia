using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Calculadora.Domain.Nodes
{
    public class Parenthesis : IMathNode
    {
        public Parenthesis(bool open)
        {  
            Open = open;
        }

        public bool Open { get; set; }
    }
}
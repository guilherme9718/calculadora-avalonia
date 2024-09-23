using System.Globalization;
using System.Text.RegularExpressions;
using Calculadora.Domain.MathExpressions;
using Calculadora.Domain.Nodes;
using Calculadora.Domain.Operators;


namespace Calculadora.Domain
{
    public partial class ExpressionFactory
    {
        private ExpressionStringSplitter _splitter = new();
        private ExpressionPostfixBuilder _postfixBuilder = new();

        public MathExpression BuildExpression(string expr)
        {
            if (string.IsNullOrWhiteSpace(expr))
                return new LiteralMathExpression(0);
            var splited = _splitter.SplitString(expr);
            var postFix = _postfixBuilder.ToPostfix(splited);
            return CreateExpression(postFix);
        }

        private MathExpression CreateExpression(IEnumerable<IMathNode> postfix) 
        {
            var stack = new Stack<IMathNode>();
            foreach(var node in postfix) 
            {
                if(node is MathExpression) 
                {
                    stack.Push(node);   
                }
                else if(node is Operator @operator) 
                {
                    var n2 = stack.Pop() as MathExpression;
                    var n1 = stack.Pop() as MathExpression;
                    stack.Push(new BinaryMathExpression(@operator, n1!, n2!));
                }
            }

            return (MathExpression)stack.Pop();
        }
    }
}
using System.Globalization;
using System.Text.RegularExpressions;
using Calculadora.Domain.MathExpressions;
using Calculadora.Domain.Nodes;
using Calculadora.Domain.Operators;


namespace Calculadora.Domain
{
    public partial class ExpressionFactory
    {
        public MathExpression BuildExpression(string expr)
        {
            if (string.IsNullOrWhiteSpace(expr))
                return new LiteralMathExpression(0);
            var splited = SplitString(expr);
            var postFix = ToPostfix(splited);
            return CreateExpression(postFix);
        }

        private IEnumerable<IMathNode> SplitString(string expr)
        {
            var literals = LiteralRegex()
                .Matches(expr)
                .Select(v => v.Value);
            var literalsQueue = new Queue<string>(literals);
            
            var exprLiteralsFormated = LiteralRegex().Replace(expr, "%lf");
            var split = OperationsRegex().Split(exprLiteralsFormated)
                .Where(v => !string.IsNullOrWhiteSpace(v))
                .Select(v => v.Trim());
            foreach (var token in split)
            {
                if(token == "%lf") 
                {
                    var parsedToken = double.Parse(literalsQueue.Dequeue(), CultureInfo.InvariantCulture);
                    yield return new LiteralMathExpression(parsedToken);
                }
                else if(Operator.OperatorDict.TryGetValue(token, out var func)) 
                {
                    yield return func.Invoke();
                }
                else if(token == "(") 
                {
                    yield return new Parenthesis(true);
                }
                else if(token == ")") 
                {
                    yield return new Parenthesis(false);
                }
            }
        }

        private IEnumerable<IMathNode> ToPostfix(IEnumerable<IMathNode> expr)
        {
            var result = new LinkedList<IMathNode>();
            var stack = new Stack<IMathNode>();

            foreach(var node in expr) 
            {
                if(node is LiteralMathExpression literal) 
                {
                    result.AddLast(literal);
                }
                else if (node is Operator op) 
                {
                    while(stack.Count != 0
                    && stack.Peek() is Operator topOp
                    && topOp.Priority >= op.Priority) 
                    {
                        result.AddLast(stack.Pop());
                    }
                    stack.Push(node);
                }
                else if (node is Parenthesis parenthesis) 
                {
                    if(parenthesis.Open)
                        stack.Push(node);
                    else 
                    {
                        while(stack.Count != 0 
                        && stack.Peek() is not Parenthesis) 
                        {
                            result.AddLast(stack.Pop());
                        }
                        stack.Pop();
                    }
                }
            }

            while(stack.Count != 0) 
            {
                result.AddLast(stack.Pop());
            }

            return result;
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
        [GeneratedRegex("(((^[-]?)|((?<=\\()[-]?))?[0-9]*[.]?[0-9]+)")]
        private static partial Regex LiteralRegex();

        [GeneratedRegex("^[0-9]+$")]
        private static partial Regex IsNumberRegex();
        [GeneratedRegex(@"(%lf)|([\+\-\*\/\^(\)])")]
        private static partial Regex OperationsRegex();
    }
}
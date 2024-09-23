using Calculadora.Domain.MathExpressions;
using Calculadora.Domain.Nodes;
using Calculadora.Domain.Operators;

namespace Calculadora.Domain;

public class ExpressionPostfixBuilder
{
    public IEnumerable<IMathNode> ToPostfix(IEnumerable<IMathNode> expr)
    {
        var result = new LinkedList<IMathNode>();
        var stack = new Stack<IMathNode>();

        foreach (var node in expr)
        {
            if (node is LiteralMathExpression literal)
            {
                result.AddLast(literal);
            }
            else if (node is Operator op)
            {
                while (stack.Count != 0
                       && stack.Peek() is Operator topOp
                       && topOp.Priority >= op.Priority)
                {
                    result.AddLast(stack.Pop());
                }

                stack.Push(node);
            }
            else if (node is Parenthesis parenthesis)
            {
                if (parenthesis is OpenParenthesis)
                    stack.Push(node);
                else
                {
                    while (stack.Count != 0
                           && stack.Peek() is not Parenthesis)
                    {
                        result.AddLast(stack.Pop());
                    }

                    stack.Pop();
                }
            }
        }

        while (stack.Count != 0)
        {
            result.AddLast(stack.Pop());
        }

        return result;
    }
}
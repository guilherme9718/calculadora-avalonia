using Calculadora.Domain.MathExpressions;
using Calculadora.Domain.Nodes;
using Calculadora.Domain.Operators;

namespace Calculadora.Domain;

public class ExpressionValidator
{
    private ExpressionStringSplitter _splitter = new();
    private HashSet<string> _validCharacters = ["(", ")", " "];

    public ExpressionValidator()
    {
        foreach (var character in Operator.OperatorDict.Keys)
            _validCharacters.Add(character);
    }
    public bool IsValid(string expression)
    {
        return OperatorsHasTwoArguments(expression) && HasValidParentheses(expression);
    }

    public bool HasValidLiteralsAndOperators(string expression)
    {
        (var expr, var queue) = _splitter.CatchLiterals(expression);
        var exprWithoutLiterals = expr.Replace("%lf", "");
        foreach (var token in exprWithoutLiterals)
        {
            if (!_validCharacters.Contains(token.ToString()))
            {
                return false;
            }
        }
        
        return true;
    }

    public bool OperatorsHasTwoArguments(string expression)
    {
        var nodes = _splitter.SplitString(expression)
            .Where(n => n is not Parenthesis)
            .ToArray();
        for (int i=0; i<nodes.Length; i++)
        {
            if (nodes[i] is not Operator op) continue;
            
            if (i == 0 || i == nodes.Length - 1)
                return false;
            if (nodes[i - 1] is not LiteralMathExpression || nodes[i + 1] is not LiteralMathExpression)
                return false;
        }

        return true;
    }
    
    public bool HasValidParentheses(string expression)
    {
        Stack<char> stack = new Stack<char>();
        foreach (var c in expression.Where(c => c is '(' or ')'))
        {
            if (c == '(')
            {
                stack.Push(c);
            }
            else
            {
                if (stack.Count == 0)
                    return false;

                stack.Pop();
            }
        }
        
        return stack.Count == 0;
    }
}
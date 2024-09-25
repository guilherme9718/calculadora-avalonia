using System.Globalization;
using System.Text.RegularExpressions;
using Calculadora.Domain.MathExpressions;
using Calculadora.Domain.Nodes;
using Calculadora.Domain.Operators;

namespace Calculadora.Domain;

public partial class ExpressionStringSplitter
{
    
    [GeneratedRegex(@"(((^[-]?)|((?<=[\(\-\+\*\/\^])[-]?))?[0-9]*[.]?[0-9]+)")]
    private static partial Regex LiteralRegex();
    
    [GeneratedRegex(@"(%lf)|([\+\-\*\/\^(\)])")]
    private static partial Regex OperationsRegex();

    public (string, Queue<string>) CatchLiterals(string expr)
    {
        var literals = LiteralRegex()
            .Matches(expr)
            .Select(v => v.Value);
        var literalsQueue = new Queue<string>(literals);
            
        var exprLiteralsFormated = LiteralRegex().Replace(expr, "%lf");
        return (exprLiteralsFormated, literalsQueue);
    }
    public IEnumerable<IMathNode> SplitString(string expr)
    {
        (var exprLiteralsFormated, var literalsQueue) = CatchLiterals(expr);
        var split = OperationsRegex().Split(exprLiteralsFormated)
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .Select(v => v.Trim())
            .ToList();
        
        for (int i=0; i < split.Count; i++)
        {
            var token = split[i];
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
                if (i > 0 && (split[i-1] == "%lf" || split[i-1] == ")"))
                    yield return new Multiplication();
                yield return new OpenParenthesis();
            }
            else if(token == ")") 
            {
                yield return new CloseParenthesis();
                if (i < split.Count-1 && split[i+1] == "%lf")
                    yield return new Multiplication();
            }
        }
    }
}
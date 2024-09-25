using Calculadora.Domain;
using FluentValidation;
using MediatR;

namespace Calculadora.Application.Features.CalculatorFeature.Queries;

public class CalculateExpressionRequest : IRequest<CalculateExpressionResponse>
{
    public string Expression { get; set; }
}

public class CalculateExpressionResponse
{
    public double Result { get; set; }
}

public class CalculateExpressionHandler : IRequestHandler<CalculateExpressionRequest, CalculateExpressionResponse>
{
    CalculateExpressionValidator _validator;
    Calculator _calculator;

    public CalculateExpressionHandler(Calculator calculator, CalculateExpressionValidator validator)
    {
        _calculator = calculator;
        _validator = validator;
    }

    public Task<CalculateExpressionResponse> Handle(CalculateExpressionRequest request, CancellationToken cancellationToken)
    {
        _validator.ValidateAndThrow(request);
        var result = _calculator.Calculate(request.Expression);
        return Task.FromResult(new CalculateExpressionResponse { Result = result });
    }
}

public class CalculateExpressionValidator : AbstractValidator<CalculateExpressionRequest>
{
    public CalculateExpressionValidator(ExpressionValidator validator)
    {
        RuleFor(x => x.Expression)
            .Must(x => validator.HasValidParentheses(x))
            .WithMessage("Parênteses inválidos")
            .Must(x => validator.OperatorsHasTwoArguments(x))
            .WithMessage("Operadores precisam ter dois argumentos")
            .Must(x => validator.HasValidLiteralsAndOperators(x))
            .WithMessage("Caracter não reconhecido na expressão");
    }
}
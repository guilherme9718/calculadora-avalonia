using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using Calculadora.Application.Features.CalculatorFeature.Queries;
using Calculadora.Domain;
using FluentValidation;
using MediatR;
using ReactiveUI;

namespace Calculadora.UI.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{
    public string Greeting => "Welcome to Avalonia!";
    private string expression = "";
    public IEnumerable<string> errors = Enumerable.Empty<string>();

    public IEnumerable<string> Errors
    {
        get => errors;
        set => this.RaiseAndSetIfChanged(ref errors, value);
    }
    public string Expression
    {
        get => expression;
        set => this.RaiseAndSetIfChanged(ref expression, value);
    }

    public ICommand PrintCalculatorCommand { get; }
    public ICommand CalculateCommand { get; }
    public ICommand BackCommand { get; }
    private IMediator _mediator;
    public MainWindowViewModel(IMediator mediator)
    {
        _mediator = mediator;
        PrintCalculatorCommand = ReactiveCommand.Create<string>(input => { Expression += input; });

        CalculateCommand = ReactiveCommand.Create(Calculate);

        BackCommand = ReactiveCommand.Create(() => {
            if (Expression.Length > 0)
                Expression = Expression.Substring(0, Expression.Length - 1);
        });
    }

    private async Task Calculate()
    {
        try
        {
            Stopwatch watch = Stopwatch.StartNew();
            var response = await _mediator.Send(new CalculateExpressionRequest() {Expression = Expression});
            watch.Stop();
            Expression = response.Result.ToString(CultureInfo.InvariantCulture);
        }
        catch (ValidationException ex)
        {
            Errors = ex.Errors.Select(a => a.ErrorMessage);
            Expression = Errors.First();
        }
    }
}
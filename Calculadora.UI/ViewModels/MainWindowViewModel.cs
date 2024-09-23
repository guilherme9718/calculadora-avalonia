using System;
using System.Globalization;
using System.Windows.Input;
using Avalonia.Threading;
using Calculadora.Domain;
using ReactiveUI;

namespace Calculadora.UI.ViewModels;

public partial class MainWindowViewModel : ReactiveObject
{
    public string Greeting => "Welcome to Avalonia!";
    private string expression = "";

    public string Expression
    {
        get => expression;
        set => this.RaiseAndSetIfChanged(ref expression, value);
    }

    public ICommand PrintCalculatorCommand { get; }
    public ICommand CalculateCommand { get; }
    public ICommand BackCommand { get; }
    private Calculator _calculator = new();

    public MainWindowViewModel()
    {
        PrintCalculatorCommand = ReactiveCommand.Create<string>(input => { Expression += input; });

        CalculateCommand = ReactiveCommand.Create(() => {
            double result = _calculator.Calculate(Expression);
            Expression = result.ToString(CultureInfo.InvariantCulture);
        });

        BackCommand = ReactiveCommand.Create(() => {
            if (Expression.Length > 0)
                Expression = Expression.Substring(0, Expression.Length - 1);
        });
    }
}
// See https://aka.ms/new-console-template for more information
using Calculadora.Domain;

string? entrada = "";
do {
    Console.WriteLine("Digite a expressao matemática: ");
    entrada = Console.ReadLine();

    if(!string.IsNullOrWhiteSpace(entrada)) {
        var factory = new ExpressionFactory();
        var expr = factory.BuildExpression(entrada);
        Console.WriteLine($"O resultado da expressão é: {expr.Calculate()}");
    }
} while(!string.IsNullOrWhiteSpace(entrada));

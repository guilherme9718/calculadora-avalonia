// See https://aka.ms/new-console-template for more information

using Calculadora.Application.Features.CalculatorFeature.Queries;
using Calculadora.Domain;
using Calculadora.UI.Extensions;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddApplicationServices();
var provider = services.BuildServiceProvider();
var mediator = provider.GetRequiredService<IMediator>();

string? entrada = "";
do
{
    Console.WriteLine("Digite a expressao matemática: ");
    entrada = Console.ReadLine();
    try
    {
        var response = await mediator.Send(new CalculateExpressionRequest() {Expression = entrada});
        Console.WriteLine(response.Result);
    }
    catch (ValidationException ex)
    {
        Console.WriteLine($"Erro de validação: {string.Join(';', ex.Errors.Select(a => a.ErrorMessage))}");
    }
} while (!string.IsNullOrWhiteSpace(entrada));
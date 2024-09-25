using System.Reflection;
using Calculadora.Application.Features.CalculatorFeature.Queries;
using Calculadora.Domain;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Calculadora.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<Calculator>();
        services.AddTransient<ExpressionValidator>();
        services.AddTransient<ExpressionFactory>();
        services.AddTransient<ExpressionStringSplitter>();
        services.AddTransient<ExpressionPostfixBuilder>();
        
        services.AddValidatorsFromAssemblyContaining<CalculateExpressionValidator>();
        services.AddMediatR(options => {
            options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
    }
}
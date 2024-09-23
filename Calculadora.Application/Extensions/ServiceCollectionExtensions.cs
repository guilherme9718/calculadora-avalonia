using System.Reflection;
using Calculadora.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Calculadora.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddTransient<Calculator>();
        services.AddMediatR(options => {
            options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
        });
    }
}
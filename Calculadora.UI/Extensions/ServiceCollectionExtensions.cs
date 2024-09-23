using Calculadora.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Calculadora.UI.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddTransient<MainWindowViewModel>();
        services.AddApplicationServices();
    }
}
using Microsoft.Extensions.DependencyInjection;
using TestResults.Repositories.Implementations;
using TestResults.Repositories.Interfaces;

namespace TestResults.Repositories.Extensions;

/// <summary>
/// A szolgáltatások gyűjteményének kiterjesztései.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Hozzáadja a tesztek eredményeit kezelő adattárakat.
    /// </summary>
    /// <param name="services">A szolgáltatások gyűjteménye.</param>
    /// <returns>A szolgáltatások gyűjteménye.</returns>
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IMemoryUsageResultRepository, MemoryUsageResultRepository>();
        services.AddScoped<IRunTimeResultRepository, RunTimeResultRepository>();

        return services;
    }
}

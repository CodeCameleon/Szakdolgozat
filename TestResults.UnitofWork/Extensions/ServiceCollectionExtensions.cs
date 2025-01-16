using Microsoft.Extensions.DependencyInjection;
using TestResults.UnitofWork.Implementations;
using TestResults.UnitofWork.Interfaces;

namespace TestResults.UnitofWork.Extensions;

/// <summary>
/// A szolgáltatások gyűjteményének kiterjesztései.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Hozzáadja a tesztek eredményeit kezelő egységmunkát.
    /// </summary>
    /// <param name="services">A szolgáltatások gyűjteménye.</param>
    /// <returns>A szolgáltatások gyűjteménye.</returns>
    public static IServiceCollection AddUnitofWork(this IServiceCollection services)
    {
        services.AddScoped<ITestResultsUnitofWork, TestResultsUnitofWork>();

        return services;
    }
}

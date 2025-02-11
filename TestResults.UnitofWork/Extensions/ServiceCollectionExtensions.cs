using Microsoft.Extensions.DependencyInjection;
using TestResults.UnitOfWork.Implementations;
using TestResults.UnitOfWork.Interfaces;

namespace TestResults.UnitOfWork.Extensions;

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
    public static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        services.AddScoped<ITestResultsUnitOfWork, TestResultsUnitOfWork>();

        return services;
    }
}

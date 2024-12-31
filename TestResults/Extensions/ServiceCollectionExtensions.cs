using Microsoft.Extensions.DependencyInjection;
using TestResults.Context;
using TestResults.Implementations;
using TestResults.Interfaces;

namespace TestResults.Extensions;

/// <summary>
/// A szolgáltatások gyűjteményének kiterjesztései.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Hozzáadja a teszteredmények adatbázisának szolgáltatásait.
    /// </summary>
    /// <param name="services">A szolgáltatások gyűjteménye.</param>
    /// <returns>A szolgáltatások gyűjteménye.</returns>
    public static IServiceCollection AddTestResultsDatabase(this IServiceCollection services)
    {
        services.AddDbContext<TestResultsDbContext>();
        services.AddScoped<ITransactionManager, TransactionManager>();

        services.AddScoped<IMemoryUsageResultRepository, MemoryUsageResultRepository>();
        services.AddScoped<IRunTimeResultRepository, RunTimeResultRepository>();

        return services;
    }
}

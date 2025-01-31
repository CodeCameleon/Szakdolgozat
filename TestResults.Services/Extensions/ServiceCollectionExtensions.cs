using Microsoft.Extensions.DependencyInjection;
using TestResults.Services.Implementations;
using TestResults.Services.Interfaces;

namespace TestResults.Services.Extensions;

/// <summary>
/// A szolgáltatások gyűjteményének kiterjesztései.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Hozzáadja a tesztek eredményeit kezelő szolgáltatásokat.
    /// </summary>
    /// <param name="services">A szolgáltatások gyűjteménye.</param>
    /// <returns>A szolgáltatások gyűjteménye.</returns>
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IMemoryUsageResultService, MemoryUsageResultService>();
        services.AddScoped<IRunTimeResultService, RunTimeResultService>();
        services.AddScoped<ITestCaseService, TestCaseService>();

        return services;
    }
}

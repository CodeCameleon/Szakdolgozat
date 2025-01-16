using Microsoft.Extensions.DependencyInjection;
using Shared.Utilities.Implementations;
using Shared.Utilities.Interfaces;

namespace Shared.Utilities.Extensions;

/// <summary>
/// A szolgáltatások gyűjteményének kiterjesztései.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Hozzáadja a teszteseteket lértehozó eszközt.
    /// </summary>
    /// <param name="services">A szolgáltatások gyűjteménye.</param>
    /// <returns>A szolgáltatások gyűjteménye.</returns>
    public static IServiceCollection AddTestInputGenerator(this IServiceCollection services)
    {
        services.AddTransient<ITestInputGenerator, TestInputGenerator>();

        return services;
    }
}

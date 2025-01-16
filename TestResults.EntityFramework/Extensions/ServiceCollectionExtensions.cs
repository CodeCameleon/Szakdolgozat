using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestResults.EntityFramework.Extensions;

/// <summary>
/// A szolgáltatások gyűjteményének kiterjesztései.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Hozzáadja a tesztek eredményeit tároló adatbázis kontextust.
    /// </summary>
    /// <param name="services">A szolgáltatások gyűjteménye.</param>
    /// <param name="connectionString">Az adatbázis kapcsolódási karakterlánc.</param>
    /// <returns>A szolgáltatások gyűjteménye.</returns>
    public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<TestResultsDbContext>(options =>
            options.UseSqlite(connectionString)
        );

        return services;
    }
}

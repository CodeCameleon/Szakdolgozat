using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestResults.Context;
using TestResults.Extensions;

namespace AlgorithmTest;

/// <summary>
/// Az adatbázis kapcsolatot előkészítő osztály.
/// </summary>
[SetUpFixture]
internal class DatabaseSetup
{
    /// <summary>
    /// Az adatbázis kapcsolatot tároló adattag.
    /// </summary>
    public static IServiceProvider ServiceProvider { get; private set; }

    /// <summary>
    /// Az adatbázis kapcsolatot előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void GlobalSetUp()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddTestResultsDatabase();
        ServiceProvider = services.BuildServiceProvider();

        using IServiceScope serviceScope = ServiceProvider.CreateScope();
        TestResultsDbContext context = serviceScope.ServiceProvider
            .GetRequiredService<TestResultsDbContext>();
        context.Database.Migrate();
    }

    /// <summary>
    /// Az adatbázis kapcsolatot lezáró függvény.
    /// </summary>
    [OneTimeTearDown]
    public void GlobalTearDown()
    {
        foreach (IDisposable disposable in ServiceProvider.GetServices<IDisposable>())
        {
            disposable.Dispose();
        }

        if (ServiceProvider is IDisposable disposableServiceProvider)
        {
            disposableServiceProvider.Dispose();
        }
    }
}

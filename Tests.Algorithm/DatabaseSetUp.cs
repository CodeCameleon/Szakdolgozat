using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Constants;
using TestResults.EntityFramework;
using TestResults.EntityFramework.Extensions;
using TestResults.Repositories.Extensions;
using TestResults.Services.Extensions;
using TestResults.UnitofWork.Extensions;

namespace Tests.Algorithm;

/// <summary>
/// Az adatbázis kapcsolatot előkészítő osztály.
/// </summary>
[SetUpFixture]
internal class DatabaseSetUp
{
    /// <summary>
    /// Az adatbázis kapcsolatot tároló adattag.
    /// </summary>
    public static IServiceProvider ServiceProvider { get; private set; }

    /// <summary>
    /// Az adatbázis kapcsolatot előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        IServiceCollection services = new ServiceCollection();
        services.AddDbContext(GlobalConfiguration.DefaultConnection);
        services.AddRepositories();
        services.AddUnitofWork();
        services.AddServices();
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
    public void OneTimeTearDown()
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

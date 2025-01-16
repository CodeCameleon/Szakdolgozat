using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestResults.EntityFramework;
using TestResults.EntityFramework.Extensions;
using TestResults.Repositories.Extensions;
using TestResults.Services.Extensions;
using TestResults.UnitofWork.Extensions;

namespace Base.Test;

/// <summary>
/// Egy teszteseteket tartalmazó absztrakt ősosztály.
/// </summary>
[TestFixture]
public abstract class BaseTestFixture
{
    /// <summary>
    /// Az adatbázis kapcsolatot tároló adattag.
    /// </summary>
    protected IServiceProvider _serviceProvider;

    /// <summary>
    /// Az adatbázis kapcsolatot előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    protected void OneTimeSetUp()
    {
        IServiceCollection services = new ServiceCollection();
        //services.AddDbContext(); // TODO Ide kell majd a kapcsolódási karakterlánc.
        services.AddRepositories();
        services.AddUnitofWork();
        services.AddServices();
        _serviceProvider = services.BuildServiceProvider();

        using IServiceScope serviceScope = _serviceProvider.CreateScope();
        TestResultsDbContext context = serviceScope.ServiceProvider.GetRequiredService<TestResultsDbContext>();
        context.Database.Migrate();
    }

    /// <summary>
    /// Az adatbázis kapcsolatot lezáró függvény.
    /// </summary>
    [OneTimeTearDown]
    protected void OneTimeTearDown()
    {
        foreach (IDisposable disposable in _serviceProvider.GetServices<IDisposable>())
        {
            disposable.Dispose();
        }

        if (_serviceProvider is IDisposable disposableServiceProvider)
        {
            disposableServiceProvider.Dispose();
        }
    }
}

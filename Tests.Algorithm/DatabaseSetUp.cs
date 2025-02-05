using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Algorithms.Implementations;
using Shared.Algorithms.Interfaces;
using Shared.Constants;
using Shared.Utilities.Extensions;
using TestResults.Dtos;
using TestResults.EntityFramework;
using TestResults.EntityFramework.Extensions;
using TestResults.Repositories.Extensions;
using TestResults.Services.Extensions;
using TestResults.Services.Interfaces;
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
    /// A tesztelendő algoritmusokat tároló adattag.
    /// </summary>
    public static List<IEncryptionAlgorithm> TestAlgorithms { get; private set; }

    /// <summary>
    /// A teszteseteket tároló adattag.
    /// </summary>
    public static List<TestCaseDto> TestCases { get; private set; }

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
        services.AddTestInputGenerator();
        ServiceProvider = services.BuildServiceProvider();

        using (TestResultsDbContext context = ServiceProvider.GetRequiredService<TestResultsDbContext>())
        {
            context.Database.Migrate();
        }

        TestAlgorithms =
        [
            new AesAlgorithm(),
            new DesAlgorithm(),
            new MathCryptAlgorithm()
        ];

        ITestCaseService testCaseService = ServiceProvider.GetRequiredService<ITestCaseService>();
        TestCases = testCaseService.GetEnabledDtoListAsync().Result;
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

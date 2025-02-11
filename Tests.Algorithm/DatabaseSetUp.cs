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
    /// A szolgáltatások konténerét tároló adattag.
    /// </summary>
    private static readonly Lazy<IServiceProvider> _serviceProvider = new(new ServiceCollection()
        .AddDbContext(GlobalConfiguration.DefaultConnection)
        .AddRepositories()
        .AddUnitofWork()
        .AddServices()
        .AddTestInputGenerator()
        .BuildServiceProvider()
    );

    /// <summary>
    /// A szolgáltatások konténere.
    /// </summary>
    public static IServiceProvider ServiceProvider => _serviceProvider.Value;

    /// <summary>
    /// Lekéri a tesztelendő algoritmusokat.
    /// </summary>
    /// <returns>A tesztelendő algoritmusok listája.</returns>
    public static List<IEncryptionAlgorithm> GetTestAlgorithms() =>
    [
        new AesAlgorithm(),
        new DesAlgorithm(),
        new MathCryptAlgorithm()
    ];

    /// <summary>
    /// Lekéri a teszteseteket az adatbázisból.
    /// </summary>
    /// <returns>A tesztesetek adatátmeneti objektumainak listája.</returns>
    public static async Task<List<TestCaseDto>> GetTestCases()
    {
        using IServiceScope scope = ServiceProvider.CreateScope();
        ITestCaseService testCaseService = scope.ServiceProvider.GetRequiredService<ITestCaseService>();
        return await testCaseService.GetEnabledDtoListAsync();
    }

    /// <summary>
    /// Az adatbázis kapcsolatot előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        using IServiceScope scope = ServiceProvider.CreateScope();
        TestResultsDbContext context = scope.ServiceProvider.GetRequiredService<TestResultsDbContext>();
        context.Database.Migrate();
    }

    /// <summary>
    /// Az adatbázis kapcsolatot lezáró függvény.
    /// </summary>
    [OneTimeTearDown]
    public void OneTimeTearDown()
    {
        if (ServiceProvider is IDisposable disposableServiceProvider)
        {
            disposableServiceProvider.Dispose();
        }
    }
}

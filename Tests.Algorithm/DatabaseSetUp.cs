using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shared.Algorithms.Extensions;
using Shared.Constants;
using Shared.Enums;
using System.Text;
using TestResults.Dtos;
using TestResults.EntityFramework;
using TestResults.EntityFramework.Extensions;
using TestResults.Repositories.Extensions;
using TestResults.Services.Extensions;
using TestResults.Services.Interfaces;
using TestResults.UnitOfWork.Extensions;

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
        .AddUnitOfWork()
        .AddServices()
        .BuildServiceProvider()
    );

    /// <summary>
    /// A szolgáltatások konténere.
    /// </summary>
    public static IServiceProvider ServiceProvider => _serviceProvider.Value;

    /// <summary>
    /// Létrehozza a bemenetet a részleges bemenet és a méret alapján.
    /// </summary>
    /// <param name="partialInput">A részleges bemenet.</param>
    /// <param name="size">A méret.</param>
    /// <returns>A kész bemenet.</returns>
    public static string CreateInput(string partialInput, int size)
    {
        Encoding encoding = Encoding.UTF8;
        int partialSize = encoding.GetByteCount(partialInput);

        if (partialSize > size)
        {
            throw new ArgumentException(ErrorMessages.TestCaseInputCantBeBiggerThenSize);
        }

        if (partialSize == size)
        {
            return partialInput;
        }

        StringBuilder result = new();
        int currentSize = 0;

        int fullRepeats = size / partialSize;
        result.Append(string.Concat(Enumerable.Repeat(partialInput, fullRepeats)));
        currentSize = fullRepeats * partialSize;

        int remainingBytes = size - currentSize;
        if (remainingBytes == 0)
        {
            return result.ToString();
        }

        int[] charByteSizes = partialInput.Select(c => encoding.GetByteCount([c])).ToArray();
        for (int i = 0; i < partialInput.Length; i++)
        {
            if (remainingBytes < charByteSizes[i])
            {
                break;
            }

            result.Append(partialInput[i]);
            remainingBytes -= charByteSizes[i];
        }

        return result.ToString();
    }

    /// <summary>
    /// Lekéri a tesztelendő algoritmusokat.
    /// </summary>
    /// <returns>A tesztelendő algoritmusok típusainak listája.</returns>
    public static List<Type> GetTestAlgorithms()
    {
        return Enum.GetValues<EAlgorithmName>().Select(e => e.GetImplementation()).ToList();
    }

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

using Microsoft.Extensions.DependencyInjection;
using Shared.Algorithms.Interfaces;
using Shared.Utilities.Interfaces;
using TestResults.Dtos;
using TestResults.Services.Interfaces;

namespace Tests.Algorithm;

/// <summary>
/// A titkosító algoritmusok memóriahasználatát vizsgáló tesztesetek.
/// </summary>
[TestFixture]
[NonParallelizable]
internal class MemoryUsageTests
{
    /// <summary>
    /// A memóriahasználat eredményeket kezelő szolgáltatást tároló adattag.
    /// </summary>
    private IMemoryUsageResultService _memoryUsageResultService;

    /// <summary>
    /// Az aktuális teszt futtatási környezetéhez tartozó szolgáltatás példányokat tároló adattag.
    /// </summary>
    private IServiceScope _serviceScope;

    /// <summary>
    /// A teszteseteket lértehozó eszközt tároló adattag.
    /// </summary>
    private ITestInputGenerator _testInputGenerator;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        _serviceScope = DatabaseSetUp.ServiceProvider.CreateScope();
        _memoryUsageResultService = _serviceScope.ServiceProvider.GetRequiredService<IMemoryUsageResultService>();
        _testInputGenerator = _serviceScope.ServiceProvider.GetRequiredService<ITestInputGenerator>();
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [TearDown]
    public void TearDown()
    {
        _serviceScope.Dispose();
    }

    /// <summary>
    /// Az összes teszteset memóriahasználatát vizsgáló teszt.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus.</param>
    /// <param name="testCase">A teszteset.</param>
    [Test]
    public async Task All([ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.GetTestAlgorithms))] IEncryptionAlgorithm algorithm,
        [ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.GetTestCases))] TestCaseDto testCase)
    {
        string input = _testInputGenerator.CreateInput(testCase.Input, testCase.Size);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryBefore = GC.GetTotalMemory(true);

        string cipherText = algorithm.Encrypt(input);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryBetween = GC.GetTotalMemory(true);

        string plainText = algorithm.Decrypt(cipherText);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        long memoryAfter = GC.GetTotalMemory(true);

        long encryptionMemoryUsage = Math.Max(0, memoryBetween - memoryBefore);
        long decryptionMemoryUsage = Math.Max(0, memoryAfter - memoryBetween);

        await _memoryUsageResultService.CreateAsync(new MemoryUsageResultDto
        {
            AlgorithmName = algorithm.AlgorithmName,
            AlgorithmType = algorithm.AlgorithmType,
            TestCase = testCase,
            IsSuccessful = plainText.Equals(input),
            EncryptionMemoryUsage = encryptionMemoryUsage,
            DecryptionMemoryUsage = decryptionMemoryUsage
        });

        Assert.That(plainText, Is.EqualTo(input));
    }
}

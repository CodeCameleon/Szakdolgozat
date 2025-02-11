using Microsoft.Extensions.DependencyInjection;
using Shared.Algorithms.Interfaces;
using Shared.Utilities.Interfaces;
using System.Diagnostics;
using TestResults.Dtos;
using TestResults.Services.Interfaces;

namespace Tests.Algorithm;

/// <summary>
/// A titkosító algoritmusok futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
[NonParallelizable]
internal class RunTimeTests
{
    /// <summary>
    /// A futási idő eredményeket kezelő szolgáltatást tároló adattag.
    /// </summary>
    private IRunTimeResultService _runTimeResultService;

    /// <summary>
    /// Az aktuális teszt futtatási környezetéhez tartozó szolgáltatás példányokat tároló adattag.
    /// </summary>
    private IServiceScope _serviceScope;

    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private Stopwatch _stopwatch;

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
        _runTimeResultService = _serviceScope.ServiceProvider.GetRequiredService<IRunTimeResultService>();
        _testInputGenerator = _serviceScope.ServiceProvider.GetRequiredService<ITestInputGenerator>();
        _stopwatch = new();
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
    /// Az összes teszteset futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus.</param>
    /// <param name="testCase">A teszteset.</param>
    [Test]
    public async Task All([ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.GetTestAlgorithms))] IEncryptionAlgorithm algorithm,
        [ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.GetTestCases))] TestCaseDto testCase)
    {
        string input = _testInputGenerator.CreateInput(testCase.Input, testCase.Size);

        _stopwatch.Restart();
        string cipherText = algorithm.Encrypt(input);
        _stopwatch.Stop();

        TimeSpan timeToEncrypt = _stopwatch.Elapsed;

        _stopwatch.Restart();
        string plainText = algorithm.Decrypt(cipherText);
        _stopwatch.Stop();

        TimeSpan timeToDecrypt = _stopwatch.Elapsed;

        await _runTimeResultService.CreateAsync(new RunTimeResultDto
        {
            AlgorithmName = algorithm.AlgorithmName,
            AlgorithmType = algorithm.AlgorithmType,
            TestCase = testCase,
            IsSuccessful = plainText.Equals(input),
            TimeToEncrypt = timeToEncrypt.TotalMilliseconds,
            TimeToDecrypt = timeToDecrypt.TotalMilliseconds
        });

        Assert.That(plainText, Is.EqualTo(input));
    }
}

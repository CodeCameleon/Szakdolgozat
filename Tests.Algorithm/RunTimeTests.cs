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
    [OneTimeSetUp]
    public void SetUp()
    {
        _runTimeResultService = DatabaseSetUp.ServiceProvider.GetRequiredService<IRunTimeResultService>();
        _stopwatch = new();
        _testInputGenerator = DatabaseSetUp.ServiceProvider.GetRequiredService<ITestInputGenerator>();
    }

    /// <summary>
    /// Az összes teszteset futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus.</param>
    /// <param name="testCase">A teszteset.</param>
    [Test]
    public void All([ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.TestAlgorithms))] IEncryptionAlgorithm algorithm,
        [ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.TestCases))] TestCaseDto testCase)
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

        _runTimeResultService.CreateAsync(new RunTimeResultDto
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

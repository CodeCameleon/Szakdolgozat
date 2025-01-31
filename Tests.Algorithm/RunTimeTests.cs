using Microsoft.Extensions.DependencyInjection;
using Shared.Algorithms.Interfaces;
using System.Diagnostics;
using TestResults.Dtos;
using TestResults.Services.Interfaces;

namespace Tests.Algorithm;

/// <summary>
/// A titkosító algoritmusok futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
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
    /// A teszteket előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void SetUp()
    {
        _runTimeResultService = DatabaseSetUp.ServiceProvider.GetRequiredService<IRunTimeResultService>();

        _stopwatch = new();
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [OneTimeTearDown]
    public void TearDown()
    {
        _runTimeResultService.Dispose();
    }

    /// <summary>
    /// Az összes teszteset futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus.</param>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test]
    public void All([ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.TestAlgorithms))] IEncryptionAlgorithm algorithm,
        [ValueSource(typeof(DatabaseSetUp), nameof(DatabaseSetUp.TestInputs))] string input)
    {
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
            Input = input,
            IsSuccessful = plainText.Equals(input),
            TimeToEncrypt = timeToEncrypt.TotalMilliseconds,
            TimeToDecrypt = timeToDecrypt.TotalMilliseconds
        });

        Assert.That(plainText, Is.EqualTo(input));
    }
}

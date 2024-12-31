using AlgorithmTest.Helpers;
using AlgorithmTest.Models;
using Microsoft.Extensions.DependencyInjection;
using TestResults.Entities;
using TestResults.Interfaces;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// A futási idő mérését végző absztrakt osztály.
/// </summary>
[TestFixture]
internal abstract class BaseRunTime<Algorithm>
    where Algorithm : IAlgorithm, new()
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private IAlgorithm _algorithm;

    /// <summary>
    /// Az adattárat tároló adattag.
    /// </summary>
    private IRunTimeResultRepository _repository;

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
        _algorithm = new Algorithm();

        _repository = DatabaseSetup.ServiceProvider
            .GetRequiredService<IRunTimeResultRepository>();

        _stopwatch = new();
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [OneTimeTearDown]
    public void TearDown()
    {
        _algorithm.Dispose();
    }

    /// <summary>
    /// A futási idő mérését végző függvény.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus.</param>
    /// <param name="input">A titkosítandó szöveg.</param>
    protected void RunTime(string input)
    {
        _stopwatch.Restart();
        string cipherText = _algorithm.Encrypt(input);
        _stopwatch.Stop();

        TimeSpan timeToEncrypt = _stopwatch.Elapsed;

        _stopwatch.Restart();
        string plainText = _algorithm.Decrypt(cipherText);
        _stopwatch.Stop();

        TimeSpan timeToDecrypt = _stopwatch.Elapsed;

        TestContext.Out.WriteLine(
            StringHelper.TimeToEncrypt(timeToEncrypt)
        );

        TestContext.Out.WriteLine(
            StringHelper.TimeToDecrypt(timeToDecrypt)
        );

        _repository.CreateAsync(new RunTimeResult
        {
            AlgorithmName = _algorithm.GetType().Name,
            Input = input,
            IsSuccessful = plainText.Equals(input),
            TimeToEncrypt = timeToEncrypt.TotalMilliseconds,
            TimeToDecrypt = timeToDecrypt.TotalMilliseconds
        });

        Assert.That(plainText, Is.EqualTo(input));
    }
}

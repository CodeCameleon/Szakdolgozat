using AlgorithmTest.Helpers;
using Base.Test;
using Base.Test.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using TestResults.Entities;
using TestResults.Services.Interfaces;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// A futási idő mérését végző absztrakt osztály.
/// </summary>
internal abstract class BaseRunTime<Algorithm> : BaseTestFixture
    where Algorithm : ISymmetricAlgorithm, new()
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private ISymmetricAlgorithm _algorithm;

    /// <summary>
    /// A szolgáltatást tároló adattag.
    /// </summary>
    private IRunTimeResultService _service;

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

        _service = _serviceProvider.GetRequiredService<IRunTimeResultService>();

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

        _service.CreateAsync(new RunTimeResult
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

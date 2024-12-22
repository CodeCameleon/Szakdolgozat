using AlgorithmTest.Helpers;
using AlgorithmTest.Models;

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
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private readonly Stopwatch _stopwatch = new();

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void SetUp()
    {
        _algorithm = new Algorithm();
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

        TestContext.Out.WriteLine(
            StringHelper.TimeToEncrypt(_stopwatch.Elapsed)
        );

        _stopwatch.Restart();
        string plainText = _algorithm.Decrypt(cipherText);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToDecrypt(_stopwatch.Elapsed)
        );

        Assert.That(plainText, Is.EqualTo(input));
    }
}

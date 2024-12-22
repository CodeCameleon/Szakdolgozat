using AlgorithmTest.Helpers;
using AlgorithmTest.Models;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// A futási idő mérését végző absztrakt osztály.
/// </summary>
internal abstract class BaseRunTime
{
    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private readonly Stopwatch _stopwatch = new();

    /// <summary>
    /// A futási idő mérését végző függvény.
    /// </summary>
    /// <param name="algorithm">A tesztelendő algoritmus.</param>
    /// <param name="input">A titkosítandó szöveg.</param>
    protected void RunTime(IAlgorithm algorithm, string input)
    {
        _stopwatch.Restart();
        string cipherText = algorithm.Encrypt(input);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToEncrypt(_stopwatch.Elapsed)
        );

        _stopwatch.Restart();
        string plainText = algorithm.Decrypt(cipherText);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToDecrypt(_stopwatch.Elapsed)
        );

        Assert.That(plainText, Is.EqualTo(input));
    }
}

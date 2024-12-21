using AlgorithmTest.Helpers;
using AlgorithmTest.Models;
using MathCrypt.Enums;
using MathCrypt.Services;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// A MathCrypt titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
internal class MathCryptRunTimeTests
{
    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private Stopwatch _stopwatch;

    /// <summary>
    /// A MathCrypt titkosító algoritmust tároló adattag.
    /// </summary>
    private MathCryptAlgorithm _mathCrpyt;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void SetUp()
    {
        _stopwatch = new Stopwatch();

        char[][] key = KeyGenService.Instance.GenerateKey(
            strength: 2,
            ECharset.Space,
            ECharset.Numbers,
            ECharset.MathSymbols,
            ECharset.Punctuations,
            ECharset.EN,
            ECharset.HU
        );

        _mathCrpyt = new(key);
    }

    /// <summary>
    /// Az egyszerű tesztesetek futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleRunTime(string input)
    {
        _stopwatch.Restart();
        string cipherText = _mathCrpyt.Encrypt(input);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToEncrypt(_stopwatch.Elapsed)
        );

        _stopwatch.Restart();
        string plainText = _mathCrpyt.Decrypt(cipherText);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToDecrypt(_stopwatch.Elapsed)
        );

        Assert.That(plainText, Is.EqualTo(input));
    }
}

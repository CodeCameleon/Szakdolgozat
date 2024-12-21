using AlgorithmTest.Models;
using MathCrypt.Enums;
using MathCrypt.Services;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// A MathCrypt titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
internal class MathCryptRunTimeTests
    : BaseRunTime
{
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
        char[][] key = KeyGenService.Instance.GenerateKey(
            strength: 2,
            ECharset.Space,
            ECharset.Numbers,
            ECharset.MathSymbols,
            ECharset.Punctuations,
            ECharset.EN,
            ECharset.HU
        );

        _mathCrpyt = new MathCryptAlgorithm(key);
    }

    /// <summary>
    /// Az egyszerű tesztesetek futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleRunTime(string input)
    {
        RunTime(_mathCrpyt, input);
    }
}

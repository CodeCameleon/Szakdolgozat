using AlgorithmTest.Models;
using MathCrypt.Enums;
using MathCrypt.Services;

namespace AlgorithmTest.MemoryUsageTests;

/// <summary>
/// A MathCrypt titkosító algoritmus memória használatát vizsgáló tesztesetek.
/// </summary>
[TestFixture]
[NonParallelizable]
internal class MathCryptMemoryUsageTests
    : BaseMemoryUsage
{
    /// <summary>
    /// A MathCrypt titkosító algoritmust tároló adattag.
    /// </summary>
    private MathCryptAlgorithm _mathCrpyt;

    /// <inheritdoc />
    [SetUp]
    public override void SetUp()
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

        base.SetUp();
    }

    /// <inheritdoc />
    [TearDown]
    public override void TearDown()
    {
        base.TearDown();
    }

    /// <summary>
    /// Az egyszerű tesztesetek memória használatát vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleMemoryUsage(string input)
    {
        MemoryUsage(_mathCrpyt, input);
    }
}

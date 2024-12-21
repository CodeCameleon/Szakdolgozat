using AlgorithmTest.Models;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// Az AES titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
internal class AesRunTimeTests
    : BaseRunTime
{
    /// <summary>
    /// Az AES titkosító algoritmust tároló adattag.
    /// </summary>
    private AESAlgorithm _aes;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void SetUp()
    {
        _aes = new();
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [OneTimeTearDown]
    public void TearDown()
    {
        _aes.Dispose();
    }

    /// <summary>
    /// Az egyszerű tesztesetek futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleRunTime(string input)
    {
        RunTime(_aes, input);
    }
}

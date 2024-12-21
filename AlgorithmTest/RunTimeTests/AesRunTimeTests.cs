using AlgorithmTest.Helpers;
using AlgorithmTest.Models;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// Az AES titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
internal class AesRunTimeTests
{
    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private Stopwatch _stopwatch;

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
        _stopwatch = new Stopwatch();

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
        _stopwatch.Restart();
        string cipherText = _aes.Encrypt(input);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToEncrypt(_stopwatch.Elapsed)
        );

        _stopwatch.Restart();
        string plainText = _aes.Decrypt(cipherText);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToDecrypt(_stopwatch.Elapsed)
        );

        Assert.That(plainText, Is.EqualTo(input));
    }
}

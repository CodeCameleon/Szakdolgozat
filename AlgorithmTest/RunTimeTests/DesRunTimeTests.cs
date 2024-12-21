using AlgorithmTest.Helpers;
using AlgorithmTest.Models;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// Az DES titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
internal class DesRunTimeTests
{
    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private Stopwatch _stopwatch;

    /// <summary>
    /// Az DES titkosító algoritmust tároló adattag.
    /// </summary>
    private DESAlgorithm _des;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void SetUp()
    {
        _stopwatch = new Stopwatch();

        _des = new();
    }

    /// <summary>
    /// A teszteket lezáró függvény.
    /// </summary>
    [OneTimeTearDown]
    public void TearDown()
    {
        _des.Dispose();
    }

    /// <summary>
    /// Az egyszerű tesztesetek futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.SimpleTestCases))]
    public void SimpleRunTime(string input)
    {
        _stopwatch.Restart();
        string cipherText = _des.Encrypt(input);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToEncrypt(_stopwatch.Elapsed)
        );

        _stopwatch.Restart();
        string plainText = _des.Decrypt(cipherText);
        _stopwatch.Stop();

        TestContext.Out.WriteLine(
            StringHelper.TimeToDecrypt(_stopwatch.Elapsed)
        );

        Assert.That(plainText, Is.EqualTo(input));
    }
}

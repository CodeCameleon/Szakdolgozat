using AlgorithmTest.Models;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// A DES titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
[TestFixture]
internal class DesRunTimeTests
    : BaseRunTime
{
    /// <summary>
    /// A DES titkosító algoritmust tároló adattag.
    /// </summary>
    private DESAlgorithm _des;

    /// <summary>
    /// A teszteket előkészítő függvény.
    /// </summary>
    [OneTimeSetUp]
    public void SetUp()
    {
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
        RunTime(_des, input);
    }
}

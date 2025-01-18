using Shared.Algorithms.Implementations;

namespace Tests.Algorithm.RunTimeTests;

/// <summary>
/// A DES szimmetrikus titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
internal class DesRunTimeTests
    : BaseRunTime<DesAlgorithm>
{
    /// <summary>
    /// Az összes teszteset futási idejét vizsgáló teszt.
    /// </summary>
    /// <param name="input">A titkosítandó szöveg.</param>
    [Test, TestCaseSource(typeof(TestCaseSources), nameof(TestCaseSources.AllTestCases))]
    public void All(string input)
    {
        RunTime(input);
    }
}

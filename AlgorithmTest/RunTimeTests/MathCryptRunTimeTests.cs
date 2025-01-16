using Base.Test.Implementations;

namespace AlgorithmTest.RunTimeTests;

/// <summary>
/// A MathCrypt titkosító algoritmus futási idejét vizsgáló tesztesetek.
/// </summary>
internal class MathCryptRunTimeTests
    : BaseRunTime<MathCryptAlgorithm>
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

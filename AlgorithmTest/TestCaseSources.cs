namespace AlgorithmTest;

/// <summary>
/// A tesztesetekket tartalmazó osztály.
/// </summary>
internal class TestCaseSources
{
    /// <summary>
    /// Az egyszerű teszteseteket tároló tulajdonság.
    /// </summary>
    public static IEnumerable<TestCaseData> SimpleTestCases =>
    [
        new TestCaseData("Alma"),
        new TestCaseData("Bannán"),
        new TestCaseData("Narancs"),
        new TestCaseData("Ez már kicsit nehezebb!"),
        new TestCaseData("4-5-1 és még 4 talán 2 is?")
    ];
}

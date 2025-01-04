namespace AlgorithmTest;

/// <summary>
/// A tesztesetekket tartalmazó osztály.
/// </summary>
internal class TestCaseSources
{
    /// <summary>
    /// A csak számokat tartalmazó bemeneteket tároló adattag.
    /// </summary>
    private static readonly string[] _numbersOnlyInputs =
    [
        "10110011",
        "12345678",
        "90372958",
        "34861855",
        "78474572",
        "27961734",
        "97135856",
        "14514756",
        "87528687",
        "42826872"
    ];

    /// <summary>
    /// A speciális karaktereket tartalmazó bemeneteket tároló adattag.
    /// </summary>
    private static readonly string[] _specialCharactersInputs =
    [
        "!@#$%^&*",
        "{}[]<>()",
        ".,;:'\"`´",
        "-_=+|\\/°",
        "~`!@#$%^",
        "^&*()_+-",
        "[{]}|:;.",
        "'\"<|>,./",
        "?-_=+\\|/",
        "@#$%^&*(",
    ];

    /// <summary>
    /// A speciális karaktereket tartalmazó teszteseteket tároló tulajdonság.
    /// </summary>
    public static IEnumerable<TestCaseData> SpecialCharactersTestCases => CreateTestCases(_specialCharactersInputs);

    /// <summary>
    /// Csak számokat tartalmazó teszteseteket tároló tulajdonság.
    /// </summary>
    public static IEnumerable<TestCaseData> NumbersOnlyTestCases => CreateTestCases(_numbersOnlyInputs);

    /// <summary>
    /// Az összes tesztesetet tartalmazó tulajdonság.
    /// </summary>
    public static IEnumerable<TestCaseData> AllTestCases => CreateTestCases(_numbersOnlyInputs
        .Concat(_specialCharactersInputs)
    );

    /// <summary>
    /// Elkészíti a különböző méretű teszteseteket a bemenetek alapján.
    /// </summary>
    /// <param name="inputs">A bemenetek.</param>
    /// <returns>A tesztesetek.</returns>
    /// <exception cref="ArgumentException"></exception>
    private static IEnumerable<TestCaseData> CreateTestCases(IEnumerable<string> inputs)
    {
        foreach (string input in inputs)
        {
            if (System.Text.ASCIIEncoding.ASCII.GetByteCount(input) != 8)
            {
                throw new ArgumentException("A bemenetnek pontosan 8 bájtnak kell lennie.");
            }

            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 128))); // 1 Kilobyte
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 256))); // 2 Kilobytes
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 384))); // 3 Kilobytes
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 512))); // 4 Kilobytes
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 640))); // 5 Kilobytes
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 768))); // 6 Kilobytes
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 896))); // 7 Kilobytes
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 1024))); // 8 Kilobytes
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 1152))); // 9 Kilobytes
            yield return new TestCaseData(string.Concat(Enumerable.Repeat(input, 1280))); // 10 Kilobytes
        }
    }
}

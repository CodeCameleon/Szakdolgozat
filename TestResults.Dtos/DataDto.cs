namespace TestResults.Dtos;

/// <summary>
/// Egy adatot ábrázoló adatátmeneti objektum.
/// </summary>
public class DataDto
{
    /// <summary>
    /// A teszteset mérete bájtban.
    /// </summary>
    public int TestCaseSize { get; set; }

    /// <summary>
    /// A teszteredmény.
    /// </summary>
    public double TestResult { get; set; }
}

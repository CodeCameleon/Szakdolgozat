namespace TestResults.Dtos;

/// <summary>
/// Egy tesztesetet ábrázoló adatátmeneti objektum.
/// </summary>
public class TestCaseDto
{
    /// <summary>
    /// A teszteset bemenete.
    /// </summary>
    public required string Input { get; set; }

    /// <summary>
    /// A teszteset mérete bájtban.
    /// </summary>
    public int Size { get; set; }
}

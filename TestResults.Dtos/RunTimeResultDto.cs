namespace TestResults.Dtos;

/// <summary>
/// Egy futási idő eredményt ábrázoló adatátmeneti objektum.
/// </summary>
public class RunTimeResultDto
    : TestResultDto
{
    /// <summary>
    /// A bemenet titkosításának ideje milliszekundumban.
    /// </summary>
    public double TimeToEncrypt { get; set; }

    /// <summary>
    /// A bemenet visszafejtésének ideje milliszekundumban.
    /// </summary>
    public double TimeToDecrypt { get; set; }
}

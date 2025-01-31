namespace TestResults.Dtos;

/// <summary>
/// Egy memóriahasználat eredményt ábrázoló adatátmeneti objektum.
/// </summary>
public class MemoryUsageResultDto
    : TestResultDto
{
    /// <summary>
    /// A titkosítás során felhasznált memória bájtban.
    /// </summary>
    public long EncryptionMemoryUsage { get; set; }

    /// <summary>
    /// A visszafejtés során felhasznált memória bájtban.
    /// </summary>
    public long DecryptionMemoryUsage { get; set; }
}

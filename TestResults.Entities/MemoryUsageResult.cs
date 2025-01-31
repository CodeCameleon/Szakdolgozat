using System.ComponentModel.DataAnnotations;

namespace TestResults.Entities;

/// <summary>
/// Egy memóriahasználat eredményt ábrázoló osztály.
/// </summary>
public class MemoryUsageResult
{
    /// <summary>
    /// A teszt eredmény azonosítója.
    /// </summary>
    [Key]
    public Guid TestResultId { get; set; }

    /// <summary>
    /// A titkosítás során felhasznált memória bájtban.
    /// </summary>
    public long EncryptionMemoryUsage { get; set; }

    /// <summary>
    /// A visszafejtés során felhasznált memória bájtban.
    /// </summary>
    public long DecryptionMemoryUsage { get; set; }

    /// <summary>
    /// A teszt eredmény navigációs tulajdonsága.
    /// </summary>
    public virtual TestResult? TestResult { get; set; }
}

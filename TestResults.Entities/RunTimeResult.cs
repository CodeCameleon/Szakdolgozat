using System.ComponentModel.DataAnnotations;

namespace TestResults.Entities;

/// <summary>
/// Egy futási idő eredményt ábrázoló osztály.
/// </summary>
public class RunTimeResult
{
    /// <summary>
    /// A teszt eredmény azonosítója.
    /// </summary>
    [Key]
    public Guid TestResultId { get; set; }

    /// <summary>
    /// A bemenet titkosításának ideje milliszekundumban.
    /// </summary>
    public double TimeToEncrypt { get; set; }

    /// <summary>
    /// A bemenet visszafejtésének ideje milliszekundumban.
    /// </summary>
    public double TimeToDecrypt { get; set; }

    /// <summary>
    /// A teszt eredmény navigációs tulajdonsága.
    /// </summary>
    public virtual TestResult? TestResult { get; set; }
}

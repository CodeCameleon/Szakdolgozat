using System.ComponentModel.DataAnnotations;

namespace TestResults.Entities;

/// <summary>
/// Egy teszt eredményt ábrázoló osztály.
/// </summary>
public class TestResult
{
    /// <summary>
    /// A teszt eredmény azonosítója.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// Sikeres volt-e a teszt.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// A teszthez használt algoritmus azonosítója.
    /// </summary>
    public Guid AlgorithmId { get; set; }

    /// <summary>
    /// A teszthez használt teszteset azonosítója.
    /// </summary>
    public Guid TestCaseId { get; set; }

    /// <summary>
    /// A teszthez használt algoritmus navigációs tulajdonsága.
    /// </summary>
    public virtual Algorithm? Algorithm { get; set; }

    /// <summary>
    /// A teszthez tartozó memóriahasználat eredmény navigációs tulajdonsága.
    /// </summary>
    public virtual MemoryUsageResult? MemoryUsageResult { get; set; }

    /// <summary>
    /// A teszthez tartozó futási idő eredmény navigációs tulajdonsága.
    /// </summary>
    public virtual RunTimeResult? RunTimeResult { get; set; }

    /// <summary>
    /// A teszthez használt teszteset navigációs tulajdonsága.
    /// </summary>
    public virtual TestCase? TestCase { get; set; }
}

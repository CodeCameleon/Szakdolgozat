using System.ComponentModel.DataAnnotations;

namespace TestResults.Entities;

/// <summary>
/// Egy algoritmust ábrázoló osztály.
/// </summary>
public class Algorithm
{
    /// <summary>
    /// Az algoritmus azonosítója.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Az algoritmus neve.
    /// </summary>
    [Required]
    public required string Name { get; set; }

    /// <summary>
    /// Az algoritmus típusának azonosítója.
    /// </summary>
    public int TypeId { get; set; }

    /// <summary>
    /// Az algoritmus típusának navigációs tulajdonsága.
    /// </summary>
    public virtual AlgorithmType? Type { get; set; }

    /// <summary>
    /// Az algoritmushoz tartozó teszteredmények navigációs tulajdonsága.
    /// </summary>
    public virtual List<TestResult>? TestResults { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace TestResults.Entities;

/// <summary>
/// Az algoritmus típusát ábrázoló osztály.
/// </summary>
public class AlgorithmType
{
    /// <summary>
    /// A típus azonosítója.
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// A típus megnevezése.
    /// </summary>
    [Required]
    public required string Type { get; set; }

    /// <summary>
    /// A típushoz tartozó algoritmusok navigációs tulajdonsága.
    /// </summary>
    public virtual List<Algorithm>? Algorithms { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace TestResults.Entities;

/// <summary>
/// Egy futási idő eredményt ábrázoló osztály.
/// </summary>
public class RunTimeResult
{
    /// <summary>
    /// Az eredmény azonosítója.
    /// </summary>
    [Key]
    public Guid Id { get; set; }

    /// <summary>
    /// A titkosítási algoritmus neve.
    /// </summary>
    [Required]
    public required string AlgorithmName { get; set; }

    /// <summary>
    /// A tesztelt szöveg.
    /// </summary>
    [Required]
    public required string Input { get; set; }

    /// <summary>
    /// Sikeres volt-e a teszt.
    /// </summary>
    public bool IsSuccessful { get; set; }

    /// <summary>
    /// A szöveg titkosításának ideje milliszekundumban.
    /// </summary>
    public double TimeToEncrypt { get; set; }

    /// <summary>
    /// A szöveg visszafejtésének ideje milliszekundumban.
    /// </summary>
    public double TimeToDecrypt { get; set; }
}

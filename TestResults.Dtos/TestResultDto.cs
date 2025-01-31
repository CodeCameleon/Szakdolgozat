using Shared.Enums;

namespace TestResults.Dtos;

/// <summary>
/// Egy teszt eredményt ábrázoló adatátmeneti objektum.
/// </summary>
public class TestResultDto
{
    /// <summary>
    /// A használt algoritmus neve.
    /// </summary>
    public required string AlgorithmName { get; set; }

    /// <summary>
    /// A használt algoritmus típusa.
    /// </summary>
    public EAlgorithmType AlgorithmType { get; set; }

    /// <summary>
    /// A használt bemenete.
    /// </summary>
    public required string Input { get; set; }

    /// <summary>
    /// Sikeres volt-e a teszt.
    /// </summary>
    public bool IsSuccessful { get; set; }
}

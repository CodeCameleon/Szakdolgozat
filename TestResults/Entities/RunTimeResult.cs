namespace TestResults.Entities;

/// <summary>
/// Egy futási idő eredményt ábrázoló osztály.
/// </summary>
public class RunTimeResult
{
    /// <summary>
    /// Az eredmény azonosítója.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// A titkosítási algoritmus neve.
    /// </summary>
    public string AlgorithmName { get; set; }

    /// <summary>
    /// A tesztelt szöveg.
    /// </summary>
    public string Input { get; set; }

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

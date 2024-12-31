namespace TestResults.Entities;

/// <summary>
/// Egy memóriahasználat eredményt ábrázoló osztály.
/// </summary>
public class MemoryUsageResult
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
    /// A titkosítás során felhasznált memória bájtban.
    /// </summary>
    public long EncryptionMemoryUsage { get; set; }

    /// <summary>
    /// A visszafejtés során felhasznált memória bájtban.
    /// </summary>
    public long DecryptionMemoryUsage { get; set; }
}

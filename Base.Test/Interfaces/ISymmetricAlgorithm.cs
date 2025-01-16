namespace Base.Test.Interfaces;

/// <summary>
/// Egy szimmetrikus titkosító algoritmust ábrázoló interfész.
/// </summary>
public interface ISymmetricAlgorithm
    : IDisposable
{
    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plainText">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg.</returns>
    string Encrypt(string plainText);

    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="cipherText">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg.</returns>
    string Decrypt(string cipherText);
}

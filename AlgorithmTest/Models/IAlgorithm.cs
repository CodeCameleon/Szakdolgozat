namespace AlgorithmTest.Models;

/// <summary>
/// Egy általános titkosító algoritmust ábrázoló interfész.
/// </summary>
internal interface IAlgorithm
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

namespace AlgorithmTest.Models;

/// <summary>
/// Egy általános titkosító algoritmust ábrázoló interfész.
/// </summary>
internal interface IAlgorithm
{
    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plainText">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg és a futási idő.</returns>
    (string CipherText, TimeSpan TimeToRun) Encrypt(string plainText);

    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="cipherText">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg és a futási idő.</returns>
    (string PlainText, TimeSpan TimeToRun) Decrypt(string cipherText);
}

using System.Diagnostics;

namespace AlgorithmTest.Models;

/// <summary>
/// Egy általános titkosító algoritmust ábrázoló interfész.
/// </summary>
internal interface IAlgorithm
{
    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plaintext">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg és a futási idő.</returns>
    (string CipherText, Stopwatch TimeToRun) Encrypt(string plaintext);

    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="ciphertext">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg és a futási idő.</returns>
    (string PlainText, Stopwatch TimeToRun) Decrypt(string ciphertext);
}

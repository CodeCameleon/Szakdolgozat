namespace AlgorithmTest;

/// <summary>
/// Egy általános titkosítási algoritmust ábrázoló interfész.
/// </summary>
internal interface IAlgorithm
{
    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plaintext">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg.</returns>
    string Encrypt(string plaintext);

    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="ciphertext">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg.</returns>
    string Decrypt(string ciphertext);
}

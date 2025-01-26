namespace Thesis.MathCrypt.Interfaces;

/// <summary>
/// A saját titkosító algoritmusomat ábrázoló interfész.
/// </summary>
public interface IMathCrypt
{
    /// <summary>
    /// A titkosító algoritmus kulcsa.
    /// </summary>
    char[][] Key { get; set; }

    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="cipherText">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg.</returns>
    string Decrypt(string cipherText);

    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plainText">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg.</returns>
    string Encrypt(string plainText);
}

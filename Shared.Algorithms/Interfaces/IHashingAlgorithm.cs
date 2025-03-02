namespace Shared.Algorithms.Interfaces;

/// <summary>
/// Egy hasító algoritmust ábrázoló interfész.
/// </summary>
public interface IHashingAlgorithm
    : ICryptographicAlgorithm
{
    /// <summary>
    /// Viszafejthetetlenül titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plainText">A titkosítandó szöveg.</param>
    /// <returns>A viszafejthetetlen szöveg.</returns>
    string Hash(string plainText);
}

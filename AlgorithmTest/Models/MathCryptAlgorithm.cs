using MathCrypt.Services;
using System.Diagnostics;

namespace AlgorithmTest.Models;

/// <summary>
/// A MathCrypt titkosító algoritmust megvalósító osztály.
/// </summary>
internal class MathCryptAlgorithm
    : IAlgorithm
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly CryptionService _service;

    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private readonly Stopwatch _stopwatch;

    /// <summary>
    /// Az osztály paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A kulcs, amelyet a titkosító algoritmus használ.</param>
    public MathCryptAlgorithm(char[][] key)
    {
        _service = new CryptionService(key);
        _stopwatch = new Stopwatch();
    }

    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plaintext">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg és a futási idő.</returns>
    public (string CipherText, Stopwatch TimeToRun) Encrypt(string plaintext)
    {
        _stopwatch.Restart();
        string ciphertext = _service.Encrypt(plaintext);
        _stopwatch.Stop();

        return (ciphertext, _stopwatch);
    }

    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="ciphertext">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg és a futási idő.</returns>
    public (string PlainText, Stopwatch TimeToRun) Decrypt(string ciphertext)
    {
        _stopwatch.Restart();
        string plaintext = _service.Decrypt(ciphertext);
        _stopwatch.Stop();

        return (plaintext, _stopwatch);
    }
}

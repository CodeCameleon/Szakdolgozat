using System.Diagnostics;
using System.Security.Cryptography;

namespace AlgorithmTest.Models;

/// <summary>
/// Az AES titkosító algoritmust megvalósító osztály.
/// </summary>
internal class AESAlgorithm
    : IAlgorithm
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly Aes _aes;

    /// <summary>
    /// A futási idő mérésére szolgáló osztályt tároló adattag.
    /// </summary>
    private readonly Stopwatch _stopwatch;

    /// <summary>
    /// Az osztály konstruktora.
    /// </summary>
    public AESAlgorithm()
    {
        _aes = Aes.Create();
        _aes.GenerateKey();
        _aes.GenerateIV();
        _stopwatch = new Stopwatch();
    }

    /// <summary>
    /// Az osztály paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A kulcs, amelyet a titkosító algoritmus használ.</param>
    /// <param name="iv">Az inicializáló vektor.</param>
    public AESAlgorithm(byte[] key, byte[] iv)
    {
        _aes = Aes.Create();
        _aes.Key = key;
        _aes.IV = iv;
        _stopwatch = new Stopwatch();
    }

    /// <summary>
    /// Az osztály destruktora.
    /// </summary>
    ~AESAlgorithm()
    {
        _aes.Dispose();
    }

    /// <summary>
    /// Titkosítja a megadott szöveget.
    /// </summary>
    /// <param name="plainText">A titkosítandó szöveg.</param>
    /// <returns>A titkosított szöveg és a futási idő.</returns>
    public (string CipherText, TimeSpan TimeToRun) Encrypt(string plainText)
    {
        _stopwatch.Restart();
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, _aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter sw = new(cs);

        sw.Write(plainText);
        sw.Flush();
        cs.FlushFinalBlock();

        string cipherText = Convert.ToBase64String(ms.ToArray());
        _stopwatch.Stop();

        return (cipherText, _stopwatch.Elapsed);
    }
    
    /// <summary>
    /// Visszafejti a megadott szöveget.
    /// </summary>
    /// <param name="cipherText">A visszafejtendő szöveg.</param>
    /// <returns>A visszafejtett szöveg és a futási idő.</returns>
    public (string PlainText, TimeSpan TimeToRun) Decrypt(string cipherText)
    {
        _stopwatch.Restart();
        using MemoryStream ms = new(Convert.FromBase64String(cipherText));
        using CryptoStream cs = new(ms, _aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        string plainText = sr.ReadToEnd();
        _stopwatch.Stop();

        return (plainText, _stopwatch.Elapsed);
    }
}

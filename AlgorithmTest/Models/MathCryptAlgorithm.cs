using MathCrypt.Enums;
using MathCrypt.Services;

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
    /// Az osztály konstruktora.
    /// </summary>
    public MathCryptAlgorithm()
    {
        char[][] key = KeyGenService.Instance.GenerateKey(
            strength: 2,
            ECharset.Space,
            ECharset.Numbers,
            ECharset.MathSymbols,
            ECharset.Punctuations,
            ECharset.EN,
            ECharset.HU
        );

        _service = new CryptionService(key);
    }

    /// <summary>
    /// Az osztály paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A kulcs, amelyet a titkosító algoritmus használ.</param>
    public MathCryptAlgorithm(char[][] key)
    {
        _service = new CryptionService(key);
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        // Itt nem kell semmit felszabadítani.
    }

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        return _service.Encrypt(plainText);
    }

    /// <inheritdoc />
    public string Decrypt(string cipherText)
    {
        return _service.Decrypt(cipherText);
    }
}

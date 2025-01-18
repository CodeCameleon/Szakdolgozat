using MathCrypt.Enums;
using MathCrypt.Services;
using Shared.Algorithms.Interfaces;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// A MathCrypt szimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class MathCryptAlgorithm
    : ISymmetricAlgorithm
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly CryptionService _service;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
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

        _service = new(key);
    }

    /// <summary>
    /// Az algoritmust paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A titkosító algoritmus kulcsa.</param>
    public MathCryptAlgorithm(char[][] key)
    {
        _service = new(key);
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

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

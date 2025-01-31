using Shared.Algorithms.Interfaces;
using Shared.Enums;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// Az AES szimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class AesAlgorithm
    : ISymmetricAlgorithm
{
    /// <summary>
    /// Az algoritmus nevét tároló adattag.
    /// </summary>
    private const string _algorithmName = "AES";

    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly Aes _aes;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public AesAlgorithm()
    {
        _aes = Aes.Create();
        _aes.GenerateKey();
        _aes.GenerateIV();
    }

    /// <summary>
    /// Az algoritmust paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A titkosító algoritmus kulcsa.</param>
    /// <param name="iv">Az inicializáló vektor.</param>
    public AesAlgorithm(byte[] key, byte[] iv)
    {
        _aes = Aes.Create();
        _aes.Key = key;
        _aes.IV = iv;
    }

    /// <inheritdoc />
    public string AlgorithmName => _algorithmName;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Symmetric;

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, _aes.CreateEncryptor(), CryptoStreamMode.Write);
        using StreamWriter sw = new(cs);

        sw.Write(plainText);
        sw.Flush();
        cs.FlushFinalBlock();

        return Convert.ToBase64String(ms.ToArray());
    }

    /// <inheritdoc />
    public string Decrypt(string cipherText)
    {
        using MemoryStream ms = new(Convert.FromBase64String(cipherText));
        using CryptoStream cs = new(ms, _aes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _aes.Dispose();

        GC.SuppressFinalize(this);
    }
}

using Shared.Algorithms.Interfaces;
using Shared.Enums;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// A TripleDES szimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class TripleDesAlgorithm
    : ISymmetricAlgorithm
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly TripleDES _tripleDes;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public TripleDesAlgorithm()
    {
        _tripleDes = TripleDES.Create();
        _tripleDes.GenerateKey();
        _tripleDes.GenerateIV();
    }

    /// <summary>
    /// Az algoritmust paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A titkosító algoritmus kulcsa.</param>
    /// <param name="iv">Az inicializáló vektor.</param>
    public TripleDesAlgorithm(byte[] key, byte[] iv)
    {
        _tripleDes = TripleDES.Create();
        _tripleDes.Key = key;
        _tripleDes.IV = iv;
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.TripleDes;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Symmetric;

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, _tripleDes.CreateEncryptor(), CryptoStreamMode.Write);
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
        using CryptoStream cs = new(ms, _tripleDes.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _tripleDes.Dispose();

        GC.SuppressFinalize(this);
    }
}

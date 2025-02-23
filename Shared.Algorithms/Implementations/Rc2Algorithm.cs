using Shared.Algorithms.Interfaces;
using Shared.Enums;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// Az RC2 szimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class Rc2Algorithm
    : ISymmetricAlgorithm
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly RC2 _rc2;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public Rc2Algorithm()
    {
        _rc2 = RC2.Create();
        _rc2.GenerateKey();
        _rc2.GenerateIV();
    }

    /// <summary>
    /// Az algoritmust paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A titkosító algoritmus kulcsa.</param>
    /// <param name="iv">Az inicializáló vektor.</param>
    public Rc2Algorithm(byte[] key, byte[] iv)
    {
        _rc2 = RC2.Create();
        _rc2.Key = key;
        _rc2.IV = iv;
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.Rc2;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Symmetric;

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, _rc2.CreateEncryptor(), CryptoStreamMode.Write);
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
        using CryptoStream cs = new(ms, _rc2.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _rc2.Dispose();

        GC.SuppressFinalize(this);
    }
}

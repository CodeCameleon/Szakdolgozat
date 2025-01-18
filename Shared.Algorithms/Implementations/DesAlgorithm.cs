using Shared.Algorithms.Interfaces;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// A DES szimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class DesAlgorithm
    : ISymmetricAlgorithm
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly DES _des;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public DesAlgorithm()
    {
        _des = DES.Create();
        _des.GenerateKey();
        _des.GenerateIV();
    }

    /// <summary>
    /// Az algoritmust paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A titkosító algoritmus kulcsa.</param>
    /// <param name="iv">Az inicializáló vektor.</param>
    public DesAlgorithm(byte[] key, byte[] iv)
    {
        _des = DES.Create();
        _des.Key = key;
        _des.IV = iv;
    }

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, _des.CreateEncryptor(), CryptoStreamMode.Write);
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
        using CryptoStream cs = new(ms, _des.CreateDecryptor(), CryptoStreamMode.Read);
        using StreamReader sr = new(cs);

        return sr.ReadToEnd();
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _des.Dispose();

        GC.SuppressFinalize(this);
    }
}

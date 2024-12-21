namespace AlgorithmTest.Models;

/// <summary>
/// Az AES titkosító algoritmust megvalósító osztály.
/// </summary>
internal class AESAlgorithm
    : IAlgorithm, IDisposable
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly Aes _aes;

    /// <summary>
    /// Az osztály konstruktora.
    /// </summary>
    public AESAlgorithm()
    {
        _aes = Aes.Create();
        _aes.GenerateKey();
        _aes.GenerateIV();
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
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _aes.Dispose();
    }

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
}

using Shared.Algorithms.Interfaces;
using Shared.Enums;
using System.Text;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// Az RSA aszimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class RsaAlgorithm
    : IEncryptionAlgorithm
{
    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly RSA _rsa;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public RsaAlgorithm()
    {
        _rsa = RSA.Create();
    }

    /// <summary>
    /// Az algoritmust paraméteres konstruktora.
    /// </summary>
    /// <param name="privateKey">A titkosító algoritmus privát kulcsa.</param>
    /// <param name="publicKey">A titkosító algoritmus publikus kulcsa.</param>
    public RsaAlgorithm(byte[] privateKey, byte[] publicKey)
    {
        _rsa = RSA.Create();
        _rsa.ImportRSAPrivateKey(privateKey, out _);
        _rsa.ImportRSAPublicKey(publicKey, out _);
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.Rsa;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Asymmetric;

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainText);
        byte[] encryptedData = _rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA256);
        return Convert.ToBase64String(encryptedData);
    }

    /// <inheritdoc />
    public string Decrypt(string cipherText)
    {
        byte[] encryptedData = Convert.FromBase64String(cipherText);
        byte[] decryptedData = _rsa.Decrypt(encryptedData, RSAEncryptionPadding.OaepSHA256);
        return Encoding.UTF8.GetString(decryptedData);
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _rsa.Dispose();

        GC.SuppressFinalize(this);
    }
}

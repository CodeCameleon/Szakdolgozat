using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Shared.Algorithms.Interfaces;
using Shared.Enums;
using System.Text;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// Az ElGamal aszimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class ElGamalAlgorithm
    : IEncryptionAlgorithm
{
    /// <summary>
    /// A titkosító algoritmus privát kulcsát tároló adattag.
    /// </summary>
    private readonly ElGamalKeyParameters _privateKey;

    /// <summary>
    /// A titkosító algoritmus publikus kulcsát tároló adattag.
    /// </summary>
    private readonly ElGamalKeyParameters _publicKey;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public ElGamalAlgorithm()
    {
        ElGamalParametersGenerator parametersGenerator = new();
        parametersGenerator.Init(1024, 12, new SecureRandom());
        ElGamalParameters parameters = parametersGenerator.GenerateParameters();

        ElGamalKeyPairGenerator keyPairGenerator = new();
        keyPairGenerator.Init(new ElGamalKeyGenerationParameters(new SecureRandom(), parameters));
        AsymmetricCipherKeyPair keyPair = keyPairGenerator.GenerateKeyPair();

        _privateKey = (ElGamalPrivateKeyParameters)keyPair.Private;
        _publicKey = (ElGamalPublicKeyParameters)keyPair.Public;
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.ElGamal;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Asymmetric;

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainText);

        ElGamalEngine engine = new();
        engine.Init(true, _publicKey);

        byte[] encryptedData = engine.ProcessBlock(data, 0, data.Length);
        return Convert.ToBase64String(encryptedData);
    }

    /// <inheritdoc />
    public string Decrypt(string cipherText)
    {
        byte[] encryptedData = Convert.FromBase64String(cipherText);

        ElGamalEngine engine = new();
        engine.Init(false, _privateKey);

        byte[] decryptedData = engine.ProcessBlock(encryptedData, 0, encryptedData.Length);
        return Encoding.UTF8.GetString(decryptedData);
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Asn1.X9;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Agreement;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Shared.Algorithms.Interfaces;
using Shared.Enums;
using System.Text;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// Az ECIES aszimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class EciesAlgorithm
    : IEncryptionAlgorithm
{
    /// <summary>
    /// A titkosító algoritmus privát kulcsát tároló adattag.
    /// </summary>
    private readonly ECPrivateKeyParameters _privateKey;

    /// <summary>
    /// A titkosító algoritmus publikus kulcsát tároló adattag.
    /// </summary>
    private readonly ECPublicKeyParameters _publicKey;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public EciesAlgorithm()
    {
        ECKeyPairGenerator keyPairGenerator = new();
        keyPairGenerator.Init(new ECKeyGenerationParameters(ECKeyGenerationParameters("secp256r1"), new SecureRandom()));
        AsymmetricCipherKeyPair keyPair = keyPairGenerator.GenerateKeyPair();

        _privateKey = (ECPrivateKeyParameters)keyPair.Private;
        _publicKey = (ECPublicKeyParameters)keyPair.Public;
    }

    /// <summary>
    /// Az algoritmus kulcsának generálásához szükséges paramétereket elkészítő függvény.
    /// </summary>
    /// <param name="curveName">A görbe neve.</param>
    /// <returns>A kész paraméterek.</returns>
    private static ECDomainParameters ECKeyGenerationParameters(string curveName)
    {
        X9ECParameters ecSpec = SecNamedCurves.GetByName(curveName);
        return new ECDomainParameters(ecSpec.Curve, ecSpec.G, ecSpec.N, ecSpec.H, ecSpec.GetSeed());
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.Ecies;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Asymmetric;

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        byte[] data = Encoding.UTF8.GetBytes(plainText);
        
        ECKeyPairGenerator ephemeralGenerator = new();
        ephemeralGenerator.Init(new ECKeyGenerationParameters(_publicKey.Parameters, new SecureRandom()));
        AsymmetricCipherKeyPair ephemeralKeyPair = ephemeralGenerator.GenerateKeyPair();

        ECPrivateKeyParameters ephemeralPrivate = (ECPrivateKeyParameters)ephemeralKeyPair.Private;
        ECPublicKeyParameters ephemeralPublic = (ECPublicKeyParameters)ephemeralKeyPair.Public;

        IesEngine engine = new(new ECDHBasicAgreement(), new Kdf2BytesGenerator(new Sha256Digest()), new HMac(new Sha256Digest()));
        engine.Init(true, ephemeralPrivate, _publicKey, new IesParameters([], [], 128));
        byte[] encryptedData = engine.ProcessBlock(data, 0, data.Length);

        byte[] ephemeralEncoded = ephemeralPublic.Q.GetEncoded(true);
        byte[] combined = new byte[ephemeralEncoded.Length + encryptedData.Length];
        Buffer.BlockCopy(ephemeralEncoded, 0, combined, 0, ephemeralEncoded.Length);
        Buffer.BlockCopy(encryptedData, 0, combined, ephemeralEncoded.Length, encryptedData.Length);

        return Convert.ToBase64String(combined);
    }

    /// <inheritdoc />
    public string Decrypt(string cipherText)
    {
        byte[] combined = Convert.FromBase64String(cipherText);

        int ecPointLength = (_privateKey.Parameters.Curve.FieldSize + 7) / 8 + 1;
        byte[] ephemeralEncoded = combined[..ecPointLength];
        byte[] encryptedData = combined[ecPointLength..];

        Org.BouncyCastle.Math.EC.ECPoint ecPoint = _privateKey.Parameters.Curve.DecodePoint(ephemeralEncoded);
        ECPublicKeyParameters ephemeralPublic = new(ecPoint, _privateKey.Parameters);

        IesEngine engine = new(new ECDHBasicAgreement(), new Kdf2BytesGenerator(new Sha256Digest()), new HMac(new Sha256Digest()));
        engine.Init(false, _privateKey, ephemeralPublic, new IesParameters([], [], 128));
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

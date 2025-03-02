using Org.BouncyCastle.Crypto.Digests;
using Shared.Algorithms.Interfaces;
using Shared.Enums;
using System.Text;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// A BLAKE2B hasító algoritmust megvalósító osztály.
/// </summary>
public class Blake2bAlgorithm
    : IHashingAlgorithm
{
    /// <summary>
    /// A hasító algoritmust tároló adattag.
    /// </summary>
    private readonly Blake2bDigest _blake2b;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public Blake2bAlgorithm()
    {
        _blake2b = new();
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.Blake2b;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Hashing;

    /// <inheritdoc />
    public string Hash(string plainText)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] hashBytes = new byte[_blake2b.GetDigestSize()];

        _blake2b.BlockUpdate(inputBytes, 0, inputBytes.Length);
        _blake2b.DoFinal(hashBytes, 0);

        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

using Org.BouncyCastle.Crypto.Digests;
using Shared.Algorithms.Interfaces;
using Shared.Enums;
using System.Text;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// A Keccak-256 hasító algoritmust megvalósító osztály.
/// </summary>
public class Keccak256Algorithm
    : IHashingAlgorithm
{
    /// <summary>
    /// A hasító algoritmust tároló adattag.
    /// </summary>
    private readonly KeccakDigest _keccak256;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public Keccak256Algorithm()
    {
        _keccak256 = new(256);
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.Keccak256;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Hashing;

    /// <inheritdoc />
    public string Hash(string plainText)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] hashBytes = new byte[_keccak256.GetDigestSize()];

        _keccak256.BlockUpdate(inputBytes, 0, inputBytes.Length);
        _keccak256.DoFinal(hashBytes, 0);

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

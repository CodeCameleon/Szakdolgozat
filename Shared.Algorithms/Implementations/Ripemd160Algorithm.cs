using Org.BouncyCastle.Crypto.Digests;
using Shared.Algorithms.Interfaces;
using Shared.Enums;
using System.Text;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// A RIPEMD-160 hasító algoritmust megvalósító osztály.
/// </summary>
public class Ripemd160Algorithm
    :IHashingAlgorithm
{
    /// <summary>
    /// A hasító algoritmust tároló adattag.
    /// </summary>
    private readonly RipeMD160Digest _ripemd160;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public Ripemd160Algorithm()
    {
        _ripemd160 = new();
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.Ripemd160;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Hashing;

    /// <inheritdoc />
    public string Hash(string plainText)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] hashBytes = new byte[_ripemd160.GetDigestSize()];

        _ripemd160.BlockUpdate(inputBytes, 0, inputBytes.Length);
        _ripemd160.DoFinal(hashBytes, 0);

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

using Shared.Algorithms.Interfaces;
using Shared.Enums;
using System.Text;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// Az SHA256 hasító algoritmust megvalósító osztály.
/// </summary>
public class Sha256Algorithm
    : IHashingAlgorithm
{
    /// <summary>
    /// A hasító algoritmust tároló adattag.
    /// </summary>
    private readonly SHA256 _sha256;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public Sha256Algorithm()
    {
        _sha256 = SHA256.Create();
    }

    /// <inheritdoc />
    public EAlgorithmName AlgorithmName => EAlgorithmName.Sha256;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Hashing;

    /// <inheritdoc />
    public string Hash(string plainText)
    {
        byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
        byte[] hashBytes = _sha256.ComputeHash(inputBytes);

        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _sha256.Dispose();

        GC.SuppressFinalize(this);
    }
}

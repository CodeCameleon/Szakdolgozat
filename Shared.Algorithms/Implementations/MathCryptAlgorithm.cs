using Shared.Algorithms.Interfaces;
using Shared.Enums;
using Thesis.MathCrypt.Implementations;
using Thesis.MathCrypt.Interfaces;

namespace Shared.Algorithms.Implementations;

/// <summary>
/// A MathCrypt szimmetrikus titkosító algoritmust megvalósító osztály.
/// </summary>
public class MathCryptAlgorithm
    : ISymmetricAlgorithm
{
    /// <summary>
    /// Az algoritmus nevét tároló adattag.
    /// </summary>
    private const string _algorithmName = "MathCrypt";

    /// <summary>
    /// A titkosító algoritmust tároló adattag.
    /// </summary>
    private readonly IMathCrypt _mathCrypt;

    /// <summary>
    /// Az algoritmust alapértelmezett konstruktora.
    /// </summary>
    public MathCryptAlgorithm()
    {
        IMathCryptKeyGenerator keyGenerator = new MathCryptKeyGenerator();

        char[][] key = keyGenerator.GenerateKey(strength: 2);

        _mathCrypt = new MathCrypt(key);
    }

    /// <summary>
    /// Az algoritmust paraméteres konstruktora.
    /// </summary>
    /// <param name="key">A titkosító algoritmus kulcsa.</param>
    public MathCryptAlgorithm(char[][] key)
    {
        _mathCrypt = new MathCrypt(key);
    }

    /// <inheritdoc />
    public string AlgorithmName => _algorithmName;

    /// <inheritdoc />
    public EAlgorithmType AlgorithmType => EAlgorithmType.Symmetric;

    /// <inheritdoc />
    public string Encrypt(string plainText)
    {
        return _mathCrypt.Encrypt(plainText);
    }

    /// <inheritdoc />
    public string Decrypt(string cipherText)
    {
        return _mathCrypt.Decrypt(cipherText);
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}

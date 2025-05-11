using Shared.Algorithms.Implementations;
using Shared.Constants;
using Shared.Enums;

namespace Shared.Algorithms.Extensions;

/// <summary>
/// Az algoritmusok neveit tartalmazó enum kiterjesztései.
/// </summary>
public static class EAlgorithmNameExtensions
{
    /// <summary>
    /// Az megvalósításokat tartalmazó szótár.
    /// </summary>
    private static readonly Dictionary<EAlgorithmName, Type> _algorithmImplementations = new()
    {
        { EAlgorithmName.Aes, typeof(AesAlgorithm) },
        { EAlgorithmName.Blake2b, typeof(Blake2bAlgorithm) },
        { EAlgorithmName.Des, typeof(DesAlgorithm) },
        { EAlgorithmName.Ecies, typeof(EciesAlgorithm)},
        { EAlgorithmName.ElGamal, typeof(ElGamalAlgorithm)},
        { EAlgorithmName.Keccak256, typeof(Keccak256Algorithm) },
        { EAlgorithmName.MathCrypt, typeof(MathCryptAlgorithm) },
        { EAlgorithmName.Rc2, typeof(Rc2Algorithm) },
        { EAlgorithmName.Ripemd160, typeof(Ripemd160Algorithm) },
        { EAlgorithmName.Rsa, typeof(RsaAlgorithm) },
        { EAlgorithmName.Sha256, typeof(Sha256Algorithm) },
        { EAlgorithmName.TripleDes, typeof(TripleDesAlgorithm) }
    };

    /// <summary>
    /// Lekéri az algoritmus megvalósítását.
    /// </summary>
    /// <param name="algorithm">A keresett algoritmus.</param>
    /// <returns>Az algoritmust megvalósító osztály típusa.</returns>
    public static Type GetImplementation(this EAlgorithmName algorithm)
    {
        if (_algorithmImplementations.TryGetValue(algorithm, out var implementation))
        {
            return implementation;
        }

        throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, ErrorMessages.Undefined.AlgorithmImplementation);
    }
}

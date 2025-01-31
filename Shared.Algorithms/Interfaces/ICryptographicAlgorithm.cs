using Shared.Enums;

namespace Shared.Algorithms.Interfaces;

/// <summary>
/// Egy kriptográfiai algoritmust ábrázoló interfész.
/// </summary>
public interface ICryptographicAlgorithm
    : IDisposable
{
    /// <summary>
    /// A kriptográfiai algoritmus neve.
    /// </summary>
    string AlgorithmName { get; }

    /// <summary>
    /// A kriptográfiai algoritmus típusa.
    /// </summary>
    EAlgorithmType AlgorithmType { get; }
}

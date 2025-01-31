using TestResults.Entities;

namespace TestResults.Repositories.Interfaces;

/// <summary>
/// Az algoritmusokat kezelő adattárat ábrázoló interfész.
/// </summary>
public interface IAlgorithmRepository
{
    /// <summary>
    /// Létrehoz egy új algoritmust az adatbázisban.
    /// </summary>
    /// <param name="algorithm">A létrehozni kívánt algoritmus.</param>
    Task CreateAsync(Algorithm algorithm);

    /// <summary>
    /// Lekéri a nevéhez tartozó algoritmust az adatbázisból.
    /// </summary>
    /// <param name="name">A keresett algoritmus neve.</param>
    /// <returns>Az algoritmus ha létezik, különben null.</returns>
    Task<Algorithm?> GetAsync(string name);
}

using TestResults.Entities;

namespace TestResults.Interfaces;

/// <summary>
/// A memóriahasználat eredményeket kezelő adattárat ábrázoló interfész.
/// </summary>
public interface IMemoryUsageResultRepository
{
    /// <summary>
    /// Létrehoz egy új memóriahasználat eredményt az adatbázisban.
    /// </summary>
    /// <param name="memoryUsageResult">A létrehozni kívánt memóriahasználat eredmény.</param>
    Task CreateAsync(MemoryUsageResult memoryUsageResult);

    /// <summary>
    /// Lekéri az azonosítóhoz tartozó memóriahasználat eredményt az adatbázisból.
    /// </summary>
    /// <param name="id">A keresett memóriahasználat eredmény azonosítója.</param>
    /// <returns>A memóriahasználat eredmény.</returns>
    Task<MemoryUsageResult> GetAsync(Guid id);

    /// <summary>
    /// Lekéri az összes memóriahasználat eredményt az adatbázisból.
    /// </summary>
    /// <returns>A memóriahasználat eredmények listája.</returns>
    Task<List<MemoryUsageResult>> GetListAsync();
}

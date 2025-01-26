using TestResults.Entities;

namespace TestResults.Services.Interfaces;

/// <summary>
/// A memóriahasználat eredményeket kezelő szolgáltatást ábrázoló interfész.
/// </summary>
public interface IMemoryUsageResultService
{
    /// <summary>
    /// Létrehoz egy új memóriahasználat eredményt.
    /// </summary>
    /// <param name="memoryUsageResult">A létrehozni kívánt memóriahasználat eredmény.</param>
    Task CreateAsync(MemoryUsageResult memoryUsageResult);

    /// <summary>
    /// Lekéri az azonosítóhoz tartozó memóriahasználat eredményt.
    /// </summary>
    /// <param name="id">A keresett memóriahasználat eredmény azonosítója.</param>
    /// <returns>A memóriahasználat eredmény.</returns>
    Task<MemoryUsageResult> GetAsync(Guid id);

    /// <summary>
    /// Lekéri az összes memóriahasználat eredményt.
    /// </summary>
    /// <returns>A memóriahasználat eredmények listája.</returns>
    Task<List<MemoryUsageResult>> GetListAsync();
}

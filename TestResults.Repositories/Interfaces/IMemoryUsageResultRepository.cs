using TestResults.Entities;

namespace TestResults.Repositories.Interfaces;

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
}

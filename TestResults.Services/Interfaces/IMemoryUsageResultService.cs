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
}

using TestResults.Dtos;

namespace TestResults.Services.Interfaces;

/// <summary>
/// A memóriahasználat eredményeket kezelő szolgáltatást ábrázoló interfész.
/// </summary>
public interface IMemoryUsageResultService
    : IDisposable
{
    /// <summary>
    /// Létrehoz egy új memóriahasználat eredményt.
    /// </summary>
    /// <param name="memoryUsageResultDto">A létrehozni kívánt memóriahasználat eredmény adatátmeneti objektumként.</param>
    Task CreateAsync(MemoryUsageResultDto memoryUsageResultDto);
}

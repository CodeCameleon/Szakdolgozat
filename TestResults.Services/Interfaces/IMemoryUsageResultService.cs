using Shared.Enums;
using TestResults.Dtos;

namespace TestResults.Services.Interfaces;

/// <summary>
/// A memóriahasználat eredményeket kezelő szolgáltatást ábrázoló interfész.
/// </summary>
public interface IMemoryUsageResultService
{
    /// <summary>
    /// Létrehoz egy új memóriahasználat eredményt.
    /// </summary>
    /// <param name="memoryUsageResultDto">A létrehozni kívánt memóriahasználat eredmény adatátmeneti objektumként.</param>
    Task CreateAsync(MemoryUsageResultDto memoryUsageResultDto);

    /// <summary>
    /// Lekéri az összes memóriahasználat eredményt adathalmazként.
    /// </summary>
    /// <param name="algorithm">A keresett algoritmus.</param>
    /// <returns>A memóriahasználat eredmények adathalmazának listája.</returns>
    Task<List<DatasetDto>> GetDatasetListAsync(EAlgorithmName? algorithm);
}

using Shared.Enums;
using TestResults.Dtos;
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

    /// <summary>
    /// Lekéri az összes memóriahasználat eredményt adathalmazként az adatbázisból.
    /// </summary>
    /// <param name="algorithm">A keresett algoritmus.</param>
    /// <param name="type">A keresett algoritmus típusa.</param>
    /// <returns>A memóriahasználat eredmények adathalmazának listája.</returns>
    Task<List<DatasetDto>> GetDatasetListAsync(EAlgorithmName? algorithm, EAlgorithmType? type);
}

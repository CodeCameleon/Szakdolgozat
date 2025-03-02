using Shared.Enums;
using TestResults.Dtos;
using TestResults.Entities;

namespace TestResults.Repositories.Interfaces;

/// <summary>
/// A futási idő eredményeket kezelő adattárat ábrázoló interfész.
/// </summary>
public interface IRunTimeResultRepository
{
    /// <summary>
    /// Létrehoz egy új futási idő eredményt az adatbázisban.
    /// </summary>
    /// <param name="runTimeResult">A létrehozni kívánt futási idő eredmény.</param>
    Task CreateAsync(RunTimeResult runTimeResult);

    /// <summary>
    /// Lekéri az összes futási idő eredményt adathalmazként az adatbázisból.
    /// </summary>
    /// <param name="algorithm">A keresett algoritmus.</param>
    /// <param name="type">A keresett algoritmus típusa.</param>
    /// <returns>A futási idő eredmények adathalmazának listája.</returns>
    Task<List<DatasetDto>> GetDatasetListAsync(EAlgorithmName? algorithm, EAlgorithmType? type);
}

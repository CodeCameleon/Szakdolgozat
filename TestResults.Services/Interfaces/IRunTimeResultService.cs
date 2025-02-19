using Shared.Enums;
using TestResults.Dtos;

namespace TestResults.Services.Interfaces;

/// <summary>
/// A futási idő eredményeket kezelő szolgáltatást ábrázoló interfész.
/// </summary>
public interface IRunTimeResultService
{
    /// <summary>
    /// Létrehoz egy új futási idő eredményt.
    /// </summary>
    /// <param name="runTimeResultDto">A létrehozni kívánt futási idő eredmény adatátmeneti objektumként.</param>
    Task CreateAsync(RunTimeResultDto runTimeResultDto);

    /// <summary>
    /// Lekéri az összes futási idő eredményt adathalmazként.
    /// </summary>
    /// <param name="algorithm">A keresett algoritmus.</param>
    /// <returns>A futási idő eredmények adathalmazának listája.</returns>
    Task<List<DatasetDto>> GetDatasetListAsync(EAlgorithmName? algorithm);
}

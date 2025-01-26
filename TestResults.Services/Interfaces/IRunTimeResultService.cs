using TestResults.Entities;

namespace TestResults.Services.Interfaces;

/// <summary>
/// A futási idő eredményeket kezelő szolgáltatást ábrázoló interfész.
/// </summary>
public interface IRunTimeResultService
{
    /// <summary>
    /// Létrehoz egy új futási idő eredményt.
    /// </summary>
    /// <param name="runTimeResult">A létrehozni kívánt futási idő eredmény.</param>
    Task CreateAsync(RunTimeResult runTimeResult);

    /// <summary>
    /// Lekéri az azonosítóhoz tartozó futási idő eredményt.
    /// </summary>
    /// <param name="id">A keresett futási idő eredmény azonosítója.</param>
    /// <returns>A futási idő eredmény.</returns>
    Task<RunTimeResult> GetAsync(Guid id);

    /// <summary>
    /// Lekéri az összes futási idő eredményt.
    /// </summary>
    /// <returns>A futási idő eredmények listája.</returns>
    Task<List<RunTimeResult>> GetListAsync();
}

using TestResults.Entities;

namespace TestResults.Interfaces;

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
    /// Lekéri az azonosítóhoz tartozó futási idő eredményt az adatbázisból.
    /// </summary>
    /// <param name="id">A keresett futási idő eredmény azonosítója.</param>
    /// <returns>A futási idő eredmény.</returns>
    Task<RunTimeResult> GetAsync(Guid id);

    /// <summary>
    /// Lekéri az összes futási idő eredményt az adatbázisból.
    /// </summary>
    /// <returns>A futási idő eredmények listája.</returns>
    Task<List<RunTimeResult>> GetListAsync();
}

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
}

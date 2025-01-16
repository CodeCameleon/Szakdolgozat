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
}

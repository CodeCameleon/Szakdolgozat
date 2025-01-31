using TestResults.Entities;

namespace TestResults.Repositories.Interfaces;

/// <summary>
/// A teszt eredményeket kezelő adattárat ábrázoló interfész.
/// </summary>
public interface ITestResultRepository
{
    /// <summary>
    /// Létrehoz egy új teszt eredményt az adatbázisban.
    /// </summary>
    /// <param name="testResult">A létrehozni kívánt teszt eredmény.</param>
    Task CreateAsync(TestResult testResult);
}

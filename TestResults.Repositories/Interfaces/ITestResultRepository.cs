using TestResults.Entities;

namespace TestResults.Repositories.Interfaces;

/// <summary>
/// A teszteredményeket kezelő adattárat ábrázoló interfész.
/// </summary>
public interface ITestResultRepository
{
    /// <summary>
    /// Létrehoz egy új teszteredményt az adatbázisban.
    /// </summary>
    /// <param name="testResult">A létrehozni kívánt teszteredmény.</param>
    Task CreateAsync(TestResult testResult);
}

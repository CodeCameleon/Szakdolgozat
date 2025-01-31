using Microsoft.EntityFrameworkCore;
using TestResults.Entities;
using TestResults.EntityFramework;
using TestResults.Repositories.Interfaces;

namespace TestResults.Repositories.Implementations;

/// <summary>
/// A teszt eredményeket kezelő adattárat megvalósító osztály.
/// </summary>
public class TestResultRepository
    : ITestResultRepository
{
    /// <summary>
    /// A teszt eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    private readonly DbSet<TestResult> _testResults;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public TestResultRepository(TestResultsDbContext context)
    {
        _testResults = context.TestResults;
    }

    /// <inheritdoc />
    public async Task CreateAsync(TestResult testResult)
    {
        await _testResults.AddAsync(testResult);
    }
}

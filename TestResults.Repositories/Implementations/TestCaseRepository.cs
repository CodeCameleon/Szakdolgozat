using Microsoft.EntityFrameworkCore;
using TestResults.Entities;
using TestResults.EntityFramework;
using TestResults.Repositories.Interfaces;

namespace TestResults.Repositories.Implementations;

/// <summary>
/// A teszteseteket kezelő adattárat megvalósító osztály.
/// </summary>
public class TestCaseRepository
    : ITestCaseRepository
{
    /// <summary>
    /// A teszteseteket tartalmazó adatbázis tábla.
    /// </summary>
    private readonly DbSet<TestCase> _testCases;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public TestCaseRepository(TestResultsDbContext context)
    {
        _testCases = context.TestCases;
    }

    /// <inheritdoc />
    public async Task CreateAsync(TestCase testCase)
    {
        await _testCases.AddAsync(testCase);
    }

    /// <inheritdoc />
    public async Task<bool> NotExistsAsync(string input)
    {
        return !await _testCases.AnyAsync(tc => tc.Input.Equals(input));
    }

    /// <inheritdoc />
    public async Task<TestCase> GetAsync(string input)
    {
        return await _testCases.Where(tc => tc.Input.Equals(input)).AsNoTracking().SingleAsync();
    }

    /// <inheritdoc />
    public async Task<List<string>> GetEnabledInputListAsync()
    {
        return await _testCases.Where(tc => tc.Enabled).Select(tc => tc.Input).ToListAsync();
    }
}

using Microsoft.EntityFrameworkCore;
using TestResults.Dtos;
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
    public async Task<bool> ExistsAsync(string input, int size)
    {
        return await _testCases.AnyAsync(tc => tc.Input.Equals(input) && tc.Size == size);
    }

    /// <inheritdoc />
    public async Task<bool> IsDeletableAsync(Guid id)
    {
        return await _testCases.AnyAsync(tc => tc.Id.Equals(id) && tc.TestResults != null && tc.TestResults.Count == 0);
    }

    /// <inheritdoc />
    public async Task<TestCase?> GetAsync(Guid id)
    {
        return await _testCases.Where(tc => tc.Id.Equals(id)).AsNoTracking().SingleOrDefaultAsync();
    }

    /// <inheritdoc />
    public async Task<TestCase> GetAsync(string input, int size)
    {
        return await _testCases.Where(tc => tc.Input.Equals(input) && tc.Size == size).AsNoTracking().SingleAsync();
    }

    /// <inheritdoc />
    public async Task<int> GetTestResultsCountAsync(Guid id)
    {
        return await _testCases.Where(tc => tc.Id.Equals(id))
            .Select(tc => tc.TestResults != null ? tc.TestResults.Count : 0).SingleAsync();
    }

    /// <inheritdoc />
    public async Task<List<TestCaseDto>> GetEnabledDtoListAsync()
    {
        return await _testCases.Where(tc => tc.Enabled).Select(tc => new TestCaseDto
        {
            Input = tc.Input,
            Size = tc.Size
        }).ToListAsync();
    }

    /// <inheritdoc />
    public async Task<List<TestCase>> GetListAsync()
    {
        return await _testCases.AsNoTracking().ToListAsync();
    }

    /// <inheritdoc />
    public async Task UpdateEnabledAsync(Guid id, bool enabled)
    {
        TestCase testCase = await _testCases.Where(tc => tc.Id.Equals(id)).SingleAsync();
        testCase.Enabled = enabled;
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        _testCases.Remove(await _testCases.Where(tc => tc.Id.Equals(id)).SingleAsync());
    }
}

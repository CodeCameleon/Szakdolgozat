using Microsoft.EntityFrameworkCore;
using TestResults.Entities;
using TestResults.EntityFramework;
using TestResults.Repositories.Interfaces;

namespace TestResults.Repositories.Implementations;

/// <summary>
/// A memóriahasználat eredményeket kezelő adattárat megvalósító osztály.
/// </summary>
public class MemoryUsageResultRepository
    : IMemoryUsageResultRepository
{
    /// <summary>
    /// A memóriahasználat eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    private readonly DbSet<MemoryUsageResult> _memoryUsageResults;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public MemoryUsageResultRepository(TestResultsDbContext context)
    {
        _memoryUsageResults = context.MemoryUsageResults;
    }

    /// <inheritdoc />
    public async Task CreateAsync(MemoryUsageResult memoryUsageResult)
    {
        await _memoryUsageResults.AddAsync(memoryUsageResult);
    }

    /// <inheritdoc />
    public async Task<MemoryUsageResult> GetAsync(Guid id)
    {
        return await _memoryUsageResults.Where(mur => mur.Id.Equals(id)).AsNoTracking().SingleAsync();
    }

    /// <inheritdoc />
    public async Task<List<MemoryUsageResult>> GetListAsync()
    {
        return await _memoryUsageResults.AsNoTracking().ToListAsync();
    }
}

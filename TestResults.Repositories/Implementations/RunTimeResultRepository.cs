using Microsoft.EntityFrameworkCore;
using TestResults.Entities;
using TestResults.EntityFramework;
using TestResults.Repositories.Interfaces;

namespace TestResults.Repositories.Implementations;

/// <summary>
/// A futási idő eredményeket kezelő adattárat megvalósító osztály.
/// </summary>
public class RunTimeResultRepository
    : IRunTimeResultRepository
{
    /// <summary>
    /// A futási idő eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    private readonly DbSet<RunTimeResult> _runTimeResults;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public RunTimeResultRepository(TestResultsDbContext context)
    {
        _runTimeResults = context.RunTimeResults;
    }

    /// <inheritdoc />
    public async Task CreateAsync(RunTimeResult runTimeResult)
    {
        await _runTimeResults.AddAsync(runTimeResult);
    }

    /// <inheritdoc />
    public async Task<RunTimeResult> GetAsync(Guid id)
    {
        return await _runTimeResults.Where(rtr => rtr.Id.Equals(id)).AsNoTracking().SingleAsync();
    }

    /// <inheritdoc />
    public async Task<List<RunTimeResult>> GetListAsync()
    {
        return await _runTimeResults.AsNoTracking().ToListAsync();
    }
}

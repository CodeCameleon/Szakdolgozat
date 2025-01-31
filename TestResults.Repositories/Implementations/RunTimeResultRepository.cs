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
}

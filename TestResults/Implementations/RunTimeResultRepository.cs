using TestResults.Context;
using TestResults.Entities;
using TestResults.Interfaces;

namespace TestResults.Implementations;

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
    /// A tranzakció kezelőt tároló adattag.
    /// </summary>
    private readonly ITransactionManager _transactionManager;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    /// <param name="transactionManager">A tranzakció kezelő példánya.</param>
    public RunTimeResultRepository(TestResultsDbContext context, ITransactionManager transactionManager)
    {
        _runTimeResults = context.RunTimeResults;
        _transactionManager = transactionManager;
    }

    /// <inheritdoc />
    public async Task CreateAsync(RunTimeResult runTimeResult)
    {
        await _transactionManager.BeginTransactionAsync();

        await _runTimeResults.AddAsync(runTimeResult);

        await _transactionManager.CommitTransactionAsync();
    }

    /// <inheritdoc />
    public async Task<RunTimeResult> GetAsync(Guid id)
    {
        return await _runTimeResults.Where(rtr => rtr.Id.Equals(id))
            .AsNoTracking()
            .SingleAsync();
    }

    /// <inheritdoc />
    public async Task<List<RunTimeResult>> GetListAsync()
    {
        return await _runTimeResults.AsNoTracking()
            .ToListAsync();
    }
}

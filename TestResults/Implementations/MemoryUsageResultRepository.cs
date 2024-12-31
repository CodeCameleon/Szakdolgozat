using TestResults.Context;
using TestResults.Entities;
using TestResults.Interfaces;

namespace TestResults.Implementations;

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
    /// A tranzakció kezelőt tároló adattag.
    /// </summary>
    private readonly ITransactionManager _transactionManager;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    /// <param name="transactionManager">A tranzakció kezelő példánya.</param>
    public MemoryUsageResultRepository(TestResultsDbContext context, ITransactionManager transactionManager)
    {
        _memoryUsageResults = context.MemoryUsageResults;
        _transactionManager = transactionManager;
    }

    /// <inheritdoc />
    public async Task CreateAsync(MemoryUsageResult memoryUsageResult)
    {
        await _transactionManager.BeginTransactionAsync();

        await _memoryUsageResults.AddAsync(memoryUsageResult);

        await _transactionManager.CommitTransactionAsync();
    }

    /// <inheritdoc />
    public async Task<MemoryUsageResult> GetAsync(Guid id)
    {
        return await _memoryUsageResults.Where(mur => mur.Id.Equals(id))
            .AsNoTracking()
            .SingleAsync();
    }

    /// <inheritdoc />
    public async Task<List<MemoryUsageResult>> GetListAsync()
    {
        return await _memoryUsageResults.AsNoTracking()
            .ToListAsync();
    }
}

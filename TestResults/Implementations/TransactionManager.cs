using Microsoft.EntityFrameworkCore.Storage;
using TestResults.Context;
using TestResults.Interfaces;

namespace TestResults.Implementations;

/// <summary>
/// Az adatbázis kontextus tranzakció kezelőjét megvalósító osztály.
/// </summary>
public class TransactionManager
    : ITransactionManager
{
    /// <summary>
    /// Az adatbázis kontextust tároló adattag.
    /// </summary>
    private readonly TestResultsDbContext _context;

    /// <summary>
    /// Az aktuális tranzakciót tároló adattag.
    /// </summary>
    private IDbContextTransaction _transaction;

    /// <summary>
    /// A tranzakció kezelő konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public TransactionManager(TestResultsDbContext context)
    {
        _context = context;
        _transaction = null;
    }

    /// <inheritdoc />
    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            await RollbackTransactionAsync();
        }

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    /// <inheritdoc />
    public async Task CommitTransactionAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
            await DisposeTransactionAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
    }

    /// <inheritdoc />
    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await DisposeTransactionAsync();
        }
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync()
    {
        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            await RollbackTransactionAsync();
            throw;
        }
    }

    /// <summary>
    /// Felszabadítja az osztály erőforrásait.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await DisposeTransactionAsync();
        }

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Felszabadítja az osztály erőforrásait.
    /// </summary>
    public void Dispose()
    {
        if (_transaction != null)
        {
            DisposeTransaction();
        }

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Felszabadítja a folyamatban lévő tranzakciót.
    /// </summary>
    private async Task DisposeTransactionAsync()
    {
        await _transaction.DisposeAsync();
        _transaction = null;
    }

    /// <summary>
    /// Felszabadítja a folyamatban lévő tranzakciót.
    /// </summary>
    private void DisposeTransaction()
    {
        _transaction.Dispose();
        _transaction = null;
    }
}

using Microsoft.EntityFrameworkCore.Storage;
using TestResults.EntityFramework;
using TestResults.UnitofWork.Interfaces;

namespace TestResults.UnitofWork.Implementations;

/// <summary>
/// A tesztek eredményeit kezelő egységmunkát megvalósító osztály.
/// </summary>
public class TestResultsUnitofWork
    : ITestResultsUnitofWork
{
    /// <summary>
    /// Az adatbázis kontextust tároló adattag.
    /// </summary>
    private readonly TestResultsDbContext _context;

    /// <summary>
    /// Az aktuális tranzakciót tároló adattag.
    /// </summary>
    private IDbContextTransaction? _transaction;

    /// <summary>
    /// Az egységmunka konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public TestResultsUnitofWork(TestResultsDbContext context)
    {
        _context = context;
    }

    /// <inheritdoc />
    public async Task BeginTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
        }

        _transaction = await _context.Database.BeginTransactionAsync();
    }

    /// <inheritdoc />
    public async Task CommitTransactionAsync()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException(nameof(_transaction));
        }

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <inheritdoc />
    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    /// <inheritdoc />
    public async Task SaveChangesAsync()
    {
        if (_transaction == null)
        {
            throw new NullReferenceException(nameof(_transaction));
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
            throw;
        }
    }

    /// <summary>
    /// Felszabadítja az osztály erőforrásait.
    /// </summary>
    public void Dispose()
    {
        if (_transaction != null)
        {
            _transaction.Dispose();
            _transaction = null;
        }
        _context.Dispose();

        GC.SuppressFinalize(this);
    }
}

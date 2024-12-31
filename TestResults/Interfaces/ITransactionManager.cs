namespace TestResults.Interfaces;

/// <summary>
/// Az adatbázis kontextus tranzakció kezelőjét ábrázoló interfész.
/// </summary>
public interface ITransactionManager
    : IAsyncDisposable, IDisposable
{
    /// <summary>
    /// Elindít egy új tranzakciót az adatbázis kontextuson.
    /// </summary>
    Task BeginTransactionAsync();

    /// <summary>
    /// Véglegesíti a folyamatban lévő tranzakciót.
    /// </summary>
    Task CommitTransactionAsync();

    /// <summary>
    /// Elveti a folyamatban lévő tranzakciót.
    /// </summary>
    Task RollbackTransactionAsync();

    /// <summary>
    /// Menti a változtatásokat a folyamatban lévő tranzakcióban.
    /// </summary>
    Task SaveChangesAsync();
}

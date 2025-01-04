using Microsoft.EntityFrameworkCore.Design;
using TestResults.Helpers;

namespace TestResults.Context;

/// <summary>
/// Az adatbázis kontextust létrehozó osztály.
/// </summary>
public class TestResultsDbContextFactory
    : IDesignTimeDbContextFactory<TestResultsDbContext>
{
    /// <summary>
    /// Létrehoz egy új példányt a <see cref="TestResultsDbContext"/> osztályból.
    /// </summary>
    /// <param name="args">Argumentumok.</param>
    /// <returns>Az adatbázis kontextus példánya.</returns>
    public TestResultsDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<TestResultsDbContext> optionsBuilder = new();

        optionsBuilder.UseSqlite(StringHelper.DefaultConnectionString);

        return new TestResultsDbContext(optionsBuilder.Options);
    }
}

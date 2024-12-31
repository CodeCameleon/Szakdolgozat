using Microsoft.EntityFrameworkCore.Design;

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

        optionsBuilder.UseSqlite(@"Data Source=./../../../Database/TestResults.db");

        return new TestResultsDbContext(optionsBuilder.Options);
    }
}

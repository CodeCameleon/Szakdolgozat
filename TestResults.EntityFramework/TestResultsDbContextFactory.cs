using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Shared.Constants;

namespace TestResults.EntityFramework;

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
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppSettings.BasePath)
            .AddJsonFile(AppSettings.DevelopmentJson, optional: false)
            .Build();

        string connectionString = configuration.GetConnectionString(AppSettings.DefaultConnection)
            ?? throw new InvalidOperationException(ErrorMessages.DefaultConnectionNotFound);

        DbContextOptionsBuilder<TestResultsDbContext> optionsBuilder = new();
        optionsBuilder.UseSqlite(connectionString);

        return new TestResultsDbContext(optionsBuilder.Options);
    }
}

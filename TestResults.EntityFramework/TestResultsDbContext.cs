using Microsoft.EntityFrameworkCore;
using TestResults.Entities;

namespace TestResults.EntityFramework;

/// <summary>
/// A tesztek eredményeit tároló adatbázis kontextus.
/// </summary>
public class TestResultsDbContext
    : DbContext
{
    /// <summary>
    /// A memóriahasználat eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    public DbSet<MemoryUsageResult> MemoryUsageResults { get; set; }

    /// <summary>
    /// A futási idő eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    public DbSet<RunTimeResult> RunTimeResults { get; set; }

    /// <summary>
    /// Az adatbázis kontextus konstruktora.
    /// </summary>
    /// <param name="options">Az adatbázis kontextus beállítási.</param>
    public TestResultsDbContext(DbContextOptions<TestResultsDbContext> options)
        : base(options)
    {
    }

    /// <summary>
    /// Az adatbázis kontextus beállítását végző függvény.
    /// </summary>
    /// <param name="optionsBuilder">Az adatbázis kontextust beállító építő példánya.</param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    /// <summary>
    /// Az adatbázis kontextus model építését végző függvény.
    /// </summary>
    /// <param name="modelBuilder">Az adatbázis kontextus model építő példánya.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MemoryUsageResult>(entity =>
        {
            entity.HasIndex(mur => mur.AlgorithmName);
        });

        modelBuilder.Entity<RunTimeResult>(entity =>
        {
            entity.HasIndex(rtr => rtr.AlgorithmName);
        });
    }
}

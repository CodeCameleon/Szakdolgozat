using Microsoft.EntityFrameworkCore;
using Shared.Enums;
using TestResults.Entities;

namespace TestResults.EntityFramework;

/// <summary>
/// A tesztek eredményeit tároló adatbázis kontextus.
/// </summary>
public class TestResultsDbContext
    : DbContext
{
    /// <summary>
    /// Az algoritmusokat tartalmazó adatbázis tábla.
    /// </summary>
    public DbSet<Algorithm> Algorithms { get; set; }

    /// <summary>
    /// Az algoritmus típusokat tartalmazó adatbázis tábla.
    /// </summary>
    public DbSet<AlgorithmType> AlgorithmTypes { get; set; }

    /// <summary>
    /// A memóriahasználat eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    public DbSet<MemoryUsageResult> MemoryUsageResults { get; set; }

    /// <summary>
    /// A futási idő eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    public DbSet<RunTimeResult> RunTimeResults { get; set; }

    /// <summary>
    /// A teszteseteket tartalmazó adatbázis tábla.
    /// </summary>
    public DbSet<TestCase> TestCases { get; set; }

    /// <summary>
    /// A teszteredményeket tartalmazó adatbázis tábla.
    /// </summary>
    public DbSet<TestResult> TestResults { get; set; }

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

        modelBuilder.Entity<Algorithm>(entity =>
        {
            entity.HasOne(a => a.Type)
                .WithMany(at => at.Algorithms)
                .HasForeignKey(a => a.TypeId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<AlgorithmType>().HasData(
            Enum.GetValues<EAlgorithmType>().Select(e => new AlgorithmType
            {
                Id = (int)e,
                Type = e.ToString()
            })
        );

        modelBuilder.Entity<MemoryUsageResult>(entity =>
        {
            entity.HasOne(mur => mur.TestResult)
                .WithOne(tr => tr.MemoryUsageResult)
                .HasForeignKey<MemoryUsageResult>(mur => mur.TestResultId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<RunTimeResult>(entity =>
        {
            entity.HasOne(rtr => rtr.TestResult)
                .WithOne(tr => tr.RunTimeResult)
                .HasForeignKey<RunTimeResult>(rtr => rtr.TestResultId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<TestResult>(entity =>
        {
            entity.HasOne(tr => tr.Algorithm)
                .WithMany(a => a.TestResults)
                .HasForeignKey(tr => tr.AlgorithmId)
                .OnDelete(DeleteBehavior.Restrict);
            entity.HasOne(tr => tr.TestCase)
                .WithMany(tc => tc.TestResults)
                .HasForeignKey(tr => tr.TestCaseId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

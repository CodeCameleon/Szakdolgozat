using Microsoft.EntityFrameworkCore;
using TestResults.Entities;
using TestResults.EntityFramework;
using TestResults.Repositories.Interfaces;

namespace TestResults.Repositories.Implementations;

/// <summary>
/// Az algoritmusokat kezelő adattárat megvalósító osztály.
/// </summary>
public class AlgorithmRepository
    : IAlgorithmRepository
{
    /// <summary>
    /// Az algoritmusokat tartalmazó adatbázis tábla.
    /// </summary>
    private readonly DbSet<Algorithm> _algorithms;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public AlgorithmRepository(TestResultsDbContext context)
    {
        _algorithms = context.Algorithms;
    }

    /// <inheritdoc />
    public async Task CreateAsync(Algorithm algorithm)
    {
        await _algorithms.AddAsync(algorithm);
    }
    
    /// <inheritdoc />
    public async Task<Algorithm?> GetAsync(int id)
    {
        return await _algorithms.Where(a => a.Id == id).AsNoTracking().SingleOrDefaultAsync();
    }
}

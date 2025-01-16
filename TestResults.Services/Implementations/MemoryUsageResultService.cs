using TestResults.Entities;
using TestResults.Repositories.Interfaces;
using TestResults.Services.Interfaces;
using TestResults.UnitofWork.Interfaces;

namespace TestResults.Services.Implementations;

/// <summary>
/// A memóriahasználat eredményeket kezelő szolgáltatást megvalósító osztály.
/// </summary>
public class MemoryUsageResultService
    : IMemoryUsageResultService
{
    /// <summary>
    /// A memóriahasználat eredményeket kezelő adattárat tároló adattag.
    /// </summary>
    private readonly IMemoryUsageResultRepository _memoryUsageResultRepository;

    /// <summary>
    /// A tesztek eredményeit kezelő egységmunkát tároló adattag.
    /// </summary>
    private readonly ITestResultsUnitofWork _testResultsUnitofWork;

    /// <summary>
    /// A szolgáltatás konstruktora.
    /// </summary>
    /// <param name="memoryUsageResultRepository">A memóriahasználat eredményeket kezelő adattár példánya.</param>
    /// <param name="unitofWork">A tesztek eredményeit kezelő egységmunka példánya.</param>
    public MemoryUsageResultService(IMemoryUsageResultRepository memoryUsageResultRepository,
        ITestResultsUnitofWork testResultsUnitofWork)
    {
        _memoryUsageResultRepository = memoryUsageResultRepository;
        _testResultsUnitofWork = testResultsUnitofWork;
    }

    /// <inheritdoc />
    public async Task CreateAsync(MemoryUsageResult memoryUsageResult)
    {
        await _testResultsUnitofWork.BeginTransactionAsync();

        await _memoryUsageResultRepository.CreateAsync(memoryUsageResult);

        await _testResultsUnitofWork.CommitTransactionAsync();
    }
}

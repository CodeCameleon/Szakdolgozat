using TestResults.Entities;
using TestResults.Repositories.Interfaces;
using TestResults.Services.Interfaces;
using TestResults.UnitofWork.Interfaces;

namespace TestResults.Services.Implementations;

/// <summary>
/// A futási idő eredményeket kezelő szolgáltatást megvalósító osztály.
/// </summary>
public class RunTimeResultService
    : IRunTimeResultService
{
    /// <summary>
    /// A futási idő eredményeket kezelő adattárat tároló adattag.
    /// </summary>
    private readonly IRunTimeResultRepository _runTimeResultRepository;

    /// <summary>
    /// A tesztek eredményeit kezelő egységmunkát tároló adattag.
    /// </summary>
    private readonly ITestResultsUnitofWork _testResultsUnitofWork;
    
    /// <summary>
    /// A szolgáltatás konstruktora.
    /// </summary>
    /// <param name="runTimeResultRepository">A futási idő eredményeket kezelő adattár példánya.</param>
    /// <param name="unitofWork">A tesztek eredményeit kezelő egységmunka példánya.</param>
    public RunTimeResultService(IRunTimeResultRepository runTimeResultRepository,
        ITestResultsUnitofWork testResultsUnitofWork)
    {
        _runTimeResultRepository = runTimeResultRepository;
        _testResultsUnitofWork = testResultsUnitofWork;
    }

    /// <inheritdoc />
    public async Task CreateAsync(RunTimeResult runTimeResult)
    {
        await _testResultsUnitofWork.BeginTransactionAsync();

        await _runTimeResultRepository.CreateAsync(runTimeResult);

        await _testResultsUnitofWork.CommitTransactionAsync();
    }

    /// <inheritdoc />
    public async Task<RunTimeResult> GetAsync(Guid id)
    {
        return await _runTimeResultRepository.GetAsync(id);
    }

    /// <inheritdoc />
    public async Task<List<RunTimeResult>> GetListAsync()
    {
        return await _runTimeResultRepository.GetListAsync();
    }
}

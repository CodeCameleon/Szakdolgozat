using TestResults.Dtos;
using TestResults.Entities;
using TestResults.Repositories.Interfaces;
using TestResults.Services.Interfaces;
using TestResults.UnitOfWork.Interfaces;

namespace TestResults.Services.Implementations;

/// <summary>
/// A teszteseteket kezelő szolgáltatást megvalósító osztály.
/// </summary>
public class TestCaseService
    : ITestCaseService
{
    /// <summary>
    /// A teszteseteket kezelő adattárat tároló adattag.
    /// </summary>
    private readonly ITestCaseRepository _testCaseRepository;

    /// <summary>
    /// A tesztek eredményeit kezelő egységmunkát tároló adattag.
    /// </summary>
    private readonly ITestResultsUnitOfWork _testResultsUnitOfWork;

    /// <summary>
    /// A szolgáltatás konstruktora.
    /// </summary>
    /// <param name="testCaseRepository">A teszteseteket kezelő adattár példánya.</param>
    /// <param name="testResultsUnitOfWork">A tesztek eredményeit kezelő egységmunka példánya.</param>
    public TestCaseService(ITestCaseRepository testCaseRepository,
        ITestResultsUnitOfWork testResultsUnitOfWork)
    {
        _testCaseRepository = testCaseRepository;
        _testResultsUnitOfWork = testResultsUnitOfWork;
    }

    /// <inheritdoc />
    public async Task CreateAsync(TestCase testCase)
    {
        await _testResultsUnitOfWork.BeginTransactionAsync();

        await _testCaseRepository.CreateAsync(testCase);

        await _testResultsUnitOfWork.CommitTransactionAsync();
    }

    /// <inheritdoc />
    public async Task<bool> ExistsAsync(string input, int size)
    {
        return await _testCaseRepository.ExistsAsync(input, size);
    }

    /// <inheritdoc />
    public async Task<bool> IsDeletableAsync(Guid id)
    {
        return await _testCaseRepository.IsDeletableAsync(id);
    }

    /// <inheritdoc />
    public async Task<TestCase?> GetAsync(Guid id)
    {
        return await _testCaseRepository.GetAsync(id);
    }

    /// <inheritdoc />
    public async Task<List<TestCaseDto>> GetEnabledDtoListAsync()
    {
        return await _testCaseRepository.GetEnabledDtoListAsync();
    }
    
    /// <inheritdoc />
    public async Task<List<TestCase>> GetListAsync()
    {
        return await _testCaseRepository.GetListAsync();
    }

    /// <inheritdoc />
    public async Task UpdateEnabledAsync(Guid id, bool enabled)
    {
        await _testResultsUnitOfWork.BeginTransactionAsync();

        await _testCaseRepository.UpdateEnabledAsync(id, enabled);

        await _testResultsUnitOfWork.CommitTransactionAsync();
    }

    /// <inheritdoc />
    public async Task DeleteAsync(Guid id)
    {
        await _testResultsUnitOfWork.BeginTransactionAsync();

        await _testCaseRepository.DeleteAsync(id);

        await _testResultsUnitOfWork.CommitTransactionAsync();
    }
}

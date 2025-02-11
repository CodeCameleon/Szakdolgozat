using Shared.Constants;
using TestResults.Dtos;
using TestResults.Entities;
using TestResults.Repositories.Interfaces;
using TestResults.Services.Interfaces;
using TestResults.UnitOfWork.Interfaces;

namespace TestResults.Services.Implementations;

/// <summary>
/// A memóriahasználat eredményeket kezelő szolgáltatást megvalósító osztály.
/// </summary>
public class MemoryUsageResultService
    : IMemoryUsageResultService
{
    /// <summary>
    /// Az algoritmusokat kezelő adattárat tároló adattag.
    /// </summary>
    private readonly IAlgorithmRepository _algorithmRepository;

    /// <summary>
    /// A memóriahasználat eredményeket kezelő adattárat tároló adattag.
    /// </summary>
    private readonly IMemoryUsageResultRepository _memoryUsageResultRepository;

    /// <summary>
    /// A teszteseteket kezelő adattárat tároló adattag.
    /// </summary>
    private readonly ITestCaseRepository _testCaseRepository;

    /// <summary>
    /// A teszteredményeket kezelő adattárat tároló adattag.
    /// </summary>
    private readonly ITestResultRepository _testResultRepository;

    /// <summary>
    /// A tesztek eredményeit kezelő egységmunkát tároló adattag.
    /// </summary>
    private readonly ITestResultsUnitOfWork _testResultsUnitOfWork;

    /// <summary>
    /// A szolgáltatás konstruktora.
    /// </summary>
    /// <param name="algorithmRepository">Az algoritmusokat kezelő adattár példánya.</param>
    /// <param name="memoryUsageResultRepository">A memóriahasználat eredményeket kezelő adattár példánya.</param>
    /// <param name="testCaseRepository">A teszteseteket kezelő adattár példánya.</param>
    /// <param name="testResultRepository">A teszteredményeket kezelő adattár példánya.</param>
    /// <param name="testResultsUnitOfWork">A tesztek eredményeit kezelő egységmunka példánya.</param>
    public MemoryUsageResultService(IAlgorithmRepository algorithmRepository,
        IMemoryUsageResultRepository memoryUsageResultRepository,
        ITestCaseRepository testCaseRepository,
        ITestResultRepository testResultRepository,
        ITestResultsUnitOfWork testResultsUnitOfWork)
    {
        _algorithmRepository = algorithmRepository;
        _memoryUsageResultRepository = memoryUsageResultRepository;
        _testCaseRepository = testCaseRepository;
        _testResultRepository = testResultRepository;
        _testResultsUnitOfWork = testResultsUnitOfWork;
    }

    /// <inheritdoc />
    public async Task CreateAsync(MemoryUsageResultDto memoryUsageResultDto)
    {
        await _testResultsUnitOfWork.BeginTransactionAsync();

        Algorithm? algorithm = await _algorithmRepository.GetAsync(memoryUsageResultDto.AlgorithmName);

        if (algorithm == null)
        {
            algorithm = new Algorithm
            {
                Name = memoryUsageResultDto.AlgorithmName,
                TypeId = (int)memoryUsageResultDto.AlgorithmType
            };

            await _algorithmRepository.CreateAsync(algorithm);

            await _testResultsUnitOfWork.SaveChangesAsync();
        }

        TestCaseDto testCaseDto = memoryUsageResultDto.TestCase;
        TestCase testCase = await _testCaseRepository.GetAsync(testCaseDto.Input, testCaseDto.Size);

        if (!testCase.Enabled)
        {
            throw new ArgumentException(ErrorMessages.TestCaseNotEnabled);
        }

        TestResult testResult = new()
        {
            IsSuccessful = memoryUsageResultDto.IsSuccessful,
            AlgorithmId = algorithm.Id,
            TestCaseId = testCase.Id
        };

        await _testResultRepository.CreateAsync(testResult);

        await _testResultsUnitOfWork.SaveChangesAsync();

        await _memoryUsageResultRepository.CreateAsync(new MemoryUsageResult
        {
            TestResultId = testResult.Id,
            EncryptionMemoryUsage = memoryUsageResultDto.EncryptionMemoryUsage,
            DecryptionMemoryUsage = memoryUsageResultDto.DecryptionMemoryUsage
        });

        await _testResultsUnitOfWork.CommitTransactionAsync();
    }
}

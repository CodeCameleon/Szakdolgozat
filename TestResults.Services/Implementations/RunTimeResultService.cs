using Shared.Constants;
using TestResults.Dtos;
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
    /// Az algoritmusokat kezelő adattárat tároló adattag.
    /// </summary>
    private readonly IAlgorithmRepository _algorithmRepository;

    /// <summary>
    /// A futási idő eredményeket kezelő adattárat tároló adattag.
    /// </summary>
    private readonly IRunTimeResultRepository _runTimeResultRepository;

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
    private readonly ITestResultsUnitofWork _testResultsUnitofWork;

    /// <summary>
    /// A szolgáltatás konstruktora.
    /// </summary>
    /// <param name="algorithmRepository">Az algoritmusokat kezelő adattár példánya.</param>
    /// <param name="runTimeResultRepository">A futási idő eredményeket kezelő adattár példánya.</param>
    /// <param name="testCaseRepository">A teszteseteket kezelő adattár példánya.</param>
    /// <param name="testResultRepository">A teszteredményeket kezelő adattár példánya.</param>
    /// <param name="testResultsUnitofWork">A tesztek eredményeit kezelő egységmunka példánya.</param>
    public RunTimeResultService(IAlgorithmRepository algorithmRepository,
        IRunTimeResultRepository runTimeResultRepository,
        ITestCaseRepository testCaseRepository,
        ITestResultRepository testResultRepository,
        ITestResultsUnitofWork testResultsUnitofWork)
    {
        _algorithmRepository = algorithmRepository;
        _runTimeResultRepository = runTimeResultRepository;
        _testCaseRepository = testCaseRepository;
        _testResultRepository = testResultRepository;
        _testResultsUnitofWork = testResultsUnitofWork;
    }

    /// <inheritdoc />
    public async Task CreateAsync(RunTimeResultDto runTimeResultDto)
    {
        await _testResultsUnitofWork.BeginTransactionAsync();

        Algorithm? algorithm = await _algorithmRepository.GetAsync(runTimeResultDto.AlgorithmName);

        if (algorithm == null)
        {
            algorithm = new Algorithm
            {
                Name = runTimeResultDto.AlgorithmName,
                TypeId = (int)runTimeResultDto.AlgorithmType
            };

            await _algorithmRepository.CreateAsync(algorithm);

            await _testResultsUnitofWork.SaveChangesAsync();
        }

        TestCaseDto testCaseDto = runTimeResultDto.TestCase;
        TestCase testCase = await _testCaseRepository.GetAsync(testCaseDto.Input, testCaseDto.Size);

        if (!testCase.Enabled)
        {
            throw new ArgumentException(ErrorMessages.TestCaseNotEnabled);
        }

        TestResult testResult = new()
        {
            IsSuccessful = runTimeResultDto.IsSuccessful,
            AlgorithmId = algorithm.Id,
            TestCaseId = testCase.Id
        };

        await _testResultRepository.CreateAsync(testResult);

        await _testResultsUnitofWork.SaveChangesAsync();

        await _runTimeResultRepository.CreateAsync(new RunTimeResult
        {
            TestResultId = testResult.Id,
            TimeToEncrypt = runTimeResultDto.TimeToEncrypt,
            TimeToDecrypt = runTimeResultDto.TimeToDecrypt
        });

        await _testResultsUnitofWork.CommitTransactionAsync();
    }
}

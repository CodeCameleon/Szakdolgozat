using TestResults.Entities;
using TestResults.Repositories.Interfaces;
using TestResults.Services.Interfaces;
using TestResults.UnitofWork.Interfaces;

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
    private readonly ITestResultsUnitofWork _testResultsUnitofWork;

    /// <summary>
    /// A szolgáltatás konstruktora.
    /// </summary>
    /// <param name="testCaseRepository">A teszteseteket kezelő adattár példánya.</param>
    /// <param name="testResultsUnitofWork">A tesztek eredményeit kezelő egységmunka példánya.</param>
    public TestCaseService(ITestCaseRepository testCaseRepository,
        ITestResultsUnitofWork testResultsUnitofWork)
    {
        _testCaseRepository = testCaseRepository;
        _testResultsUnitofWork = testResultsUnitofWork;
    }

    /// <inheritdoc />
    public async Task<bool> CreateAsync(string input)
    {
        bool result = false;
        await _testResultsUnitofWork.BeginTransactionAsync();

        if (await _testCaseRepository.NotExistsAsync(input))
        {
            TestCase testCase = new()
            {
                Enabled = true,
                Input = input
            };

            await _testCaseRepository.CreateAsync(testCase);

            result = true;
        }

        await _testResultsUnitofWork.CommitTransactionAsync();
        return result;
    }

    /// <inheritdoc />
    public async Task<List<string>> GetEnabledInputListAsync()
    {
        return await _testCaseRepository.GetEnabledInputListAsync();
    }

    /// <summary>
    /// Felszabadítja a használt erőforrásokat.
    /// </summary>
    public void Dispose()
    {
        _testResultsUnitofWork.Dispose();

        GC.SuppressFinalize(this);
    }
}

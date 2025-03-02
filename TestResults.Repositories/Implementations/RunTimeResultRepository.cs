using Microsoft.EntityFrameworkCore;
using Shared.Constants;
using Shared.Enums;
using Shared.Enums.Extensions;
using TestResults.Dtos;
using TestResults.Entities;
using TestResults.EntityFramework;
using TestResults.Repositories.Interfaces;

namespace TestResults.Repositories.Implementations;

/// <summary>
/// A futási idő eredményeket kezelő adattárat megvalósító osztály.
/// </summary>
public class RunTimeResultRepository
    : IRunTimeResultRepository
{
    /// <summary>
    /// A futási idő eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    private readonly DbSet<RunTimeResult> _runTimeResults;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public RunTimeResultRepository(TestResultsDbContext context)
    {
        _runTimeResults = context.RunTimeResults;
    }

    /// <inheritdoc />
    public async Task CreateAsync(RunTimeResult runTimeResult)
    {
        await _runTimeResults.AddAsync(runTimeResult);
    }

    /// <inheritdoc />
    public async Task<List<DatasetDto>> GetDatasetListAsync(EAlgorithmName? algorithm, EAlgorithmType? type)
    {
        IQueryable<RunTimeResult> query = _runTimeResults.Include(rtr => rtr.TestResult)
            .ThenInclude(tr => tr!.TestCase)
            .Where(rtr => rtr.TestResult!.IsSuccessful);

        if (algorithm.HasValue)
        {
            query = query.Where(rtr => rtr.TestResult!.AlgorithmId == (int)algorithm.Value);
        }

        if (type.HasValue)
        {
            query = query.Where(rtr => rtr.TestResult!.Algorithm!.TypeId == (int)type.Value);
        }

        List<RunTimeResult> results = await query.ToListAsync();

        List<IGrouping<EAlgorithmName, RunTimeResult>> groupedResults = results.GroupBy(rtr =>
            (EAlgorithmName)rtr.TestResult!.AlgorithmId
        ).ToList();

        return groupedResults.SelectMany(group => new List<DatasetDto>
        {
            new()
            {
                Label = group.Key.GetDisplayName() + ChartTypes.LabelEncrypt,
                DataList = group.Where(rtr => rtr.TimeToEncrypt != 0)
                    .Select(rtr => new DataDto
                    {
                        TestCaseSize = rtr.TestResult!.TestCase!.Size,
                        TestResult = rtr.TimeToEncrypt
                    })
                    .ToList(),
                BorderColor = group.Key.GetBorderColor(),
                BackgroundColor = group.Key.GetBackgroundColor(),
                Type = ChartTypes.Scatter
            },
            new()
            {
                Label = group.Key.GetDisplayName() + ChartTypes.LabelDecrypt,
                DataList = group.Where(rtr => rtr.TimeToDecrypt != 0)
                    .Select(rtr => new DataDto
                    {
                        TestCaseSize = rtr.TestResult!.TestCase!.Size,
                        TestResult = rtr.TimeToDecrypt
                    })
                    .ToList(),
                BorderColor = group.Key.GetBorderColor(),
                BackgroundColor = group.Key.GetBackgroundColor(),
                Type = ChartTypes.Scatter
            },
        }).ToList();
    }
}

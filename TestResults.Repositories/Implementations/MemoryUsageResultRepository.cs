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
/// A memóriahasználat eredményeket kezelő adattárat megvalósító osztály.
/// </summary>
public class MemoryUsageResultRepository
    : IMemoryUsageResultRepository
{
    /// <summary>
    /// A memóriahasználat eredményeket tartalmazó adatbázis tábla.
    /// </summary>
    private readonly DbSet<MemoryUsageResult> _memoryUsageResults;

    /// <summary>
    /// Az adattár konstruktora.
    /// </summary>
    /// <param name="context">Az adatbázis kontextus példánya.</param>
    public MemoryUsageResultRepository(TestResultsDbContext context)
    {
        _memoryUsageResults = context.MemoryUsageResults;
    }

    /// <inheritdoc />
    public async Task CreateAsync(MemoryUsageResult memoryUsageResult)
    {
        await _memoryUsageResults.AddAsync(memoryUsageResult);
    }

    /// <inheritdoc />
    public async Task<List<DatasetDto>> GetDatasetListAsync(EAlgorithmName? algorithm)
    {
        IQueryable<MemoryUsageResult> query = _memoryUsageResults.Include(mur => mur.TestResult)
            .ThenInclude(tr => tr!.TestCase)
            .Where(mur => mur.TestResult!.IsSuccessful);

        if (algorithm.HasValue)
        {
            query = query.Where(mur => mur.TestResult!.AlgorithmId == (int)algorithm.Value);
        }

        List<MemoryUsageResult> results = await query.ToListAsync();

        List<IGrouping<EAlgorithmName, MemoryUsageResult>> groupedResults = results.GroupBy(mur =>
            (EAlgorithmName)mur.TestResult!.AlgorithmId
        ).ToList();

        List<DatasetDto> datasets = groupedResults.SelectMany(group => new List<DatasetDto>
        {
            new()
            {
                Label = group.Key.GetDisplayName() + ChartTypes.LabelEncrypt,
                DataList = group.Select(mur => new DataDto
                {
                    TestCaseSize = mur.TestResult!.TestCase!.Size,
                    TestResult = mur.EncryptionMemoryUsage
                }).ToList(),
                BorderColor = group.Key.GetBorderColor(),
                BackgroundColor = group.Key.GetBackgroundColor(),
                Type = ChartTypes.Scatter
            },
            new()
            {
                Label = group.Key.GetDisplayName() + ChartTypes.LabelDecrypt,
                DataList = group.Select(mur => new DataDto
                {
                    TestCaseSize = mur.TestResult!.TestCase!.Size,
                    TestResult = mur.DecryptionMemoryUsage
                }).ToList(),
                BorderColor = group.Key.GetBorderColor(),
                BackgroundColor = group.Key.GetBackgroundColor(),
                Type = ChartTypes.Scatter
            },
        }).ToList();

        List<DatasetDto> averagedDatasets = [];
        foreach (DatasetDto dataset in datasets)
        {
            List<DataDto> groupedData = dataset.DataList.GroupBy(data => data.TestCaseSize)
                .Select(group => new DataDto
                {
                    TestCaseSize = group.Key,
                    TestResult = group.Average(data => data.TestResult)
                })
                .ToList();

            DatasetDto averagedDataset = new()
            {
                Label = dataset.Label + ChartTypes.LabelAverage,
                DataList = groupedData,
                BorderColor = dataset.BorderColor,
                BackgroundColor = dataset.BackgroundColor,
                Type = ChartTypes.Line
            };

            averagedDatasets.Add(averagedDataset);
        }
        datasets.AddRange(averagedDatasets);

        return datasets;
    }
}

using Microsoft.AspNetCore.Mvc;
using Shared.Enums;
using System.Diagnostics;
using TestResults.Dtos;
using TestResults.Services.Interfaces;
using Thesis.WebApp.Constants;
using Thesis.WebApp.ViewModels;

namespace Thesis.WebApp.Controllers;

/// <summary>
/// A kezdőoldal vezérlője.
/// </summary>
public class HomeController
    : Controller
{
    /// <summary>
    /// A naplózót tároló adattag.
    /// </summary>
    private readonly ILogger<HomeController> _logger;

    /// <summary>
    /// A memóriahasználat eredményeket kezelő szolgáltatást tároló adattag.
    /// </summary>
    private readonly IMemoryUsageResultService _memoryUsageResultService;

    /// <summary>
    /// A futási idő eredményeket kezelő szolgáltatást tároló adattag.
    /// </summary>
    private readonly IRunTimeResultService _runTimeResultService;

    /// <summary>
    /// A vezérlő konstruktora.
    /// </summary>
    /// <param name="logger">A naplózó példánya.</param>
    /// <param name="memoryUsageResultService">A memóriahasználat eredményeket kezelő szolgáltatás példánya.</param>
    /// <param name="runTimeResultService">A futási idő eredményeket kezelő szolgáltatás példánya.</param>
    public HomeController(ILogger<HomeController> logger,
        IMemoryUsageResultService memoryUsageResultService,
        IRunTimeResultService runTimeResultService)
    {
        _logger = logger;
        _memoryUsageResultService = memoryUsageResultService;
        _runTimeResultService = runTimeResultService;
    }

    /// <summary>
    /// A kezdőoldal megjelenítése.
    /// </summary>
    /// <param name="algorithm">A megjelenítendő algoritmus.</param>
    /// <param name="type">A megjelenítendő algoritmus típusa.</param>
    /// <param name="testType">A teszt típusa.</param>
    /// <returns>A kezdőoldal nézete.</returns>
    [HttpGet]
    public async Task<IActionResult> Index(EAlgorithmName? algorithm, EAlgorithmType? type,
        string testType = TestNames.AlgorithmRunTime)
    {
        ViewData[ViewDataKeys.Algorithm] = algorithm;
        ViewData[ViewDataKeys.Type] = type;
        ViewData[ViewDataKeys.TestType] = testType;

        List<DatasetDto> datasets = [];
        if (testType.Equals(TestNames.AlgorithmMemoryUsage))
        {
            datasets.AddRange(await _memoryUsageResultService.GetDatasetListAsync(algorithm, type));
        }
        else
        {
            datasets.AddRange(await _runTimeResultService.GetDatasetListAsync(algorithm, type));
        }

        return View(datasets);
    }

    /// <summary>
    /// A hibaoldal megjelenítése.
    /// </summary>
    /// <returns>A hibaoldal nézete.</returns>
    [HttpGet]
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}

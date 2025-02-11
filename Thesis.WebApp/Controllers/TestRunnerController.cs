using Microsoft.AspNetCore.Mvc;
using Thesis.WebApp.Services.Interfaces;

namespace Thesis.WebApp.Controllers;

/// <summary>
/// Az NUnit tesztek vezérlője.
/// </summary>
public class TestRunnerController
    : Controller
{
    /// <summary>
    /// Az NUnit teszteket futtató szolgáltatást tároló adattag.
    /// </summary>
    private readonly ITestRunnerService _testRunnerService;

    /// <summary>
    /// A vezérlő konstruktora.
    /// </summary>
    /// <param name="testRunnerService">Az NUnit teszteket futtató szolgáltatás példánya.</param>
    public TestRunnerController(ITestRunnerService testRunnerService)
    {
        _testRunnerService = testRunnerService;
    }

    /// <summary>
    /// A tesztek főoldalának megjelenítése.
    /// </summary>
    /// <returns>A tesztek főoldalának nézete.</returns>
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Az algoritmusok memóriahasználatának tesztjeinek futtatása.
    /// </summary>
    /// <returns>A tesztek főoldalának nézete.</returns>
    [HttpGet]
    public IActionResult AlgorithmMemoryUsage()
    {
        return View(nameof(Index), _testRunnerService.RunAlgorithmMemoryUsageTests());
    }

    /// <summary>
    /// Az algoritmusok futási idejének tesztjeinek futtatása.
    /// </summary>
    /// <returns>A tesztek főoldalának nézete.</returns>
    [HttpGet]
    public IActionResult AlgorithmRunTime()
    {
        return View(nameof(Index), _testRunnerService.RunAlgorithmRunTimeTests());
    }

    /// <summary>
    /// Az algoritmusok tesztjeinek futtatása.
    /// </summary>
    /// <returns>A tesztek főoldalának nézete.</returns>
    [HttpGet]
    public IActionResult AlgorithmAll()
    {
        return View(nameof(Index), _testRunnerService.RunAlgorithmTests());
    }
}

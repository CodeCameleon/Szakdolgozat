using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using TestResults.Entities;
using TestResults.Services.Interfaces;
using Thesis.WebApp.ViewModels;

namespace Thesis.WebApp.Controllers;

/// <summary>
/// A tesztesetek oldalainak vezérlője.
/// </summary>
public class TestCaseController
    : Controller
{
    /// <summary>
    /// A teszteseteket kezelő szolgáltatást tároló adattag.
    /// </summary>
    private readonly ITestCaseService _testCaseService;

    /// <summary>
    /// A vezérlő konstruktora.
    /// </summary>
    /// <param name="testCaseService">A teszteseteket kezelő szolgáltatás példánya.</param>
    public TestCaseController(ITestCaseService testCaseService)
    {
        _testCaseService = testCaseService;
    }

    /// <summary>
    /// A tesztesetek főoldalának megjelenítése.
    /// </summary>
    /// <returns>A tesztesetek főoldalának nézete.</returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        List<TestCase> testCases = await _testCaseService.GetListAsync();

        return View(testCases.Select(tc => (TestCaseViewModel)tc));
    }

    /// <summary>
    /// A tesztesetek létrehozó oldal megjelenítése.
    /// </summary>
    /// <returns>A tesztesetek létrehozó oldal nézete.</returns>
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    /// <summary>
    /// Létrehoz egy új tesztesetet.
    /// </summary>
    /// <param name="viewModel">A létrehozni kívánt teszteset nézetmodelként.</param>
    /// <returns>A tesztesetek főoldalának nézete ha sikeres, különben a létrehozó oldal nézete.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Enabled,Input")] TestCaseViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        await _testCaseService.CreateAsync((TestCase)viewModel);

        return RedirectToAction(nameof(Index));
    }

    /// <summary>
    /// A tesztesetet részletező oldal megjelenítése.
    /// </summary>
    /// <param name="id">A részletezni kívánt teszteset azonosítója.</param>
    /// <returns>A tesztesetet részletező oldal nézete ha létezik a teszteset, különben hibaüzenet.</returns>
    [HttpGet]
    public async Task<IActionResult> Details(Guid? id)
    {
        if (id.HasValue)
        {
            TestCase? testCase = await _testCaseService.GetAsync(id.Value);

            if (testCase != null)
            {
                return View((TestCaseViewModel)testCase);
            }
        }

        return NotFound();
    }

    /// <summary>
    /// A tesztesetek törlő oldal megjelenítése.
    /// </summary>
    /// <param name="id">A törölni kívánt teszteset azonosítója.</param>
    /// <returns>A tesztesetek törlő oldal nézete ha létezik a teszteset, különben hibaüzenet.</returns>
    [HttpGet]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id.HasValue)
        {
            TestCase? testCase = await _testCaseService.GetAsync(id.Value);

            if (testCase != null)
            {
                return View((TestCaseViewModel)testCase);
            }
        }

        return NotFound();
    }

    /// <summary>
    /// Törli a tesztesetet.
    /// </summary>
    /// <param name="id">A törölni kívánt teszteset azonosítója.</param>
    /// <returns>A tesztesetek főoldalának nézete ha sikeres, különben a törlő oldal nézete.</returns>
    [HttpPost]
    public async Task<IActionResult> Delete(Guid id)
    {
        TestCase? testCase = await _testCaseService.GetAsync(id);

        if (testCase == null)
        {
            return NotFound();
        }

        if (!await _testCaseService.IsDeletableAsync(id))
        {
            ViewData[ErrorMessages.ViewDataKey] = ErrorMessages.TestCaseNotDeletable;

            return View((TestCaseViewModel)testCase);
        }

        await _testCaseService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}

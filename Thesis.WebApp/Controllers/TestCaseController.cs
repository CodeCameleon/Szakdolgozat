using Microsoft.AspNetCore.Mvc;
using Shared.Constants;
using TestResults.Entities;
using TestResults.Services.Interfaces;
using Thesis.WebApp.Constants;
using Thesis.WebApp.Services.Interfaces;
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
    /// A teszteseteket lértehozó eszközt tároló adattag.
    /// </summary>
    private readonly ITestInputGenerator _testInputGenerator;

    /// <summary>
    /// A vezérlő konstruktora.
    /// </summary>
    /// <param name="testCaseService">A teszteseteket kezelő szolgáltatás példánya.</param>
    /// <param name="testInputGenerator">A teszteseteket lértehozó eszköz példánya.</param>
    public TestCaseController(ITestCaseService testCaseService, ITestInputGenerator testInputGenerator)
    {
        _testCaseService = testCaseService;
        _testInputGenerator = testInputGenerator;
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
        return View(new TestCaseCreateViewModel());
    }

    /// <summary>
    /// Létrehoz egy új tesztesetet.
    /// </summary>
    /// <param name="viewModel">A tesztesetet létrehozó nézetmodell.</param>
    /// <returns>A tesztesetek főoldalának nézete ha sikeres, különben a létrehozó oldal nézete.</returns>
    [HttpPost]
    public async Task<IActionResult> Create([Bind("Enabled,Input,Size,Unit,Charsets")] TestCaseCreateViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        if (string.IsNullOrEmpty(viewModel.Input))
        {
            viewModel.Input = _testInputGenerator.GenerateInput(
                viewModel.Size!.Value,
                viewModel.Unit!.Value,
                viewModel.Charsets!
            );

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
    /// Módosítja, hogy a teszteset engedélyezve van-e.
    /// </summary>
    /// <param name="id">A módosítani kívánt teszteset azonosítója.</param>
    /// <returns>A tesztesetek főoldalának nézete ha sikeres, különben hibaüzenet.</returns>
    [HttpPost]
    public async Task<IActionResult> Edit(Guid id)
    {
        TestCase? testCase = await _testCaseService.GetAsync(id);

        if (testCase == null)
        {
            return NotFound();
        }

        await _testCaseService.UpdateEnabledAsync(id, !testCase.Enabled);

        return RedirectToAction(nameof(Index));
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
            ViewData[ViewDataKeys.ErrorMessages] = ErrorMessages.TestCaseNotDeletable;

            return View((TestCaseViewModel)testCase);
        }

        await _testCaseService.DeleteAsync(id);

        return RedirectToAction(nameof(Index));
    }
}

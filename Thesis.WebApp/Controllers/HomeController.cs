using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
    /// A vezérlő konstruktora.
    /// </summary>
    /// <param name="logger">A naplózó példánya.</param>
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// A kezdőoldal megjelenítése.
    /// </summary>
    /// <returns>A kezdőoldal nézete.</returns>
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// A hibaoldal megjelenítése.
    /// </summary>
    /// <returns>A hibaoldal nézete.</returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}

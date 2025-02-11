using Thesis.WebApp.ViewModels;

namespace Thesis.WebApp.Services.Interfaces;

/// <summary>
/// Az NUnit teszteket futtató szolgáltatást ábrázoló interfész.
/// </summary>
public interface ITestRunnerService
    : IDisposable
{
    /// <summary>
    /// Lefutatja az algoritmusok memóriahasználatának tesztjeit.
    /// </summary>
    /// <returns>A tesztek eredménye nézetmodell formátumban.</returns>
    TestSummaryViewModel RunAlgorithmMemoryUsageTests();

    /// <summary>
    /// Lefutatja az algoritmusok futási idejének tesztjeit.
    /// </summary>
    /// <returns>A tesztek eredménye nézetmodell formátumban.</returns>
    TestSummaryViewModel RunAlgorithmRunTimeTests();

    /// <summary>
    /// Lefutatja az algoritmusok tesztjeit.
    /// </summary>
    /// <returns>A tesztek eredménye nézetmodell formátumban.</returns>
    TestSummaryViewModel RunAlgorithmTests();
}

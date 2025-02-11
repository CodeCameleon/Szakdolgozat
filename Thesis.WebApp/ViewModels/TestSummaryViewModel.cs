using System.ComponentModel;
using Thesis.WebApp.Constants;

namespace Thesis.WebApp.ViewModels;

/// <summary>
/// Egy lefuttatott tesztcsoport eredményét ábrázoló nézetmodell.
/// </summary>
public class TestSummaryViewModel
{
    /// <summary>
    /// Az összes teszt száma.
    /// </summary>
    [DisplayName(DisplayNames.TestSummaryViewModel.TotalTests)]
    public int TotalTests { get; set; }

    /// <summary>
    /// A sikeres tesztek száma.
    /// </summary>
    [DisplayName(DisplayNames.TestSummaryViewModel.PassedTests)]
    public int PassedTests { get; set; }

    /// <summary>
    /// A sikertelen tesztek száma.
    /// </summary>
    [DisplayName(DisplayNames.TestSummaryViewModel.FailedTests)]
    public int FailedTests { get; set; }
}

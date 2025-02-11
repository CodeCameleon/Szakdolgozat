using System.ComponentModel;

namespace Thesis.WebApp.ViewModels;

/// <summary>
/// Egy lefuttatott tesztcsoport eredményét ábrázoló nézetmodell.
/// </summary>
public class TestSummaryViewModel
{
    /// <summary>
    /// Az összes teszt száma.
    /// </summary>
    [DisplayName("Összes teszt")]
    public int TotalTests { get; set; }

    /// <summary>
    /// A sikeres tesztek száma.
    /// </summary>
    [DisplayName("Sikeres tesztek")]
    public int PassedTests { get; set; }

    /// <summary>
    /// A sikertelen tesztek száma.
    /// </summary>
    [DisplayName("Sikertelen tesztek")]
    public int FailedTests { get; set; }
}

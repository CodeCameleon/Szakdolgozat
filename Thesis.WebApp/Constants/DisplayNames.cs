namespace Thesis.WebApp.Constants;

/// <summary>
/// A megjelenítendő neveket tartalmazó statikus osztály.
/// </summary>
public static class DisplayNames
{
    /// <summary>
    /// A tesztesetet ábrázoló nézetmodell megjelenítendő neveit tartalmazó statikus belső osztály.
    /// </summary>
    public static class TestCaseViewModel
    {
        /// <summary>
        /// A tesztesetek karakterkészleteinek megjelenítendő neve.
        /// </summary>
        public const string Charsets = "Karakterkészletek";

        /// <summary>
        /// A teszteset engedélyezetségének megjelenítendő neve.
        /// </summary>
        public const string Enabled = "Engedélyezve";

        /// <summary>
        /// A teszteset bemenetének megjelenítendő neve.
        /// </summary>
        public const string Input = "Bemenet";

        /// <summary>
        /// A bemenet méretének megjelenítendő neve.
        /// </summary>
        public const string Size = "Méret";

        /// <summary>
        /// A méret mértékegységének megjelenítendő neve.
        /// </summary>
        public const string Unit = "Mértékegység";
    }

    /// <summary>
    /// A lefuttatott tesztcsoport eredményét ábrázoló nézetmodell megjelenítendő neveit tartalmazó statikus belső osztály.
    /// </summary>
    public static class TestSummaryViewModel
    {
        /// <summary>
        /// A sikertelen tesztek számának megjelenítendő neve.
        /// </summary>
        public const string FailedTests = "Sikertelen tesztek";

        /// <summary>
        /// A sikeres tesztek számának megjelenítendő neve.
        /// </summary>
        public const string PassedTests = "Sikeres tesztek";

        /// <summary>
        /// Az összes teszt számának megjelenítendő neve.
        /// </summary>
        public const string TotalTests = "Összes teszt";
    }
}

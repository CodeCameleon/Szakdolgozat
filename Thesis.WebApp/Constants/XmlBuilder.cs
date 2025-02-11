namespace Thesis.WebApp.Constants;

/// <summary>
/// Az XML építő statikus osztály.
/// </summary>
public static class XmlBuilder
{
    /// <summary>
    /// Az XML attribútumokat tartalmazó statikus belső osztály.
    /// </summary>
    public static class Attributes
    {
        /// <summary>
        /// A total attribútum.
        /// </summary>
        public const string Total = "total";

        /// <summary>
        /// A passed attribútum.
        /// </summary>
        public const string Passed = "passed";

        /// <summary>
        /// A failed attribútum.
        /// </summary>
        public const string Failed = "failed";
    }

    /// <summary>
    /// Az XML címkéket tartalmazó statikus belső osztály.
    /// </summary>
    private static class Tags
    {
        /// <summary>
        /// A class címke.
        /// </summary>
        public const string Class = "class";

        /// <summary>
        /// A filter címke.
        /// </summary>
        public const string Filter = "filter";
    }

    /// <summary>
    /// Elkészíti a class címkével ellátott szöveget.
    /// </summary>
    /// <param name="text">A szöveg.</param>
    /// <returns>Az XML címkével ellátott szöveg.</returns>
    public static string Class(string text)
    {
        return Parse(Tags.Class, text);
    }

    /// <summary>
    /// Elkészíti a filter címkével ellátott szöveget.
    /// </summary>
    /// <param name="text">A szöveg.</param>
    /// <returns>Az XML címkével ellátott szöveg.</returns>
    public static string Filter(string text)
    {
        return Parse(Tags.Filter, text);
    }

    /// <summary>
    /// Elkészíti az XML címkével ellátott szöveget.
    /// </summary>
    /// <param name="tag">Az XML címke.</param>
    /// <param name="text">A szöveg.</param>
    /// <returns>Az XML címkével ellátott szöveg.</returns>
    private static string Parse(string tag, string text)
    {
        return $"<{tag}>{text}</{tag}>";
    }
}

namespace AlgorithmTest.Helpers;

/// <summary>
/// A szövegműveleteket segítő statikus osztály.
/// </summary>
internal static class StringHelper
{
    /// <summary>
    /// A titkosításhoz szükséges időt kiíró üzenet.
    /// </summary>
    /// <param name="timeSpan">A futási idő.</param>
    /// <returns>Az üzenet.</returns>
    public static string TimeToEncrypt(TimeSpan timeSpan)
    {
        return $"A titkosítás lefutott {timeSpan} ms alatt.";
    }

    /// <summary>
    /// A visszafejtéshez szükséges időt kiíró üzenet.
    /// </summary>
    /// <param name="timeSpan">A futási idő.</param>
    /// <returns>Az üzenet.</returns>
    public static string TimeToDecrypt(TimeSpan timeSpan)
    {
        return $"A visszafejtés lefutott {timeSpan} ms alatt.";
    }

    /// <summary>
    /// A titkosítás során felhasznált memóriát kiíró üzenet.
    /// </summary>
    /// <param name="usage">A felhasznált memória mennyisége bájtban.</param>
    /// <returns>Az üzenet.</returns>
    public static string MemoryUsageWhileEncrypt(long usage)
    {
        return $"A titkosítás {usage} bájt memóriát használt.";
    }

    /// <summary>
    /// A visszafejtés során felhasznált memóriát kiíró üzenet.
    /// </summary>
    /// <param name="usage">A felhasznált memória mennyisége bájtban.</param>
    /// <returns>Az üzenet.</returns>
    public static string MemoryUsageWhileDecrypt(long usage)
    {
        return $"A visszafejtés {usage} bájt memóriát használt.";
    }
}

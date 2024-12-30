namespace AlgorithmTest.Helpers;

/// <summary>
/// A szövegműveleteket segítő statikus osztály.
/// </summary>
internal static class StringHelper
{
    /// <summary>
    /// A log fájlok mappájának elérési útvonala.
    /// </summary>
    private const string _logDirectory = "../../../../Logs";

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

    /// <summary>
    /// A memóriahasználat log fájl elérési útvonala.
    /// </summary>
    /// <param name="algorithmName">A tesztelt algoritmus neve.</param>
    /// <returns>A log fájl elérési útvonala.</returns>
    public static string MemoryUsageLogPath(string algorithmName)
    {
        return Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            _logDirectory,
            $"{algorithmName}_MemoryUsageLog.csv"
        );
    }

    /// <summary>
    /// A futási idő log fájl elérési útvonala.
    /// </summary>
    /// <param name="algorithmName">A tesztelt algoritmus neve.</param>
    /// <returns>A log fájl elérési útvonala.</returns>
    public static string RunTimeLogPath(string algorithmName)
    {
        return Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            _logDirectory,
            $"{algorithmName}_RunTimeLog.csv"
        );
    }

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
}

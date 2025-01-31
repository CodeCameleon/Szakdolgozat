namespace TestResults.Services.Interfaces;

/// <summary>
/// A teszteseteket kezelő szolgáltatást ábrázoló interfész.
/// </summary>
public interface ITestCaseService
    : IDisposable
{
    /// <summary>
    /// Létrehoz egy új tesztesetet.
    /// </summary>
    /// <param name="input">A létrehozni kívánt teszteset bemenete.</param>
    /// <returns>Igaz ha a teszteset létre lett hozva, hamis ha már létezet.</returns>
    Task<bool> CreateAsync(string input);

    /// <summary>
    /// Lekéri az összes engedélyezett teszteset bemenetét.
    /// </summary>
    /// <returns>Az engedélyezett tesztesetek bemeneteinek listája.</returns>
    Task<List<string>> GetEnabledInputListAsync();
}

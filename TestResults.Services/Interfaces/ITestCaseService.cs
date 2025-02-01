using TestResults.Entities;

namespace TestResults.Services.Interfaces;

/// <summary>
/// A teszteseteket kezelő szolgáltatást ábrázoló interfész.
/// </summary>
public interface ITestCaseService
{
    /// <summary>
    /// Létrehoz egy új tesztesetet.
    /// </summary>
    /// <param name="testCase">A létrehozni kívánt teszteset.</param>
    Task CreateAsync(TestCase testCase);

    /// <summary>
    /// Leellenőrzi, hogy létezik-e már az adott bemenetel teszteset.
    /// </summary>
    /// <param name="input">A kérdéses teszteset bemenete.</param>
    /// <returns>Igaz ha létezik, különben hamis.</returns>
    Task<bool> ExistsAsync(string input);

    /// <summary>
    /// Leellenőrzi, hogy az adott azonosítóval rendelkező teszteset törölhető-e.
    /// </summary>
    /// <param name="id">A kérdéses teszteset azonosítója.</param>
    /// <returns>Igaz ha törölhető, különben hamis.</returns>
    Task<bool> IsDeletableAsync(Guid id);

    /// <summary>
    /// Lekéri az azonosítóhoz tartozó tesztesetet.
    /// </summary>
    /// <param name="id">A keresett teszteset azonosítója.</param>
    /// <returns>A teszteset ha létezik, különben null.</returns>
    Task<TestCase?> GetAsync(Guid id);

    /// <summary>
    /// Lekéri az összes engedélyezett teszteset bemenetét.
    /// </summary>
    /// <returns>Az engedélyezett tesztesetek bemeneteinek listája.</returns>
    Task<List<string>> GetEnabledInputListAsync();

    /// <summary>
    /// Lekéri az összes tesztesetet.
    /// </summary>
    /// <returns>A tesztesetek listája.</returns>
    Task<List<TestCase>> GetListAsync();

    /// <summary>
    /// Törli az azonosítóhoz tartozó tesztesetet.
    /// </summary>
    /// <param name="id">A törölni kívánt teszteset azonosítója.</param>
    Task DeleteAsync(Guid id);
}

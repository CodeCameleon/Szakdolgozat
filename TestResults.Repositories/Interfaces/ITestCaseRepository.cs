using TestResults.Entities;

namespace TestResults.Repositories.Interfaces;

/// <summary>
/// A teszteseteket kezelő adattárat ábrázoló interfész.
/// </summary>
public interface ITestCaseRepository
{
    /// <summary>
    /// Létrehoz egy új tesztesetet az adatbázisban.
    /// </summary>
    /// <param name="testCase">A létrehozni kívánt teszteset.</param>
    Task CreateAsync(TestCase testCase);

    /// <summary>
    /// Leellenőrzi, hogy létezik-e már az adott bemenetel teszteset az adatbázisban.
    /// </summary>
    /// <param name="input">A kérdéses teszteset bemenete.</param>
    /// <returns>Igaz ha nem létezik, különben hamis.</returns>
    Task<bool> NotExistsAsync(string input);

    /// <summary>
    /// Lekéri a bemenethez tartozó tesztesetet az adatbázisból.
    /// </summary>
    /// <param name="input">A keresett teszteset bemenete.</param>
    /// <returns>A teszteset ha létezik, különben hiba.</returns>
    Task<TestCase> GetAsync(string input);

    /// <summary>
    /// Lekéri az összes engedélyezett teszteset bemenetét az adatbázisból.
    /// </summary>
    /// <returns>Az engedélyezett tesztesetek bemeneteinek listája.</returns>
    Task<List<string>> GetEnabledInputListAsync();
}

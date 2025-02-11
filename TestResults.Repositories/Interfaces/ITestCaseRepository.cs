using TestResults.Dtos;
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
    /// Leellenőrzi, hogy létezik-e már az adott bemenetel és mérettel rendelkező teszteset az adatbázisban.
    /// </summary>
    /// <param name="input">A kérdéses teszteset bemenete.</param>
    /// <param name="size">A kérdéses teszteset mérete bájtban.</param>
    /// <returns>Igaz ha létezik, különben hamis.</returns>
    Task<bool> ExistsAsync(string input, int size);

    /// <summary>
    /// Leellenőrzi, hogy az adott azonosítóval rendelkező teszteset törölhető-e az adatbázisból.
    /// </summary>
    /// <param name="id">A kérdéses teszteset azonosítója.</param>
    /// <returns>Igaz ha törölhető, különben hamis.</returns>
    Task<bool> IsDeletableAsync(Guid id);

    /// <summary>
    /// Lekéri az azonosítóhoz tartozó tesztesetet az adatbázisból.
    /// </summary>
    /// <param name="id">A keresett teszteset azonosítója.</param>
    /// <returns>A teszteset ha létezik, különben null.</returns>
    Task<TestCase?> GetAsync(Guid id);

    /// <summary>
    /// Lekéri a bemenethez és mérethez tartozó tesztesetet az adatbázisból.
    /// </summary>
    /// <param name="input">A keresett teszteset bemenete.</param>
    /// <param name="size">A keresett teszteset mérete bájtban.</param>
    /// <returns>A teszteset ha létezik, különben hiba.</returns>
    Task<TestCase> GetAsync(string input, int size);

    /// <summary>
    /// Lekéri a tesztesethez tartozó teszteredmények számát az adatbázisból.
    /// </summary>
    /// <param name="id">A keresett teszteset azonosítója.</param>
    /// <returns>A teszteredmények száma.</returns>
    Task<int> GetTestResultsCountAsync(Guid id);

    /// <summary>
    /// Lekéri az összes engedélyezett teszteset adatátmeneti objektumként az adatbázisból.
    /// </summary>
    /// <returns>Az engedélyezett tesztesetek adatátmeneti objektumainak listája.</returns>
    Task<List<TestCaseDto>> GetEnabledDtoListAsync();

    /// <summary>
    /// Lekéri az összes tesztesetet az adatbázisból.
    /// </summary>
    /// <returns>A tesztesetek listája.</returns>
    Task<List<TestCase>> GetListAsync();

    /// <summary>
    /// Módosítja az azonosítóhoz tartozó teszteset engedélyezettségét az adatbázisban.
    /// </summary>
    /// <param name="id">A módosítani kívánt teszteset azonosítója.</param>
    /// <param name="enabled">Az új engedélyezettségi állapot.</param>
    Task UpdateEnabledAsync(Guid id, bool enabled);

    /// <summary>
    /// Törli az azonosítóhoz tartozó tesztesetet az adatbázisból.
    /// </summary>
    /// <param name="id">A törölni kívánt teszteset azonosítója.</param>
    Task DeleteAsync(Guid id);
}

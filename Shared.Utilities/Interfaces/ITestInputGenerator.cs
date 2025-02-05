using Shared.Enums;

namespace Shared.Utilities.Interfaces;

/// <summary>
/// A teszteseteket lértehozó eszközt ábrázoló interfész.
/// </summary>
public interface ITestInputGenerator
{
    /// <summary>
    /// Létrehozza a bemenetet a részleges bemenet és a méret alapján.
    /// </summary>
    /// <param name="partialInput">A részleges bemenet.</param>
    /// <param name="size">A méret.</param>
    /// <returns>A kész bemenet.</returns>
    string CreateInput(string partialInput, int size);

    /// <summary>
    /// Generál egy karakterláncot a megadott mérettel a karakterekből.
    /// </summary>
    /// <param name="size">A karakterlánc mérete.</param>
    /// <param name="unit">A méret mértékegysége.</param>
    /// <param name="charsets">A használható karakterkészletek.</param>
    /// <returns>A generált karakterlánc.</returns>
    string GenerateString(int size, ESizeUnit unit, IEnumerable<ECharset> charsets);
}

using Shared.Enums;

namespace Thesis.WebApp.Services.Interfaces;

/// <summary>
/// A teszteseteket lértehozó eszközt ábrázoló interfész.
/// </summary>
public interface ITestInputGenerator
{
    /// <summary>
    /// Generál egy karakterláncot a megadott mérettel a karakterekből.
    /// </summary>
    /// <param name="size">A karakterlánc mérete.</param>
    /// <param name="unit">A méret mértékegysége.</param>
    /// <param name="charsets">A használható karakterkészletek.</param>
    /// <returns>A generált karakterlánc.</returns>
    string GenerateInput(int size, ESizeUnit unit, IEnumerable<ECharset> charsets);
}

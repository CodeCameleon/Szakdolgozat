using Shared.Enums;

namespace Shared.Utilities.Interfaces;

/// <summary>
/// A teszteseteket lértehozó eszközt ábrázoló interfész.
/// </summary>
public interface ITestInputGenerator
{
    /// <summary>
    /// Létrehoz egy karakterláncot a megadott mérettel a karakterekből.
    /// </summary>
    /// <param name="size">A karakterlánc mérete.</param>
    /// <param name="unit">A méret mértékegysége.</param>
    /// <param name="charsets">A használható karakterkészletek.</param>
    /// <returns></returns>
    string GenerateString(int size, ESizeUnit unit, params ECharset[] charsets);
}

namespace MathCrypt.Helpers;

/// <summary>
/// A szövegműveleteket segítő statikus osztály.
/// </summary>
internal static class StringHelper
{
    /// <summary>
    /// A hibaüzeneteket tartalmazó belső osztály.
    /// </summary>
    public static class Error
    {
        /// <summary>
        /// A hiányzó karakter esetén dobandó hibaüzenet.
        /// </summary>
        /// <param name="character">A nemtalált karakter.</param>
        /// <returns>A hibaüzenet.</returns>
        public static string MissingCharacter(char character)
        {
            return $"A '{character}' karakter nem található.";
        }
    }

    /// <summary>
    /// A lépés normalizálásánál használt formula.
    /// </summary>
    /// <param name="number">Hány számjegyre kell normalizálni.</param>
    /// <returns>A normalizálási formukla.</returns>
    public static string FormatMove(int number)
    {
        return "{0:D" + number + "}";
    }
}

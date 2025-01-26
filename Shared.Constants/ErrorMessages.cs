namespace Shared.Constants;

/// <summary>
/// A hibaüzeneteket tartalmazó statikus osztály.
/// </summary>
public static class ErrorMessages
{
    /// <summary>
    /// Hiányzó karakter esetén dobandó hibaüzenet.
    /// </summary>
    /// <param name="character">A nemtalált karakter.</param>
    /// <returns>A hibaüzenet.</returns>
    public static string CharacterNotFound(char character) => $"A '{character}' karakter nem található.";

    /// <summary>
    /// Nem található alapértelmezett kapcsolódási karakterlánc esetén dobandó hibaüzenet.
    /// </summary>
    public static string DefaultConnectionNotFound => "Az alapértelmezett kapcsolódási karakterlánc nem található.";

    /// <summary>
    /// Túl kicsi kulcsméret esetén dobandó hibaüzenet.
    /// </summary>
    public static string KeyDimensionsTooSmall => "A kulcs mérete túl kicsi a megadott karakterek számához képest.";

    /// <summary>
    /// Nem található gyökérkönyvtár esetén dobandó hibaüzenet.
    /// </summary>
    public static string SolutionPathNotFound => "Az alkalmazás gyökérkönyvtára nem található.";

    /// <summary>
    /// Inicializálatlan tranzakció esetén dobandó hibaüzenet.
    /// </summary>
    public static string TransactionNotStarted => "Nincs elindított tranzakció.";

    /// <summary>
    /// Definiálatlan karakterkészlet esetén dobandó hibaüzenet.
    /// </summary>
    public static string UndefinedCharsetCharacters => "A karakterkészlet karakterei nincsenek definiálva.";
}

namespace Shared.Constants;

/// <summary>
/// A hibaüzeneteket tartalmazó statikus osztály.
/// </summary>
public static class ErrorMessages
{
    /// <summary>
    /// Nem található alapértelmezett kapcsolódási karakterlánc esetén dobanó hibaüzenet.
    /// </summary>
    public static string DefaultConnectionNotFound => "Az alapértelmezett kapcsolódási karakterlánc nem található.";

    /// <summary>
    /// Nem található gyökérkönyvtár esetén dobanó hibaüzenet.
    /// </summary>
    public static string SolutionPathNotFound => "Az alkalmazás gyökérkönyvtára nem található.";

    /// <summary>
    /// Inicializálatlan tranzakció esetén dobanó hibaüzenet.
    /// </summary>
    public static string TransactionNotStarted => "Nincs elindított tranzakció.";

    /// <summary>
    /// Definiálatlan karakterkészlet esetén dobanó hibaüzenet.
    /// </summary>
    public static string UndefinedCharsetCharacters => "A karakterkészlet karakterei nincsenek definiálva.";
}

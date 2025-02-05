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
    /// Karakterkészletek hiánya esetén dobandó hibaüzenet.
    /// </summary>
    public static string CharsetsRequired => "A karakterkészletek megadása kötelező, ha a bemenet üres.";

    /// <summary>
    /// Nem található alapértelmezett kapcsolódási karakterlánc esetén dobandó hibaüzenet.
    /// </summary>
    public static string DefaultConnectionNotFound => "Az alapértelmezett kapcsolódási karakterlánc nem található.";

    /// <summary>
    /// Hiányzó bemenet vagy generáláshoz szükséges adatok esetén dobandó hibaüzenet.
    /// </summary>
    public static string InputOrDetailsRequired => "A bemenet vagy a generálásához szükséges adatok megadása kötelező.";

    /// <summary>
    /// Túl nagy bemenet esetén dobandó hibaüzenet.
    /// </summary>
    public static string InputToBig => "A bemenet mérete túl nagy.";

    /// <summary>
    /// Túl kicsi kulcsméret esetén dobandó hibaüzenet.
    /// </summary>
    public static string KeyDimensionsTooSmall => "A kulcs mérete túl kicsi a megadott karakterek számához képest.";

    /// <summary>
    /// Nem megfelelő értékű méret esetén dobandó hibaüzenet.
    /// </summary>
    /// <param name="min">A méret minimuma.</param>
    /// <param name="max">A méret maximuma.</param>
    /// <returns>A hibaüzenet.</returns>
    public static string SizeIsOutOfRange(int min, int max) => $"A méretnek {min} és {max} között kell lennie.";

    /// <summary>
    /// Méret hiánya esetén dobandó hibaüzenet.
    /// </summary>
    public static string SizeRequired => "A méret megadása kötelező, ha a bemenet üres.";

    /// <summary>
    /// Nem található gyökérkönyvtár esetén dobandó hibaüzenet.
    /// </summary>
    public static string SolutionPathNotFound => "Az alkalmazás gyökérkönyvtára nem található.";

    /// <summary>
    /// Ha a teszteset bemenete nagyobb, mint a mérete esetén dobandó hibaüzenet.
    /// </summary>
    public static string TestCaseInputCantBeBiggerThenSize => "A teszteset bemenete nem lehet nagyobb, mint a méret.";

    /// <summary>
    /// Teszteset bemenetének üressége esetén dobandó hibaüzenet.
    /// </summary>
    public static string TestCaseInputEmpty => "A teszteset bemenete üres.";

    /// <summary>
    /// Teszteset bemenetének létezése esetén dobandó hibaüzenet.
    /// </summary>
    public static string TestCaseInputExists => "A teszteset bemenete már létezik.";

    /// <summary>
    /// Nem található a teszteset generált bemenetének maximális mérete esetén dobandó hibaüzenet.
    /// </summary>
    public static string TestCaseInputChunkSizeInBytesNotFound
        => "A teszteset generált bemenetének maximális méretét bájtban nem található.";

    /// <summary>
    /// Nem található a teszteset bemenetének maximális mérete esetén dobandó hibaüzenet.
    /// </summary>
    public static string TestCaseInputMaxSizeInBytesNotFound
        => "A teszteset bemenetének maximális méretét bájtban nem található.";

    /// <summary>
    /// Nem törölhető teszteset esetén dobandó hibaüzenet.
    /// </summary>
    public static string TestCaseNotDeletable => "A teszteset nem törölhető, mert van rá hivatkozó teszt eredmény.";

    /// <summary>
    /// Nem engedélyezett teszteset esetén dobandó hibaüzenet.
    /// </summary>
    public static string TestCaseNotEnabled => "A teszteset nincs engedélyezve.";

    /// <summary>
    /// Inicializálatlan tranzakció esetén dobandó hibaüzenet.
    /// </summary>
    public static string TransactionNotStarted => "Nincs elindított tranzakció.";

    /// <summary>
    /// Definiálatlan karakterkészlet esetén dobandó hibaüzenet.
    /// </summary>
    public static string UndefinedCharsetCharacters => "A karakterkészlet karakterei nincsenek definiálva.";

    /// <summary>
    /// Mértékegység hiánya esetén dobandó hibaüzenet.
    /// </summary>
    public static string UnitRequired => "A mértékegység megadása kötelező, ha a bemenet üres.";

    /// <summary>
    /// A hibaüzeneteket tartalmazó nézetinformáció kulcsa.
    /// </summary>
    public static string ViewDataKey => "ErrorMessages";
}

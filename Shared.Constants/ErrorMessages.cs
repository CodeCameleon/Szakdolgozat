namespace Shared.Constants;

/// <summary>
/// A hibaüzeneteket tartalmazó statikus osztály.
/// </summary>
public static class ErrorMessages
{
    /// <summary>
    /// A nem található hibaüzeneteket tartalmazó statikus belső osztály.
    /// </summary>
    public static class NotFound
    {
        /// <summary>
        /// Nem található az algoritmus teszteket tároló fájl elérési útvonala esetén dobandó hibaüzenet.
        /// </summary>
        public const string AlgorithmTestsFilePath = "Az algoritmus teszteket tároló fájl elérési útvonala nem található.";

        /// <summary>
        /// Nem található alapértelmezett kapcsolódási karakterlánc esetén dobandó hibaüzenet.
        /// </summary>
        public const string DefaultConnection = "Az alapértelmezett kapcsolódási karakterlánc nem található.";

        /// <summary>
        /// Nem található gyökérkönyvtár esetén dobandó hibaüzenet.
        /// </summary>
        public const string SolutionPath = "Az alkalmazás gyökérkönyvtára nem található.";

        /// <summary>
        /// Nem található a teszteset generált bemenetének maximális mérete bájtban esetén dobandó hibaüzenet.
        /// </summary>
        public const string TestCaseInputChunkSizeInBytes = "A teszteset generált bemenetének maximális mérete bájtban nem található.";

        /// <summary>
        /// Nem található a teszteset bemenetének maximális mérete bájtban esetén dobandó hibaüzenet.
        /// </summary>
        public const string TestCaseInputMaxSizeInBytes = "A teszteset bemenetének maximális mérete bájtban nem található.";

        /// <summary>
        /// Nem található XML attribútum esetén dobandó hibaüzenet.
        /// </summary>
        public const string XmlAttribute = "Az XML attribútum nem található.";

        /// <summary>
        /// Nem található karakter esetén dobandó hibaüzenet.
        /// </summary>
        /// <param name="character">A nemtalált karakter.</param>
        /// <returns>A hibaüzenet.</returns>
        public static string Character(char character) => $"A '{character}' karakter nem található.";
    }

    /// <summary>
    /// A hiányzó hibaüzeneteket tartalmazó statikus belső osztály.
    /// </summary>
    public static class Required
    {
        /// <summary>
        /// Hiányzó karakterkészletek esetén dobandó hibaüzenet.
        /// </summary>
        public const string Charsets = "A karakterkészletek megadása kötelező, ha a bemenet üres.";

        /// <summary>
        /// Hiányzó bemenet vagy generáláshoz szükséges adatok esetén dobandó hibaüzenet.
        /// </summary>
        public const string InputOrDetails = "A bemenet vagy a generálásához szükséges adatok megadása kötelező.";

        /// <summary>
        /// Hiányzó méret esetén dobandó hibaüzenet.
        /// </summary>
        public const string Size = "A méret megadása kötelező, ha a bemenet üres.";

        /// <summary>
        /// Hiányzó mértékegység esetén dobandó hibaüzenet.
        /// </summary>
        public const string Unit = "A mértékegység megadása kötelező, ha a bemenet üres.";
    }

    /// <summary>
    /// Túl nagy bemenet esetén dobandó hibaüzenet.
    /// </summary>
    public const string InputTooBig = "A bemenet mérete túl nagy.";

    /// <summary>
    /// Túl kicsi kulcsméret esetén dobandó hibaüzenet.
    /// </summary>
    public const string KeyDimensionsTooSmall = "A kulcs mérete túl kicsi a megadott karakterek számához képest.";

    /// <summary>
    /// Nagyobb teszteset bemenet, mint méret esetén dobandó hibaüzenet.
    /// </summary>
    public const string TestCaseInputCantBeBiggerThenSize = "A teszteset bemenete nem lehet nagyobb, mint a méret.";

    /// <summary>
    /// Üres teszteset bemenet esetén dobandó hibaüzenet.
    /// </summary>
    public const string TestCaseInputEmpty = "A teszteset bemenete üres.";

    /// <summary>
    /// Létező teszteset bemenet esetén dobandó hibaüzenet.
    /// </summary>
    public const string TestCaseInputExists = "A teszteset bemenete már létezik.";

    /// <summary>
    /// Nem törölhető teszteset esetén dobandó hibaüzenet.
    /// </summary>
    public const string TestCaseNotDeletable = "A teszteset nem törölhető, mert van rá hivatkozó teszteredmény.";

    /// <summary>
    /// Nem engedélyezett teszteset esetén dobandó hibaüzenet.
    /// </summary>
    public const string TestCaseNotEnabled = "A teszteset nincs engedélyezve.";

    /// <summary>
    /// Inicializálatlan tranzakció esetén dobandó hibaüzenet.
    /// </summary>
    public const string TransactionNotStarted = "Nincs elindított tranzakció.";

    /// <summary>
    /// Definiálatlan karakterkészlet esetén dobandó hibaüzenet.
    /// </summary>
    public const string UndefinedCharsetCharacters = "A karakterkészlet karakterei nincsenek definiálva.";

    /// <summary>
    /// Nem megfelelő értékű méret esetén dobandó hibaüzenet.
    /// </summary>
    /// <param name="min">A méret minimuma.</param>
    /// <param name="max">A méret maximuma.</param>
    /// <returns>A hibaüzenet.</returns>
    public static string SizeIsOutOfRange(int min, int max) => $"A méretnek {min} és {max} között kell lennie.";
}

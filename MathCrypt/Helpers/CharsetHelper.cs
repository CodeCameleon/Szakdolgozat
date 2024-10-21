using MathCrypt.Enums;

namespace MathCrypt.Helpers;

/// <summary>
/// A karakterkészleteket segítő statikus osztály.
/// </summary>
internal static class CharsetHelper
{
    /// <summary>
    /// A karakterkészleteket tartalmazó szótár.
    /// </summary>
    private static readonly Dictionary<ECharset, string> _charsetData = new()
    {
        { ECharset.Space, " " },
        { ECharset.Numbers, "0123456789" },
        { ECharset.MathSymbols, "+-*/=<>(){}[]^%" },
        { ECharset.Punctuations, "!\"#$&',.:;?@\\_`|~" },
        { ECharset.Special, "\r\n\t\b\f" },
        { ECharset.EN, "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ" },
        { ECharset.HU, "áéíóöőúüűÁÉÍÓÖŐÚÜŰ" }
    };

    /// <summary>
    /// Az alapértelmezett karakterkészletet tartalmazó halmaz.
    /// </summary>
    private static readonly HashSet<ECharset> _default =
    [
        ECharset.Space,
        ECharset.Numbers,
        ECharset.EN,
    ];

    /// <summary>
    /// Lekéri a karakterkészlethez tartozó karaktereket.
    /// </summary>
    /// <param name="charsets">A keresett karakterkészletek.</param>
    /// <returns>A karakterkészlet karakter tömb formájában.</returns>
    public static char[] GetCharacters(params ECharset[] charsets)
    {
        return ECharsetsToChars(charsets);
    }

    /// <summary>
    /// Lekéri az alapértelmezett karakterkészletet.
    /// </summary>
    /// <returns>Az alapértelmezett karakterkészlet karakter tömb formájában.</returns>
    public static char[] GetDefaultCharsets()
    {
        return ECharsetsToChars(_default.ToArray());
    }

    /// <summary>
    /// Visszaadja a karakterkészlethez tartozó karaktereket.
    /// </summary>
    /// <param name="charsets">A keresett karakterkészletek.</param>
    /// <returns>A karakterkészlet karakter tömb formájában.</returns>
    private static char[] ECharsetsToChars(ECharset[] charsets)
    {
        List<char> chars = [];

        foreach (ECharset charset in charsets)
        {
            chars.AddRange(_charsetData[charset]);
        }

        return chars.ToArray();
    }
}

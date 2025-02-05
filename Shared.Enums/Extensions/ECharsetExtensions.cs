using Shared.Constants;

namespace Shared.Enums.Extensions;

/// <summary>
/// A karakterkészleteket tartalamzó enum kiterjesztései.
/// </summary>
public static class ECharsetExtensions
{
    /// <summary>
    /// A karakterkészleteket tartalmazó szótár.
    /// </summary>
    private static readonly Dictionary<ECharset, List<char>> _charsetCharacters = new()
    {
        {
            ECharset.ControlCharacters, Enumerable.Range(0, 32).Union([127]).ToCharList()
        },
        {
            ECharset.PunctuationAndSymbols, Enumerable.Range(32, 16).Union(Enumerable.Range(58, 7))
                .Union(Enumerable.Range(91, 6)).Union(Enumerable.Range(123, 4)).ToCharList()
        },
        {
            ECharset.Digits, Enumerable.Range(48, 10).ToCharList()
        },
        {
            ECharset.UppercaseLetters, Enumerable.Range(65, 26).ToCharList()
        },
        {
            ECharset.LowercaseLetters, Enumerable.Range(97, 26).ToCharList()
        }
    };

    /// <summary>
    /// Az alapértelmezett karakterkészletek.
    /// </summary>
    private static readonly HashSet<ECharset> _defaultCharsets =
    [
        ECharset.ControlCharacters,
        ECharset.PunctuationAndSymbols,
        ECharset.Digits,
        ECharset.UppercaseLetters,
        ECharset.LowercaseLetters
    ];

    /// <summary>
    /// Lekéri a karakterkészlethez tartozó karaktereket.
    /// </summary>
    /// <param name="charset">A keresett karakterkészlet.</param>
    /// <returns>A karakterek listája.</returns>
    public static List<char> GetCharacters(this ECharset charset)
    {
        if (charset == ECharset.Default)
        {
            return _defaultCharsets.GetCharacters();
        }
        else if (_charsetCharacters.TryGetValue(charset, out var characters))
        {
            return characters;
        }

        throw new ArgumentOutOfRangeException(nameof(charset), charset, ErrorMessages.UndefinedCharsetCharacters);
    }

    /// <summary>
    /// Lekéri a karakterkészletekhez tartozó karaktereket.
    /// </summary>
    /// <param name="charsets">A keresett karakterkészletek.</param>
    /// <returns>A karakterek listája.</returns>
    public static List<char> GetCharacters(this IEnumerable<ECharset> charsets)
    {
        return charsets.SelectMany(charset => charset.GetCharacters()).ToList();
    }

    /// <summary>
    /// Átalakítja a számok kollekcióját karakterek listájává.
    /// </summary>
    /// <param name="ints">A számok kollekciója.</param>
    /// <returns>A karakterek listája.</returns>
    private static List<char> ToCharList(this IEnumerable<int> ints)
    {
        return ints.Select(i => (char)i).ToList();
    }
}

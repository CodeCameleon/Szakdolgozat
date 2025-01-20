using Shared.Enums;
using Shared.Enums.Extensions;
using Shared.Utilities.Interfaces;
using System.Text;

namespace Shared.Utilities.Implementations;

/// <summary>
/// A teszteseteket lértehozó eszközt megvalósító osztály.
/// </summary>
public class TestInputGenerator
    : ITestInputGenerator
{
    /// <summary>
    /// A véletlenszám generátort tároló adattag.
    /// </summary>
    private readonly Random _random;

    /// <summary>
    /// Az eszköz alapértelmezett konstruktora.
    /// </summary>
    public TestInputGenerator()
    {
        _random = new();
    }

    /// <inheritdoc />
    public string GenerateString(int size, ESizeUnit unit, params ECharset[] charsets)
    {
        StringBuilder stringBuilder = new();

        List<char> characters = charsets.GetCharacters();

        for (int i = 0; i < size * (int)unit; i++)
        {
            stringBuilder.Append(
                characters[_random.Next(0, characters.Count)]
            );
        }

        return stringBuilder.ToString();
    }
}

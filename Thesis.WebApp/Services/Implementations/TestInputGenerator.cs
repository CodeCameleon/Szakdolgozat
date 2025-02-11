using Shared.Constants;
using Shared.Enums;
using Shared.Enums.Extensions;
using System.Text;
using Thesis.WebApp.Services.Interfaces;

namespace Thesis.WebApp.Services.Implementations;

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
    public string GenerateInput(int size, ESizeUnit unit, IEnumerable<ECharset> charsets)
    {
        StringBuilder stringBuilder = new();
        List<char> characters = charsets.GetCharacters();
        Encoding utf8 = Encoding.UTF8;
        int byteSize = 0;

        while (byteSize < size * (int)unit && byteSize < GlobalConfiguration.TestCaseInputChunkSize)
        {
            char randomCharacter = characters[_random.Next(0, characters.Count)];
            stringBuilder.Append(randomCharacter);
            byteSize += utf8.GetByteCount(randomCharacter.ToString());
        }

        return stringBuilder.ToString();
    }
}

using Shared.Constants;
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
    public string CreateInput(string partialInput, int size)
    {
        Encoding encoding = Encoding.UTF8;
        int partialSize = encoding.GetByteCount(partialInput);

        if (partialSize > size)
        {
            throw new ArgumentException(ErrorMessages.TestCaseInputCantBeBiggerThenSize);
        }

        if (partialSize == size)
        {
            return partialInput;
        }

        StringBuilder result = new();
        int currentSize = 0;

        int fullRepeats = size / partialSize;
        result.Append(string.Concat(Enumerable.Repeat(partialInput, fullRepeats)));
        currentSize = fullRepeats * partialSize;

        int remainingBytes = size - currentSize;
        if (remainingBytes == 0)
        {
            return result.ToString();
        }

        int[] charByteSizes = partialInput.Select(c => encoding.GetByteCount([c])).ToArray();
        for (int i = 0; i < partialInput.Length; i++)
        {
            if (remainingBytes < charByteSizes[i])
            {
                break;
            }

            result.Append(partialInput[i]);
            remainingBytes -= charByteSizes[i];
        }

        return result.ToString();
    }

    /// <inheritdoc />
    public string GenerateString(int size, ESizeUnit unit, IEnumerable<ECharset> charsets)
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

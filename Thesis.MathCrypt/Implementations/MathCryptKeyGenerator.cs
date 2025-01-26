using Shared.Constants;
using Shared.Enums;
using Shared.Enums.Extensions;
using Thesis.MathCrypt.Interfaces;
using Thesis.MathCrypt.Models;

namespace Thesis.MathCrypt.Implementations;

/// <summary>
/// A saját titkosító algoritmusom kulcsát lértehozó eszközt megvalósító osztály.
/// </summary>
public class MathCryptKeyGenerator
    : IMathCryptKeyGenerator
{
    /// <summary>
    /// A véletlenszám generátort tároló adattag.
    /// </summary>
    private readonly Random _random;

    /// <summary>
    /// Az eszköz alapértelmezett konstruktora.
    /// </summary>
    public MathCryptKeyGenerator()
    {
        _random = new();
    }

    /// <inheritdoc />
    public char[][] GenerateKey()
    {
        List<char> characterList = ECharset.Default.GetCharacters();

        (int height, int width) = FindSmallestArraySize(characterList.Count);

        return Generate(height, width, 1, characterList);
    }

    /// <inheritdoc />
    public char[][] GenerateKey(int height, int width)
    {
        List<char> characterList = ECharset.Default.GetCharacters();

        if (height * width - characterList.Count < 0)
        {
            throw new ArgumentException(ErrorMessages.KeyDimensionsTooSmall);
        }

        return Generate(height, width, 1, characterList);
    }

    /// <inheritdoc />
    public char[][] GenerateKey(int strength)
    {
        List<char> characterList = ECharset.Default.GetCharacters();

        (int height, int width) = FindSmallestArraySize(characterList.Count * strength);

        return Generate(height, width, strength, characterList);
    }

    /// <inheritdoc />
    public char[][] GenerateKey(params ECharset[] charsets)
    {
        List<char> characterList = charsets.GetCharacters();

        (int height, int width) = FindSmallestArraySize(characterList.Count);

        return Generate(height, width, 1, characterList);
    }

    /// <inheritdoc />
    public char[][] GenerateKey(int height, int width, int strength)
    {
        List<char> characterList = ECharset.Default.GetCharacters();

        if (height * width - characterList.Count * strength < 0)
        {
            throw new ArgumentException(ErrorMessages.KeyDimensionsTooSmall);
        }

        return Generate(height, width, strength, characterList);
    }

    /// <inheritdoc />
    public char[][] GenerateKey(int height, int width, params ECharset[] charsets)
    {
        List<char> characterList = charsets.GetCharacters();

        if (height * width - characterList.Count < 0)
        {
            throw new ArgumentException(ErrorMessages.KeyDimensionsTooSmall);
        }

        return Generate(height, width, 1, characterList);
    }

    /// <inheritdoc />
    public char[][] GenerateKey(int strength, params ECharset[] charsets)
    {
        List<char> characterList = charsets.GetCharacters();

        (int height, int width) = FindSmallestArraySize(characterList.Count * strength);
        
        return Generate(height, width, strength, characterList);
    }

    /// <inheritdoc />
    public char[][] GenerateKey(int height, int width, int strength, params ECharset[] charsets)
    {
        List<char> characterList = charsets.GetCharacters();

        if (height * width - characterList.Count * strength < 0)
        {
            throw new ArgumentException(ErrorMessages.KeyDimensionsTooSmall);
        }

        return Generate(height, width, strength, characterList);
    }

    /// <summary>
    /// Megkeresi a legkisebb kétdimenziós tömb méretet, ami eltudja tárolni az adott számú karaktert.
    /// </summary>
    /// <param name="numberOfCharacters">Az eltárolni kívánt karakterek száma.</param>
    /// <returns>A kétdimenziós tömb magassága és szélessége.</returns>
    private static (int height, int width) FindSmallestArraySize(int numberOfCharacters)
    {
        double height = Math.Floor(Math.Sqrt(numberOfCharacters));
        double width = height + Math.Ceiling((numberOfCharacters - Math.Pow(height, 2)) / height);

        return ((int)height, (int)width);
    }

    /// <summary>
    /// Létrehoz egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <param name="characterList">A karakterek listája, amelyekből a kulcsot létre kell hozni.</param>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    private char[][] Generate(int height, int width, int strength, List<char> characterList)
    {
        // A kulcsot tároló kétdimenziós tömb létrehozása.
        char[][] key = new char[height][];
        for (int i = 0; i < height; i++)
        {
            key[i] = new char[width];
        }

        // A karakterek tárolására és keverésére alkalmas lista létrehozása.
        ShuffleList<char> shuffleList = [];
        shuffleList.EnsureCapacity(characterList.Count * strength);

        // A karakterek hozzáadása a listához.
        foreach (char character in characterList)
        {
            for (int i = 0; i < strength; i++)
            {
                shuffleList.Add(character);
            }
        }

        // A lista kiegészítése véletlen karakterekkel.
        int space = height * width - shuffleList.Count;
        for (int i = 0; i < space; i++)
        {
            int randomIndex = _random.Next(0, characterList.Count);
            shuffleList.Add(characterList[randomIndex]);
        }

        // A karakterek összekeverése.
        shuffleList.Shuffle();

        // A kulcs feltöltése a karakterekkel.
        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                key[j][i] = shuffleList.ElementAt(j * width + i);
            }
        }

        // A kész kulcs visszaadása.
        return key;
    }
}

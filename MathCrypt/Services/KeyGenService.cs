using MathCrypt.Enums;
using MathCrypt.Helpers;

namespace MathCrypt.Services;

/// <summary>
/// A titkosító algoritmus kulcsát generáló szolgáltatás.
/// </summary>
public class KeyGenService
{
    /// <summary>
    /// Az osztály példányát tároló adattag.
    /// </summary>
    private static readonly Lazy<KeyGenService> _instance = new(() => new KeyGenService());

    /// <summary>
    /// A véletlenszám generátort tároló adattag.
    /// </summary>
    private readonly Random _random;

    /// <summary>
    /// Az osztály konstruktora.
    /// </summary>
    private KeyGenService()
    {
        _random = new Random();
    }

    /// <summary>
    /// Az osztály példányhoz való hozzáférést biztosító publikus tulajdonság.
    /// </summary>
    public static KeyGenService Instance => _instance.Value;

    /// <summary>
    /// Generál egy kulcsot az alapértelmezet paraméterek alapján.
    /// </summary>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    public char[][] GenerateKey()
    {
        char[] charset = CharsetHelper.GetDefaultCharsets();
        (int height, int width) = FindSmallestArraySize(charset.Length);
        return Generate(height, width, 1, charset);
    }

    /// <summary>
    /// Generál egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    public char[][] GenerateKey(int height, int width)
    {
        char[] charset = CharsetHelper.GetDefaultCharsets();
        if (height * width - charset.Length < 0)
        {
            (height, width) = FindSmallestArraySize(charset.Length);
        }
        return Generate(height, width, 1, charset);
    }

    /// <summary>
    /// Generál egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    public char[][] GenerateKey(int strength)
    {
        char[] charset = CharsetHelper.GetDefaultCharsets();
        (int height, int width) = FindSmallestArraySize(charset.Length * strength);
        return Generate(height, width, strength, charset);
    }

    /// <summary>
    /// Generál egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="charsets">A kulcsban használt karakterkészletek.</param>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    public char[][] GenerateKey(params ECharset[] charsets)
    {
        char[] charset = CharsetHelper.GetCharacters(charsets);
        (int height, int width) = FindSmallestArraySize(charset.Length);
        return Generate(height, width, 1, charset);
    }

    /// <summary>
    /// Generál egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    public char[][] GenerateKey(int height, int width, int strength)
    {
        char[] charset = CharsetHelper.GetDefaultCharsets();
        if (height * width - strength * charset.Length < 0)
        {
            (height, width) = FindSmallestArraySize(charset.Length * strength);
        }
        return Generate(height, width, strength, charset);
    }

    /// <summary>
    /// Generál egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <param name="charsets">A kulcsban használt karakterkészletek.</param>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    public char[][] GenerateKey(int height, int width, params ECharset[] charsets)
    {
        char[] charset = CharsetHelper.GetCharacters(charsets);
        if (height * width - charset.Length < 0)
        {
            (height, width) = FindSmallestArraySize(charset.Length);
        }
        return Generate(height, width, 1, charset);
    }

    /// <summary>
    /// Generál egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <param name="charsets">A kulcsban használt karakterkészletek.</param>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    public char[][] GenerateKey(int strength, params ECharset[] charsets)
    {
        char[] charset = CharsetHelper.GetCharacters(charsets);
        (int height, int width) = FindSmallestArraySize(charset.Length * strength);
        return Generate(height, width, strength, charset);
    }

    /// <summary>
    /// Generál egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <param name="charsets">A kulcsban használt karakterkészletek.</param>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    public char[][] GenerateKey(int height, int width, int strength, params ECharset[] charsets)
    {
        char[] charset = CharsetHelper.GetCharacters(charsets);
        if (height * width - strength * charset.Length < 0)
        {
            (height, width) = FindSmallestArraySize(charset.Length * strength);
        }
        return Generate(height, width, strength, charset);
    }

    /// <summary>
    /// Megkeresi a legkisebb kétdimenziós tömb méretet, ami eltudja tárolni az adott számú karaktert.
    /// </summary>
    /// <param name="n">Az eltárolni kívánt karakterek száma.</param>
    /// <returns>A kétdimenziós tömb magassága és szélessége.</returns>
    private (int height, int width) FindSmallestArraySize(int n)
    {
        double height = Math.Floor(Math.Sqrt(n));
        double width = height + Math.Ceiling((n - Math.Pow(height, 2)) / height);

        return ((int) height, (int) width);
    }

    /// <summary>
    /// Generál egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <param name="charset">A kulcsban használt karakterkészletek.</param>
    /// <returns>A generált kulcs kétdimenziós karakter tömb formájában.</returns>
    private char[][] Generate(int height, int width, int strength, char[] charset)
    {
        char[][] key = new char[height][];
        for (int i = 0; i < height; i++)
        {
            key[i] = new char[width];
        }

        List<char> list = [];
        foreach (char character in charset)
        {
            for (int i = 0; i < strength; i++)
            {
                list.Add(character);
            }
        }

        int space = height * width - list.Count;
        for (int i = 0; i < space; i++)
        {
            list.Add(CharsetHelper.GetCharacters(ECharset.Space)[0]);
        }

        for (int i = 0; i < list.Count; i++)
        {
            int newIndex = _random.Next(i, list.Count);
            (list[i], list[newIndex]) = (list[newIndex], list[i]);
        }

        for (int j = 0; j < height; j++)
        {
            for (int i = 0; i < width; i++)
            {
                key[j][i] = list.ElementAt(j * width + i);
            }
        }

        return key;
    }
}

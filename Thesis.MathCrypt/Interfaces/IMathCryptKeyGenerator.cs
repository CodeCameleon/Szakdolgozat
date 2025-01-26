using Shared.Enums;

namespace Thesis.MathCrypt.Interfaces;

/// <summary>
/// A saját titkosító algoritmusom kulcsát lértehozó eszközt ábrázoló interfész.
/// </summary>
public interface IMathCryptKeyGenerator
{
    /// <summary>
    /// Létrehoz egy kulcsot az alapértelmezett paraméterek alapján.
    /// </summary>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    char[][] GenerateKey();

    /// <summary>
    /// Létrehoz egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    char[][] GenerateKey(int height, int width);

    /// <summary>
    /// Létrehoz egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    char[][] GenerateKey(int strength);

    /// <summary>
    /// Létrehoz egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="charsets">A karakterkészletek, amelyekből a kulcsot létre kell hozni.</param>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    char[][] GenerateKey(params ECharset[] charsets);

    /// <summary>
    /// Létrehoz egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    char[][] GenerateKey(int height, int width, int strength);

    /// <summary>
    /// Létrehoz egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <param name="charsets">A karakterkészletek, amelyekből a kulcsot létre kell hozni.</param>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    char[][] GenerateKey(int height, int width, params ECharset[] charsets);

    /// <summary>
    /// Létrehoz egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <param name="charsets">A karakterkészletek, amelyekből a kulcsot létre kell hozni.</param>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    char[][] GenerateKey(int strength, params ECharset[] charsets);

    /// <summary>
    /// Létrehoz egy kulcsot a megadott paraméterek alapján.
    /// </summary>
    /// <param name="height">A kulcs magassága.</param>
    /// <param name="width">A kulcs szélessége.</param>
    /// <param name="strength">Az egyes karakterek előfordulásának száma.</param>
    /// <param name="charsets">A karakterkészletek, amelyekből a kulcsot létre kell hozni.</param>
    /// <returns>A létrehozott kulcs kétdimenziós karakter tömb formájában.</returns>
    char[][] GenerateKey(int height, int width, int strength, params ECharset[] charsets);
}

using MathCrypt.Enums;
using MathCrypt.Services;

namespace SzakDProgram;

/// <summary>
/// A konzolos alkalmazást megvalósító osztály.
/// </summary>
internal class Program
{
    /// <summary>
    /// A konzolos alkalmazás belépési pontja.
    /// </summary>
    /// <param name="args">A parancssori argumentumok.</param>
    static void Main(string[] args)
    {
        // A titkosító szolgáltatás példányosítása.
        CryptionService service = new(KeyGenService.Instance.GenerateKey(
            height: 1,
            width: 354,
            strength: 3,
            ECharset.Space,
            ECharset.Numbers,
            ECharset.MathSymbols,
            ECharset.Punctuations,
            ECharset.Special,
            ECharset.EN,
            ECharset.HU
        ));

        // A titkosító szolgáltatás szótárának kiíratása.
        Console.WriteLine(service);

        // A letitkosított szöveg kiíratása.
        string cipherText = service.Encrypt("Alma");
        Console.WriteLine($"Cipher Text: {cipherText}");
    }
}

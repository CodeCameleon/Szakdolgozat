using Shared.Constants;

namespace Shared.Enums.Extensions;

/// <summary>
/// Az algoritmusok neveit tartalmazó enum kiterjesztései.
/// </summary>
public static class EAlgorithmNameExtensions
{
    /// <summary>
    /// A háttérszíneket tartalmazó szótár.
    /// </summary>
    private static readonly Dictionary<EAlgorithmName, string> _algorithmBackgroundColors = new()
    {
        { EAlgorithmName.Aes, "#B80F0A80" }, // Karmazsinvörös - 50%
        { EAlgorithmName.Blake2b, "#48AAAD80" }, // Teal - 50%
        { EAlgorithmName.Des, "#00A86B80" }, // Jádezöld - 50%
        { EAlgorithmName.Ecies, "#FC6C8580" }, // Görögdinnye - 50%
        { EAlgorithmName.ElGamal, "#8D400480" }, // Rozsda - 50%
        { EAlgorithmName.Keccak256, "#89310180" }, // Borostyán - 50%
        { EAlgorithmName.MathCrypt, "#1134A680" }, // Egyiptomi kék - 50%
        { EAlgorithmName.Rc2, "#FFD30080" }, // Cybersárga - 50%
        { EAlgorithmName.Ripemd160, "#AEF35980" }, // Zöldcitrom - 50%
        { EAlgorithmName.Rsa, "#8F00FF80" }, // Elektromos lila - 50%
        { EAlgorithmName.Sha256, "#FF991380" }, // Aranyhalnarancs - 50%
        { EAlgorithmName.TripleDes, "#01796F80" } // Fenyőzöld - 50%
    };

    /// <summary>
    /// A szegélyszíneket tartalmazó szótár.
    /// </summary>
    private static readonly Dictionary<EAlgorithmName, string> _algorithmBorderColors = new()
    {
        { EAlgorithmName.Aes, "#B80F0AFF" }, // Karmazsinvörös - 100%
        { EAlgorithmName.Blake2b, "#48AAADFF" }, // Teal - 100%
        { EAlgorithmName.Des, "#00A86BFF" }, // Jádezöld - 100%
        { EAlgorithmName.Ecies, "#FC6C85FF" }, // Görögdinnye - 100%
        { EAlgorithmName.ElGamal, "#8D4004FF" }, // Rozsda - 100%
        { EAlgorithmName.Keccak256, "#893101FF" }, // Borostyán - 100%
        { EAlgorithmName.MathCrypt, "#1134A6FF" }, // Egyiptomi kék - 100%
        { EAlgorithmName.Rc2, "#FFD300FF" }, // Cybersárga - 100%
        { EAlgorithmName.Ripemd160, "#AEF359FF" }, // Zöldcitrom - 100%
        { EAlgorithmName.Rsa, "#8F00FFFF" }, // Elektromos lila - 100%
        { EAlgorithmName.Sha256, "#FF9913FF" }, // Aranyhalnarancs - 100%
        { EAlgorithmName.TripleDes, "#01796FFF" } // Fenyőzöld - 100%
    };

    /// <summary>
    /// Lekéri az algoritmus háttérszínét.
    /// </summary>
    /// <param name="algorithm">A keresett algoritmus.</param>
    /// <returns>Az algoritmus háttérszínének hexadecimális kódja.</returns>
    public static string GetBackgroundColor(this EAlgorithmName algorithm)
    {
        if (_algorithmBackgroundColors.TryGetValue(algorithm, out var backgroundColor))
        {
            return backgroundColor;
        }

        throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, ErrorMessages.Undefined.AlgorithmBackgroundColor);
    }

    /// <summary>
    /// Lekéri az algoritmus szegélyszínét.
    /// </summary>
    /// <param name="algorithm">A keresett algoritmus.</param>
    /// <returns>Az algoritmus szegélyszínének hexadecimális kódja.</returns>
    public static string GetBorderColor(this EAlgorithmName algorithm)
    {
        if (_algorithmBorderColors.TryGetValue(algorithm, out var borderColor))
        {
            return borderColor;
        }

        throw new ArgumentOutOfRangeException(nameof(algorithm), algorithm, ErrorMessages.Undefined.AlgorithmBorderColor);
    }
}

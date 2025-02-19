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
        { EAlgorithmName.Aes, "#B80F0A80" },
        { EAlgorithmName.Des, "#00A86B80" },
        { EAlgorithmName.MathCrypt, "#1134A680" }
    };

    /// <summary>
    /// A szegélyszíneket tartalmazó szótár.
    /// </summary>
    private static readonly Dictionary<EAlgorithmName, string> _algorithmBorderColors = new()
    {
        { EAlgorithmName.Aes, "#B80F0AFF" },
        { EAlgorithmName.Des, "#00A86BFF" },
        { EAlgorithmName.MathCrypt, "#1134A6FF" }
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

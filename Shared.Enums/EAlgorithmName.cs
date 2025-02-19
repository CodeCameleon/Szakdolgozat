using System.ComponentModel.DataAnnotations;

namespace Shared.Enums;

/// <summary>
/// Az algoritmusok neveit tartalmazó enum.
/// </summary>
public enum EAlgorithmName
{
    /// <summary>
    /// AES
    /// </summary>
    [Display(Name = "AES")]
    Aes = 1,

    /// <summary>
    /// DES
    /// </summary>
    [Display(Name = "DES")]
    Des = 2,

    /// <summary>
    /// MathCrypt
    /// </summary>
    [Display(Name = "MathCrypt")]
    MathCrypt = 3
}
